using k8s;
using k8s.Models;

namespace Desktop.CustomResourceDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "acme.cert-manager.io", Kind = "Challenge", PluralName = "challenges")]
    public class Challenge : IKubernetesObject<V1ObjectMeta?>, ISpec<ChallengeSpec?>, IStatus<ChallengeStatus?>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; } = null!; 

        /// <inheritdoc />
        public string Kind { get; set; } = null!; 

        /// <inheritdoc />
        public V1ObjectMeta? Metadata { get; set; }

        public ChallengeSpec? Spec { get; set; }
        public ChallengeStatus? Status { get; set; }
    }
    
    public class ChallengeSpec    {
        public string AuthorizationUrl { get; set; } = null!;  
        public string DnsName { get; set; } = null!;
        public IssuerRef? IssuerRef { get; set; } = null;
        public string Key { get; set; } = null!;  
        public string Token { get; set; } = null!;  
        public string Type { get; set; } = null!;  
        public string Url { get; set; } = null!;
        public bool Wildcard { get; set; } = false;
    }

    public class ChallengeStatus
    {
        public bool Presented { get; set; } = false;
        public bool Processing { get; set; } = false;
        public string Reason { get; set; } = null!;  
        public string State { get; set; } = null!;  
    }
}