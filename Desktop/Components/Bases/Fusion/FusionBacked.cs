using System.Collections.Generic;
using Desktop.Services;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Stl.Fusion.Blazor;

namespace Desktop.Components.Bases.Fusion
{
    public abstract class FusionBacked<TList, TEntity> : LiveComponentBase<List<TEntity>>
        where TList : class, IKubernetesObject<V1ListMeta>, IItems<TEntity>
        where TEntity : class, IKubernetesObject<V1ObjectMeta>, IKubernetesObject, IMetadata<V1ObjectMeta>
    {
        [Inject] protected KubernetesRepository<TList, TEntity> K8SRepository { get; set; } = null!;
    }
}