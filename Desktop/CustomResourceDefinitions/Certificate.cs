using System;
using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace Desktop.CustomResourceDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "cert-manager.io", Kind = "Certificate", PluralName = "certificates")]
    public class Certificate : IKubernetesObject<V1ObjectMeta?>, ISpec<CertificateSpec?>, IStatus<CertificateStatus?>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; } = null!; 

        /// <inheritdoc />
        public string Kind { get; set; } = null!; 

        /// <inheritdoc />
        public V1ObjectMeta? Metadata { get; set; }

        public CertificateSpec? Spec { get; set; }
        public CertificateStatus? Status { get; set; }
    }
    
    public class IssuerRef    {
        public string Group { get; set; } = null!;  
        public string Kind { get; set; } = null!;  
        public string Name { get; set; } = null!; 
    }

    public class CertificateSpec    {
        public List<string> DnsNames { get; set; } = null!;  
        public IssuerRef? IssuerRef { get; set; } = null;
        public string SecretName { get; set; } = null!;  
    }

    public class CertificateCondition    {
        public DateTime? LastTransitionTime { get; set; } = null; 
        public string Message { get; set; } = null!;  
        public string Reason { get; set; } = null!;  
        public string Status { get; set; } = null!;  
        public string Type { get; set; } = null!;  
    }

    public class CertificateStatus    {
        public List<CertificateCondition>? Conditions { get; set; } 
        public DateTime? NotAfter { get; set; } = null; 
        public DateTime? NotBefore { get; set; } = null; 
        public DateTime? RenewalTime { get; set; } = null; 
        public int Revision { get; set; } 
    }

}