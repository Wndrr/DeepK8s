using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class PersistentVolumeStateContainer : GenericStateContainer<V1PersistentVolume, V1PersistentVolumeList>
    {
        public PersistentVolumeStateContainer(Kubernetes kubernetesClient, ILogger<PersistentVolumeStateContainer> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}