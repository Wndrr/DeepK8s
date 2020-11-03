using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class NamespaceStateContainer : GenericStateContainer<V1Namespace, V1NamespaceList>
    {
        public NamespaceStateContainer(Kubernetes kubernetesClient, ILogger<NamespaceStateContainer> logger,
            IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}