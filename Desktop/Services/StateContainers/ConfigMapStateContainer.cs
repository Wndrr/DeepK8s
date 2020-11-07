using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class ConfigMapStateContainer : GenericStateContainer<V1ConfigMap, V1ConfigMapList>
    {
        public ConfigMapStateContainer(Kubernetes kubernetesClient, ILogger<ConfigMapStateContainer> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}