using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Desktop.Services.StateContainers;
using k8s.Models;

namespace Desktop.Services
{
    public class GraphMaker
    {
        private PodStateContainer _podStateContainer;
        private ServiceStateContainer serviceStateContainer;
        private IngressStateContainer ingressStateContainer;
        private SelectedNamespacesState SelectedNamespacesState;

        public GraphMaker(PodStateContainer podStateContainer, IngressStateContainer ingressStateContainer, ServiceStateContainer serviceStateContainer, SelectedNamespacesState selectedNamespacesState)
        {
            _podStateContainer = podStateContainer;
            this.ingressStateContainer = ingressStateContainer;
            this.serviceStateContainer = serviceStateContainer;
            SelectedNamespacesState = selectedNamespacesState;
        }

        public List<Node> GetAllNodes()
        {
            var enumerable = _podStateContainer.Items.Where(s => SelectedNamespacesState.Contains(s.Value.Metadata.Namespace())).Select(p => new Node(p.Value.Metadata.Name, "pod")).ToList();
            enumerable.AddRange(serviceStateContainer.Items.Where(s => SelectedNamespacesState.Contains(s.Value.Metadata.Namespace())).Select(p => new Node(p.Value.Metadata.Name, "service")));
            enumerable.AddRange(ingressStateContainer.Items.Where(s => SelectedNamespacesState.Contains(s.Value.Metadata.Namespace())).Select(p => new Node(p.Value.Metadata.Name, "ingress")));
            return enumerable.ToList();
        }
        public List<Link> GetAllLinks()
        {
            var links = new List<Link>();
            foreach (var ingress in ingressStateContainer.Items.Where(s => SelectedNamespacesState.Contains(s.Value.Metadata.Namespace())))
            {
                foreach (var rule in ingress.Value.Spec.Rules ?? new List<Extensionsv1beta1IngressRule>())
                {
                    foreach (var path in rule.Http.Paths ?? new List<Extensionsv1beta1HTTPIngressPath>())
                    {
                        links.Add(new Link(ingress.Value.Metadata.Name, path.Backend.ServiceName));
                    }
                }
            }

            foreach (var service in serviceStateContainer.Items.Where(s => SelectedNamespacesState.Contains(s.Value.Metadata.Namespace())) ?? new ConcurrentDictionary<string, V1Service>())
            {   
                var selectedPods = _podStateContainer?.Items.Where(s => SelectedNamespacesState.Contains(s.Value.Metadata.Namespace()))?
                    .Where(pod => PodHasMatchingLabels(pod.Value, service.Value.Spec.Selector));

                foreach (var pod in selectedPods ?? new List<KeyValuePair<string, V1Pod>>())
                {
                    links.Add(new Link(service.Value.Metadata.Name, pod.Value.Metadata.Name));
                }
            }

            return links;
        }
        
        private bool PodHasMatchingLabels(V1Pod pod, IDictionary<string, string>? selectors)
        {
            foreach (var (selectorName, selectorValue) in selectors ?? new Dictionary<string, string>())
            {
                var hasLabel = pod.Metadata.Labels.TryGetValue(selectorName, out var podLabelValue);
                if (!hasLabel || podLabelValue != selectorValue)
                    return false;
            }

            return true;
        }

    }
    
    
    public class Node
    {
        public Node(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

        public string name { get; set; }
        public string type { get; set; }
    }  
    
    public class Link
    {
        public Link(string source, string target)
        {
            this.source = source;
            this.target = target;
        }

        public string source { get; set; }
        public string target { get; set; }
    }
}