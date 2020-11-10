using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace Desktop.CustomResourceDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "cert-manager.io", Kind = "ClusterIssuer", PluralName = "clusterissuers")]
    public class ClusterIssuer : IKubernetesObject<V1ObjectMeta?>, ISpec<IssuerSpec?>, IStatus<IssuerStatus?>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; } = null!; 

        /// <inheritdoc />
        public string Kind { get; set; } = null!; 

        /// <inheritdoc />
        public V1ObjectMeta? Metadata { get; set; }

        public IssuerSpec? Spec { get; set; }
        public IssuerStatus? Status { get; set; }
    }
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class PrivateKeySecretRef    {
        public string Name { get; set; } = null!;  
    }

    public class IssuerSpecAcme    {
        public string Email { get; set; } = null!;  
        public string PreferredChain { get; set; } = null!;
        public PrivateKeySecretRef? PrivateKeySecretRef { get; set; } = null;
        public string Server { get; set; } = null!;  
    }

    public class IssuerSpec
    {
        public IssuerSpecAcme? Acme { get; set; } = null;
    }

    public class IssuerStatusAcme    {
        public string LastRegisteredEmail { get; set; } = null!;  
        public string Uri { get; set; } = null!;  
    }

    public class IssuerStatus
    {
        public IssuerStatusAcme? Acme { get; set; } = null;
        public List<CertificateCondition>? Conditions { get; set; } = null;
    }


}