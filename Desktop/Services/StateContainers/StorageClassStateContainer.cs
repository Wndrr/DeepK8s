using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class StorageClassStateContainer : GenericStateContainer<V1StorageClass, V1StorageClassList>
    {
        public StorageClassStateContainer(Kubernetes kubernetesClient, ILogger<StorageClassStateContainer> logger,
            IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}