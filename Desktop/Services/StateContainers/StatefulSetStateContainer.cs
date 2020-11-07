using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers
{
    public class StatefulSetStateContainer : GenericStateContainer<V1StatefulSet, V1StatefulSetList>
    {
        public StatefulSetStateContainer(Kubernetes kubernetesClient, ILogger<StatefulSetStateContainer> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    } 
}