using System;
using System.Threading.Tasks;
using System.Timers;
using k8s;
using k8s.Fluent;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace Desktop.Components.Bases
{
    public abstract class SingleNamespacedAutoRefresh<T> : SingleNamespaced where T : IKubernetesObject, IMetadata<V1ObjectMeta>
    {
        [Parameter]
        public string RequestedEntityName { get; set; }

        protected string? ParentPageName { get; set; }
        
        protected T Entity { get; set; }

        
        protected override async Task OnInitializedAsync()
        {
            var entityRequest = KubernetesClient.Request<T>(RequestedNamespace, RequestedEntityName);
            Entity = await entityRequest.ExecuteAsync<T>();

            var watch = entityRequest.ToWatch<T>();
            watch.EventReceived += EventReceived;
            watch.Run();
            await base.OnInitializedAsync();
        }

        private void EventReceived(Watch<T> watch, WatchEventType eventType, T entity)
        {
            switch (eventType)
            {
                case WatchEventType.Added:
                    break;
                case WatchEventType.Modified:
                    Entity = entity;
                    break;
                case WatchEventType.Deleted:
                    if(ParentPageName != null)
                        NavigationManager.NavigateTo(ParentPageName);
                    break;
                case WatchEventType.Error:
                    break;
                case WatchEventType.Bookmark:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null);
            }

            UpdateUi();
        }

        private async void UpdateUi()
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}