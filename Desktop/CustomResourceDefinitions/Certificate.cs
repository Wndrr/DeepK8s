using System;
using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace Desktop.CustomResourceDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "cert-manager.io", Kind = "Certificate", PluralName = "certificates")]
    public class Certificate : IKubernetesObject<V1ObjectMeta>, ISpec<CertificateSpec>, IStatus<CertificateStatus>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; }

        /// <inheritdoc />
        public string Kind { get; set; }

        /// <inheritdoc />
        public V1ObjectMeta Metadata { get; set; }

        public CertificateSpec Spec { get; set; }
        public CertificateStatus Status { get; set; }
    }
    
    public class IssuerRef    {
        public string Group { get; set; } 
        public string Kind { get; set; } 
        public string Name { get; set; }
    }

    public class CertificateSpec    {
        public List<string> DnsNames { get; set; } 
        public IssuerRef IssuerRef { get; set; } 
        public string SecretName { get; set; } 
    }

    public class CertificateCondition    {
        public DateTime LastTransitionTime { get; set; } 
        public string Message { get; set; } 
        public string Reason { get; set; } 
        public string Status { get; set; } 
        public string Type { get; set; } 
    }

    public class CertificateStatus    {
        public List<CertificateCondition> Conditions { get; set; } 
        public DateTime NotAfter { get; set; } 
        public DateTime NotBefore { get; set; } 
        public DateTime RenewalTime { get; set; } 
        public int Revision { get; set; } 
    }

}