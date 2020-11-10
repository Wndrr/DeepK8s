using k8s;
using k8s.Models;

namespace Desktop.CustomResourceDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "cert-manager.io", Kind = "Issuer", PluralName = "issuers")]
    public class Issuer : IKubernetesObject<V1ObjectMeta?>, ISpec<IssuerSpec?>, IStatus<IssuerStatus?>
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
}