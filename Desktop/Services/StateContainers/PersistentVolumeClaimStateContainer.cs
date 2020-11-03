using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class PersistentVolumeClaimStateContainer : GenericStateContainer<V1PersistentVolumeClaim, V1PersistentVolumeClaimList>
    {
        public PersistentVolumeClaimStateContainer(Kubernetes kubernetesClient, ILogger<PersistentVolumeClaimStateContainer> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}