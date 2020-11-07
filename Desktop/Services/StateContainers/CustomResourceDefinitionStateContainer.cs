using System;
using System.Linq;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Desktop.Services.StateContainers
{
    public class CustomResourceDefinitionStateContainer : GenericStateContainer<V1CustomResourceDefinition, V1CustomResourceDefinitionList>
    {
        public CustomResourceDefinitionStateContainer(Kubernetes kubernetesClient, ILogger<CustomResourceDefinitionStateContainer> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {

        }
    }
}