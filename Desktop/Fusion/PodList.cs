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
    [ComputeService] // You don't need this attribute if you manually register such services
    public class PodList
    {
        private Kubernetes KubernetesClient { get; set; }

        public PodList(Kubernetes kubernetesClient)
        {
            KubernetesClient = kubernetesClient;
        }

        private readonly ConcurrentDictionary<string, V1Pod> _items = new ConcurrentDictionary<string, V1Pod>();

        [ComputeMethod]
        public virtual async Task<List<V1Pod>> GetAll()
        {
            await EnsureInitialized();
            return _items.Select(s => s.Value).ToList();
        }

        [ComputeMethod]
        public virtual async Task<V1Pod?> Get(string entityName, string entityNamespace)
        {
            await EnsureInitialized();

            return _items.Select(s => s.Value).SingleOrDefault(
                entity => entity.Name() == entityName && entity.Namespace() == entityNamespace);
        }

        private async Task EnsureInitialized()
        {
            if (_isInitialized)
                return;

            var kubernetesRequest = KubernetesClient.Request<V1PodList>();
            var initialRequest = await kubernetesRequest.ExecuteAsync<V1PodList>();
            if (initialRequest?.Items != null)
                foreach (var initialRequestItem in initialRequest.Items)
                {
                    var fullPod = await KubernetesClient.Request<V1Pod>(initialRequestItem.Namespace(),
                        initialRequestItem.Metadata.Name).ExecuteAsync<V1Pod>();
                    _items.TryAdd(fullPod.Uid(), fullPod);
                }

            _watch = kubernetesRequest.ToWatch<V1Pod>(initialRequest.ResourceVersion());
            _watch.EventReceived += EventReceived;
            _watch.Run();

            _isInitialized = true;
        }

        private bool _isInitialized;
        private Watch<V1Pod>? _watch;


        private void EventReceived(Watch<V1Pod> watch, WatchEventType eventType, V1Pod entity)
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
            Computed.Invalidate(GetAll);
            Computed.Invalidate(() => Get(entityName, entityNamespace));
        }

        private void Add(V1Pod entity)
        {
            _items.TryAdd(entity.Uid(), entity);
            Invalidate(entity.Metadata.Name, entity.Namespace());
        }

        private void Update(V1Pod entity)
        {
            _items.TryUpdate(entity.Uid(), entity, _items[entity.Uid()]);
            Invalidate(entity.Metadata.Name, entity.Namespace());
        }

        private void Delete(V1Pod entity)
        {
            _items.Remove(entity.Uid(), out var _);
            Invalidate(entity.Metadata.Name, entity.Namespace());
        }
    }
}