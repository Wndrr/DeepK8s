using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class PodStateContainer : GenericStateContainer<V1Pod, V1PodList>
    {
        /// <inheritdoc />
        public PodStateContainer(Kubernetes kubernetesClient, ILogger<GenericStateContainer<V1Pod, V1PodList>> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}