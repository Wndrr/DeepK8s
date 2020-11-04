using System;
using CertManagerDefinitions;
using k8s;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers.CertManager
{
    public class CertificateStateContainer : GenericStateContainer<Certificate, CertificateList>
    {
        public CertificateStateContainer(Kubernetes kubernetesClient, ILogger<GenericStateContainer<Certificate, CertificateList>> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}