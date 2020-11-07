using System;
using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace CertManagerDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "acme.cert-manager.io", Kind = "Challenge", PluralName = "challenges")]
    public class Challenge : IKubernetesObject<V1ObjectMeta>, ISpec<ChallengeSpec>, IStatus<ChallengeStatus>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; }

        /// <inheritdoc />
        public string Kind { get; set; }

        /// <inheritdoc />
        public V1ObjectMeta Metadata { get; set; }

        public ChallengeSpec Spec { get; set; }
        public ChallengeStatus Status { get; set; }
    }
    
    public class ChallengeSpec    {
        public string AuthorizationUrl { get; set; } 
        public string DnsName { get; set; } 
        public IssuerRef IssuerRef { get; set; } 
        public string Key { get; set; } 
        public string Token { get; set; } 
        public string Type { get; set; } 
        public string Url { get; set; } 
        public bool Wildcard { get; set; } 
    }

    public class ChallengeStatus    {
        public bool Presented { get; set; } 
        public bool Processing { get; set; } 
        public string Reason { get; set; } 
        public string State { get; set; } 
    }
}