using System;
using System.Linq;
using CertManagerDefinitions;
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
    
    public class CertManagerIssuerStateContainer : GenericStateContainer<CertManagerIssuer, CertManagerIssuerList>
    {
        public CertManagerIssuerStateContainer(Kubernetes kubernetesClient, ILogger<GenericStateContainer<CertManagerIssuer, CertManagerIssuerList>> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
            // JObject.Parse(JsonConvert.SerializeObject(obj))["kind"];

            var tt = Items.Count;
        }
    }

}