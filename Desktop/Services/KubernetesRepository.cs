using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Fluent;
using k8s.Models;
using Stl.Fusion;

namespace Desktop.Services
{
    public class KubernetesRepository<TList, TEntity>
        where TList : class, IKubernetesObject<V1ListMeta>, IItems<TEntity>
        where TEntity : class, IKubernetesObject<V1ObjectMeta>, IKubernetesObject, IMetadata<V1ObjectMeta>
    {
        private Kubernetes KubernetesClient { get; set; }
        private FusionSelectedNamespacesState SelectedNamespaces { get; set; }

        public KubernetesRepository(Kubernetes kubernetesClient, FusionSelectedNamespacesState selectedNamespaces)
        {
            KubernetesClient = kubernetesClient;
            SelectedNamespaces = selectedNamespaces;
        }

        private readonly ConcurrentDictionary<string, TEntity> _items = new ConcurrentDictionary<string, TEntity>();

        [ComputeMethod]
        public virtual async Task<List<TEntity>> GetAll()
        {
            Console.WriteLine("EntitiesDatabase.GetAll");
            await EnsureInitialized();
            var kubernetesObjects = _items.Select(s => s.Value).ToList();
            
            Console.WriteLine($"EntitiesDatabase.GetAll - Got {kubernetesObjects.Count} items");
            return kubernetesObjects;
        }

        [ComputeMethod]
        public virtual async Task<List<TEntity>> GetAllNamespaced()
        {
            await EnsureInitialized();
            var items = _items.Select(s => s.Value);
            var selectedNamespaces = await SelectedNamespaces.ToList();
            var kubernetesObjects = items.Where(i => selectedNamespaces.Contains(i.Namespace())).ToList();
            
            return kubernetesObjects;
        }

        [ComputeMethod]
        public virtual async Task<TEntity?> Get(string entityName, string? entityNamespace)
        {
            await EnsureInitialized();

            return _items.Select(s => s.Value).SingleOrDefault(
                entity => entity.Name() == entityName && entity.Namespace() == entityNamespace);
        }

        private async Task EnsureInitialized()
        {
            if (_isInitialized)
                return;

            var kubernetesRequest = KubernetesClient.Request<TList>();
            var initialRequest = await kubernetesRequest.ExecuteAsync<TList>();
            if (initialRequest?.Items != null)
                foreach (var initialRequestItem in initialRequest.Items)
                {
                    var fullPod = await KubernetesClient.Request<TEntity>(initialRequestItem.Namespace(),
                        initialRequestItem.Metadata.Name).ExecuteAsync<TEntity>();
                    _items.TryAdd(fullPod.Uid(), fullPod);
                }

            _watch = kubernetesRequest.ToWatch<TEntity>(initialRequest.ResourceVersion());
            _watch.EventReceived += EventReceived;
            _watch.Run();

            _isInitialized = true;
            Console.WriteLine("Initialized");
        }

        private bool _isInitialized;
        private Watch<TEntity>? _watch;


        private void EventReceived(Watch<TEntity> watch, WatchEventType eventType, TEntity entity)
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

        private void Add(TEntity entity)
        {
            _items.TryAdd(entity.Uid(), entity);
            Invalidate(entity.Metadata.Name, entity.Namespace());
        }

        private void Update(TEntity entity)
        {
            _items.TryUpdate(entity.Uid(), entity, _items[entity.Uid()]);
            Invalidate(entity.Metadata.Name, entity.Namespace());
        }

        private void Delete(TEntity entity)
        {
            _items.Remove(entity.Uid(), out var _);
            Invalidate(entity.Metadata.Name, entity.Namespace());
        }
    }
}