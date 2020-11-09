using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desktop.Services.Context;
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
        private KubernetesClientFactory KubernetesClientFactory { get; set; }
        private SelectedNamespacesState SelectedNamespaces { get; set; }
        private CurrentContext CurrentContext { get; set; }

        public KubernetesRepository(SelectedNamespacesState selectedNamespaces, CurrentContext currentContext,
            KubernetesClientFactory kubernetesClientFactory)
        {
            SelectedNamespaces = selectedNamespaces;
            CurrentContext = currentContext;
            KubernetesClientFactory = kubernetesClientFactory;
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
            var items = await GetAll();
            var selectedNamespaces = await SelectedNamespaces.ToList();
            var kubernetesObjects = items.Where(i => selectedNamespaces.Contains(i.Namespace())).ToList();

            return kubernetesObjects;
        }

        [ComputeMethod]
        public virtual async Task<TEntity?> Get(string entityName, string? entityNamespace)
        {
            var items = await GetAll();
            return items.SingleOrDefault(entity =>
                entity.Name() == entityName && entity.Namespace() == entityNamespace);
        }

        private string _currentAppliedContext = string.Empty;

        private async Task EnsureInitialized()
        {
            var currentSelectedContext = await CurrentContext.Get();
            if (_currentAppliedContext != currentSelectedContext)
            {
                _isInitialized = false;
                _items.Clear();
                _currentAppliedContext = currentSelectedContext;
                Computed.Invalidate(GetAll);
            }

            if (_isInitialized)
                return;
            var client = await KubernetesClientFactory.Get();
            var kubernetesRequest = client.Request<TList>();
            var initialRequest = await kubernetesRequest.ExecuteAsync<TList>();
            if (initialRequest?.Items != null)
                foreach (var initialRequestItem in initialRequest.Items)
                {
                    var fullPod = await client.Request<TEntity>(initialRequestItem.Namespace(),
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

        private void Add(TEntity entity)
        {
            _items.TryAdd(entity.Uid(), entity);
            Computed.Invalidate(() => Get(entity.Metadata.Name, entity.Namespace()));
        }

        private void Update(TEntity entity)
        {
            _items.TryUpdate(entity.Uid(), entity, _items[entity.Uid()]);
            Computed.Invalidate(() => Get(entity.Metadata.Name, entity.Namespace()));
        }

        private void Delete(TEntity entity)
        {
            _items.Remove(entity.Uid(), out var _);
            Computed.Invalidate(() => Get(entity.Metadata.Name, entity.Namespace()));
        }
    }
}