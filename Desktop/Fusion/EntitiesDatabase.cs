using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Fluent;
using k8s.Models;
using Stl.Fusion;

namespace Desktop.Fusion
{
    [ComputeService]
    public class EntitiesDatabase<TListType, TEntityType> 
        : ICache<TEntityType>
        where TListType : class, IKubernetesObject<V1ListMeta>, IItems<TEntityType>
        where TEntityType : class, IKubernetesObject<V1ObjectMeta>, IKubernetesObject, IMetadata<V1ObjectMeta>
    {
        private Kubernetes KubernetesClient { get; set; }

        public EntitiesDatabase(Kubernetes kubernetesClient)
        {
            KubernetesClient = kubernetesClient;
        }

        private readonly ConcurrentDictionary<string, TEntityType> _items = new ConcurrentDictionary<string, TEntityType>();

        [ComputeMethod]
        public virtual async Task<List<TEntityType>> GetAll()
        {
            Console.WriteLine("EntitiesDatabase.GetAll");
            await EnsureInitialized();
            var kubernetesObjects = _items.Select(s => s.Value).ToList();
            
            Console.WriteLine($"EntitiesDatabase.GetAll - Got {kubernetesObjects.Count} items");
            return kubernetesObjects;
        }

        [ComputeMethod]
        public virtual async Task<TEntityType?> Get(string entityName, string entityNamespace)
        {
            await EnsureInitialized();

            return _items.Select(s => s.Value).SingleOrDefault(
                entity => entity.Name() == entityName && entity.Namespace() == entityNamespace);
        }

        private async Task EnsureInitialized()
        {
            if (_isInitialized)
                return;

            var kubernetesRequest = KubernetesClient.Request<TListType>();
            var initialRequest = await kubernetesRequest.ExecuteAsync<TListType>();
            if (initialRequest?.Items != null)
                foreach (var initialRequestItem in initialRequest.Items)
                {
                    var fullPod = await KubernetesClient.Request<TEntityType>(initialRequestItem.Namespace(),
                        initialRequestItem.Metadata.Name).ExecuteAsync<TEntityType>();
                    _items.TryAdd(fullPod.Uid(), fullPod);
                }

            _watch = kubernetesRequest.ToWatch<TEntityType>(initialRequest.ResourceVersion());
            _watch.EventReceived += EventReceived;
            _watch.Run();

            _isInitialized = true;
            Console.WriteLine("Initialized");
        }

        private bool _isInitialized;
        private Watch<TEntityType>? _watch;


        private void EventReceived(Watch<TEntityType> watch, WatchEventType eventType, TEntityType entity)
        {
            Console.WriteLine($"{entity.Kind}/{entity.Namespace()}/{entity.Metadata.Name}: {eventType.ToString()}");
            var hasKey = _items.ContainsKey(entity.Uid());
            switch (eventType)
            {
                case WatchEventType.Modified:
                case WatchEventType.Added:
                    if (hasKey)
                        Update(entity);
                    else
                        Add(entity);
                    break;
                case WatchEventType.Deleted:
                    if (hasKey)
                        Delete(entity);
                    break;
                case WatchEventType.Error:
                    break;
                case WatchEventType.Bookmark:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null);
            }
        }

        private void Invalidate(string entityName, string entityNamespace)
        {
            Computed.Invalidate(() => GetAll());
            Computed.Invalidate(() => Get(entityName, entityNamespace));
        }

        private void Add(TEntityType entity)
        {
            _items.TryAdd(entity.Uid(), entity);
            Invalidate(entity.Metadata.Name, entity.Namespace());
        }

        private void Update(TEntityType entity)
        {
            _items.TryUpdate(entity.Uid(), entity, _items[entity.Uid()]);
            Invalidate(entity.Metadata.Name, entity.Namespace());
        }

        private void Delete(TEntityType entity)
        {
            _items.Remove(entity.Uid(), out var _);
            Invalidate(entity.Metadata.Name, entity.Namespace());
        }
    }
}