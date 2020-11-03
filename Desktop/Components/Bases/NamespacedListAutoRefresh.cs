using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Desktop.Services.StateContainers;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Desktop.Components.Bases
{
    public abstract class NamespacedListAutoRefresh<TStateContainerType, TEntityType, TListType> : NamespacedList,IDisposable where TStateContainerType : GenericStateContainer<TEntityType, TListType> where TEntityType : IKubernetesObject, IMetadata<V1ObjectMeta> where TListType : IItems<TEntityType>, IKubernetesObject<V1ListMeta>
    {
        [Inject]
        private TStateContainerType EntitiesStateContainer { get; set; }

        protected ConcurrentDictionary<string, TEntityType> Entities => EntitiesStateContainer.Items;
        protected ConcurrentDictionary<string, TEntityType> FilteredEntities
        {
            get
            {
                var items = Entities.Where(item => SelectedNamespacesState.ToList().Contains(item.Value.Metadata.Namespace()));
                return new ConcurrentDictionary<string, TEntityType>(items.ToDictionary(s => s.Key, s => s.Value));
            }
        }

        protected override async Task OnInitializedAsync()
        {
            EntitiesStateContainer.StateChangedWithParams += UpdateUi;
            SelectedNamespacesState.StateChanged += UpdateUi;
            await base.OnInitializedAsync();
        }


        private async void UpdateUi(object? sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        private async void UpdateUi(object? sender, GenericStateContainer<TEntityType, TListType>.EntityStateEventArgs arguments)
        {
            InvokeAsync(StateHasChanged);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            EntitiesStateContainer.StateChangedWithParams -= UpdateUi;
            SelectedNamespacesState.StateChanged -= UpdateUi;
        }
    }
}