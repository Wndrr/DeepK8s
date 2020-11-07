using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class SecretStateContainer : GenericStateContainer<V1Secret, V1SecretList>
    {
        public SecretStateContainer(Kubernetes kubernetesClient, ILogger<SecretStateContainer> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}