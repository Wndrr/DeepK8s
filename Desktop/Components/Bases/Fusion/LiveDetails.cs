using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Desktop.Services;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Stl.Fusion.Blazor;

namespace Desktop.Components.Bases.Fusion
{
    public class LiveDetails<TList, TEntity> : LiveComponentBase<TEntity?>
        where TList : class, IKubernetesObject<V1ListMeta>, IItems<TEntity>
        where TEntity : class, IKubernetesObject<V1ObjectMeta>, IKubernetesObject, IMetadata<V1ObjectMeta>
    {
        [Inject] private KubernetesRepository<TList, TEntity> K8SRepository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter] public string? RequestedEntityNamespace { get; set; }

        [Parameter] public string? RequestedEntityName { get; set; }

        protected string? ParentPageName { get; set; }

        protected override async Task<TEntity?> ComputeStateAsync(CancellationToken cancellationToken)
        {
            if (RequestedEntityName == null || RequestedEntityNamespace == null)
            {
                if (ParentPageName != null)
                    NavigationManager.NavigateTo(ParentPageName);

                return null;
            }

            var kubernetesObject = await K8SRepository.Get(RequestedEntityName, RequestedEntityNamespace);

            if (kubernetesObject == null && ParentPageName != null)
                NavigationManager.NavigateTo(ParentPageName);

            return kubernetesObject;
        }
    }
}