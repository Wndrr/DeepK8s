using System;
using CertManagerDefinitions;
using k8s;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers.CertManager
{
    public class IssuerStateContainer : GenericStateContainer<Issuer, IssuerList>
    {
        public IssuerStateContainer(Kubernetes kubernetesClient, ILogger<GenericStateContainer<Issuer, IssuerList>> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}