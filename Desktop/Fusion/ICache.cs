using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Stl.Fusion;

namespace Desktop.Fusion
{
    public interface ICache
    {
        public abstract Task<List<TEntity>> GetAll<TList, TEntity>()
            where TList : class, IKubernetesObject<V1ListMeta>, IItems<TEntity>
            where TEntity : class, IKubernetesObject<V1ObjectMeta>, IKubernetesObject, IMetadata<V1ObjectMeta>;

        public abstract Task<TEntity?> Get<TList, TEntity>(string entityName, string entityNamespace)
            where TList : class, IKubernetesObject<V1ListMeta>, IItems<TEntity>
            where TEntity : class, IKubernetesObject<V1ObjectMeta>, IKubernetesObject, IMetadata<V1ObjectMeta>;

    }

    public interface ICache<T>
        where T : class, IKubernetesObject<V1ObjectMeta>, IKubernetesObject, IMetadata<V1ObjectMeta>
    {
        public abstract Task<List<T>> GetAll();

        public abstract Task<T?> Get(string entityName, string entityNamespace);
    }
}