using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class IngressStateContainer : GenericStateContainer<Extensionsv1beta1Ingress, Extensionsv1beta1IngressList>
    {
        public IngressStateContainer(Kubernetes kubernetesClient, ILogger<IngressStateContainer> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}