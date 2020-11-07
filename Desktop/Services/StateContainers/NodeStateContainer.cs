using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class NodeStateContainer : GenericStateContainer<V1Node, V1NodeList>
    {
        public NodeStateContainer(Kubernetes kubernetesClient, ILogger<NodeStateContainer> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    } 
}