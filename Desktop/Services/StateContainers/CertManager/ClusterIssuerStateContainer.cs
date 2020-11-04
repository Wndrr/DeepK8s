using System;
using CertManagerDefinitions;
using k8s;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers.CertManager
{
    public class ClusterIssuerStateContainer : GenericStateContainer<ClusterIssuer, ClusterIssuerList>
    {
        public ClusterIssuerStateContainer(Kubernetes kubernetesClient, ILogger<GenericStateContainer<ClusterIssuer, ClusterIssuerList>> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}