using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace Desktop.Components.Bases.Fusion
{
    public class LiveList<TList, TEntity> : FusionBacked<TList, TEntity>
        where TList : class, IKubernetesObject<V1ListMeta>, IItems<TEntity>
        where TEntity : class, IKubernetesObject<V1ObjectMeta>, IKubernetesObject, IMetadata<V1ObjectMeta>
    {
        protected override async Task<List<TEntity>> ComputeStateAsync(CancellationToken cancellationToken)
        {
            return await K8SRepository.GetAll();
        }
    }
}