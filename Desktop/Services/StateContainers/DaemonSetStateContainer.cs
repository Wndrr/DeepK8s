using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class DaemonSetStateContainer : GenericStateContainer<V1DaemonSet, V1DaemonSetList>
    {
        public DaemonSetStateContainer(Kubernetes kubernetesClient, ILogger<DaemonSetStateContainer> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
            if (kubernetesClient == null) throw new ArgumentNullException(nameof(kubernetesClient));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
        }
    }
}