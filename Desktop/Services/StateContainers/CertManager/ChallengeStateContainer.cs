using System;
using CertManagerDefinitions;
using k8s;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers.CertManager
{
    public class ChallengeStateContainer : GenericStateContainer<Challenge, ChallengeList>
    {
        public ChallengeStateContainer(Kubernetes kubernetesClient, ILogger<GenericStateContainer<Challenge, ChallengeList>> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}