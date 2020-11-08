using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.Extensions.DependencyInjection;
using Stl.Fusion;

namespace Desktop.Fusion
{
    [ComputeService]
    public class EntitiesAccessor : ICache
    {
        private readonly IServiceProvider _provider;

        public EntitiesAccessor(IServiceProvider provider)
        {
            _provider = provider;
        }

        [ComputeMethod]
        public virtual async Task<List<TEntity>> GetAll<TList, TEntity>()
            where TList : class, IKubernetesObject<V1ListMeta>, IItems<TEntity>
            where TEntity : class, IKubernetesObject<V1ObjectMeta>, IKubernetesObject, IMetadata<V1ObjectMeta>
        {
            Console.WriteLine("EntitiesAccessor.GetAll");
            Console.WriteLine("EntitiesAccessor.Getter");
            var entitiesDatabase1 = (EntitiesDatabase<TList, TEntity>)_provider.GetRequiredService(typeof(EntitiesDatabase<TList, TEntity>));
            var entitiesDatabase = entitiesDatabase1;
            var kubernetesObjects = await entitiesDatabase.GetAll().ConfigureAwait(false);
            
            Console.WriteLine($"EntitiesAccessor.GetAll - Got {kubernetesObjects.Count} items");
            return kubernetesObjects;
        }

        [ComputeMethod]
        public virtual Task<TEntity?> Get<TList, TEntity>(string entityName, string entityNamespace)
            where TList : class, IKubernetesObject<V1ListMeta>, IItems<TEntity>
            where TEntity : class, IKubernetesObject<V1ObjectMeta>, IKubernetesObject, IMetadata<V1ObjectMeta>
        {
            Console.WriteLine("EntitiesAccessor.Getter");
            var entitiesDatabase = (EntitiesDatabase<TList, TEntity>)_provider.GetRequiredService(typeof(EntitiesDatabase<TList, TEntity>));
            return entitiesDatabase.Get(entityName, entityNamespace);
        }
    }
}