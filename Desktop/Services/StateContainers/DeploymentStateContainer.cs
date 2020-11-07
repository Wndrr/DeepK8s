using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class DeploymentStateContainer : GenericStateContainer<V1Deployment, V1DeploymentList>
    {
        public DeploymentStateContainer(Kubernetes kubernetesClient, ILogger<DeploymentStateContainer> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}