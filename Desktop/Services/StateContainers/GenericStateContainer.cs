using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Desktop.Components.UserInterface;
using JsonDiffPatchDotNet;
using k8s;
using k8s.Fluent;
using k8s.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Desktop.Services.StateContainers
{
    public class GenericStateContainer<TEntityType, TListType> : IDisposable where TEntityType : IKubernetesObject, IMetadata<V1ObjectMeta> where TListType : IKubernetesObject<V1ListMeta>, IItems<TEntityType>
    {
        private ILogger<GenericStateContainer<TEntityType, TListType>> _logger;
        private Watch<TEntityType>? _watch;
        private CancellationToken _cancellationToken;
        protected Kubernetes KubernetesClient { get; set; }
        protected IServiceProvider  IServiceProvider  { get; set; }
        public ConcurrentDictionary<string, TEntityType> Items { get; set; } = new ConcurrentDictionary<string, TEntityType>();

        public GenericStateContainer(Kubernetes kubernetesClient,
            ILogger<GenericStateContainer<TEntityType, TListType>> logger, IServiceProvider serviceProvider)
        {
            KubernetesClient = kubernetesClient;
            _logger = logger;
            IServiceProvider = serviceProvider;
        }

        private bool _isLoaded;
        private string? _currentlyLoadedContext;

        public async Task<int> Load(CancellationToken? cancellationToken = null)
        {
            if (InMemoryUserPreferencesStore.CurrentContextName != _currentlyLoadedContext)
                ContextHasChanged();
            
            if (_isLoaded) return Items.Count;
            
            _cancellationToken = cancellationToken ?? new CancellationToken();
            var initialRequest = await KubernetesClient.Request<TListType>().ExecuteAsync<TListType>(_cancellationToken);
            if (initialRequest?.Items == null)
                Items = new ConcurrentDictionary<string, TEntityType>();
            else
                Items = new ConcurrentDictionary<string, TEntityType>(initialRequest.Items.ToDictionary(s => s.Metadata.Uid, s => s));

            DispatchAddedForAll();
            if(initialRequest?.Items != null)
                Watch(initialRequest.ResourceVersion());
            _isLoaded = true;
            return Items.Count;
        }

        private void ContextHasChanged()
        {
            _isLoaded = false;
            _currentlyLoadedContext = InMemoryUserPreferencesStore.CurrentContextName;
            RemoveALl();
            _watch?.Dispose();
            KubernetesClient = (Kubernetes)IServiceProvider.GetService(typeof(Kubernetes));
        }

        private void RemoveALl()
        {
            foreach (var (key, entity) in Items)
            {
                EventReceived(WatchEventType.Deleted, entity);
            }
            
            Items.Clear();
        }

        private void DispatchAddedForAll()
        {
            foreach (var (key, entity) in Items)
            {
                DispatchStateChangedEvent(WatchEventType.Deleted, entity);
            }
        }


        private void Watch(string initialVersion)
        {
            KubernetesClient.Request<TEntityType>();
            var request = KubernetesClient.Request<TEntityType>();
            _watch = request.ToWatch<TEntityType>(initialVersion, false);
            _watch.EventReceived += EventReceived;
            _watch.Run(_cancellationToken);
        }

        public EventHandler<EntityStateEventArgs> StateChangedWithParams;
        public EventHandler StateChanged;

        // watch param exists only because an event callback requires it
        private void EventReceived(Watch<TEntityType> watch, WatchEventType eventType, TEntityType entity)
        {
            EventReceived(eventType, entity);
        }
        
        private void EventReceived(WatchEventType eventType, TEntityType entity)
        {
            UpdateItemsFromEvent(eventType, entity);
            DispatchStateChangedEvent(eventType, entity);
        }

        private void DispatchStateChangedEvent(WatchEventType eventType, TEntityType entity)
        {
            if (StateChanged == null || StateChangedWithParams == null)
                return;
            
            StateChangedWithParams.Invoke(this, new EntityStateEventArgs(eventType, entity));
            StateChanged.Invoke(this, EventArgs.Empty);
        }

        private void UpdateItemsFromEvent(WatchEventType eventType, TEntityType entity)
        {
            switch (eventType)
            {
                case WatchEventType.Added:
                    var isAddSuccess = Items.TryAdd(entity.Metadata.Uid, entity);
                    if (!isAddSuccess)
                        UpdateEntity(entity);
                    break;
                case WatchEventType.Modified:
                    UpdateEntity(entity);
                    break;
                case WatchEventType.Deleted:
                    if (!Items.ContainsKey(entity.Metadata.Uid))
                        break;
                    var isRemoveSuccess = Items.TryRemove(entity.Metadata.Uid, out _);
                    if (!isRemoveSuccess)
                        _logger.LogError($"Could not remove entity {entity.Metadata.Name}");
                    break;
                case WatchEventType.Error:
                    break;
                case WatchEventType.Bookmark:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null);
            }
        }

        private void UpdateEntity(TEntityType entity)
        {
            Items[entity.Metadata.Uid] = entity;
        }

        public class EntityStateEventArgs : EventArgs
        {
            public WatchEventType EventType { get; }
            public TEntityType Entity { get; }

            public EntityStateEventArgs(WatchEventType eventType, TEntityType entity)
            {
                EventType = eventType;
                Entity = entity;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _watch?.Dispose();
            KubernetesClient?.Dispose();
        }
    }

    public class JsonHelper
    {
        public static Dictionary<string, object> DeserializeAndFlatten(JToken token)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            FillDictionaryFromJToken(dict, token, "");
            return dict;
        }

        private static void FillDictionaryFromJToken(Dictionary<string, object> dict, JToken token, string prefix)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (JProperty prop in token.Children<JProperty>())
                    {
                        FillDictionaryFromJToken(dict, prop.Value, Join(prefix, prop.Name));
                    }

                    break;

                case JTokenType.Array:
                    int index = 0;
                    foreach (JToken value in token.Children())
                    {
                        FillDictionaryFromJToken(dict, value, Join(prefix, index.ToString()));
                        index++;
                    }

                    break;

                default:
                    dict.Add(prefix, ((JValue) token).Value);
                    break;
            }
        }

        private static string Join(string prefix, string name)
        {
            return (string.IsNullOrEmpty(prefix) ? name : prefix + "." + name);
        }
    }
}