using System;
using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace CertManagerDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "cert-manager.io", Kind = "ClusterIssuer", PluralName = "clusterissuers")]
    public class ClusterIssuer : IKubernetesObject<V1ObjectMeta>, ISpec<IssuerSpec>, IStatus<IssuerStatus>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; }

        /// <inheritdoc />
        public string Kind { get; set; }

        /// <inheritdoc />
        public V1ObjectMeta Metadata { get; set; }

        public IssuerSpec Spec { get; set; }
        public IssuerStatus Status { get; set; }
    }
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class PrivateKeySecretRef    {
        public string Name { get; set; } 
    }

    public class IssuerSpecAcme    {
        public string Email { get; set; } 
        public string PreferredChain { get; set; } 
        public PrivateKeySecretRef PrivateKeySecretRef { get; set; } 
        public string Server { get; set; } 
    }

    public class IssuerSpec    {
        public IssuerSpecAcme Acme { get; set; } 
    }

    public class IssuerStatusAcme    {
        public string LastRegisteredEmail { get; set; } 
        public string Uri { get; set; } 
    }

    public class IssuerStatus    {
        public IssuerStatusAcme Acme { get; set; } 
        public List<CertificateCondition> Conditions { get; set; } 
    }


}