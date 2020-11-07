using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class ServiceStateContainer : GenericStateContainer<V1Service, V1ServiceList>
    {
        public ServiceStateContainer(Kubernetes kubernetesClient, ILogger<ServiceStateContainer> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}