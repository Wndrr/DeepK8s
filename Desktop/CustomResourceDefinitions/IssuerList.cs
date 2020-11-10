using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace Desktop.CustomResourceDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "cert-manager.io", Kind = "IssuerList", PluralName = "issuers")]
    public class IssuerList : IKubernetesObject<V1ListMeta?>, IItems<Issuer>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; } = null!; 

        /// <inheritdoc />
        public string Kind { get; set; } = null!;

        /// <inheritdoc />
        public V1ListMeta? Metadata { get; set; } = null;

        /// <inheritdoc />
        public IList<Issuer>? Items { get; set; } = null;
    }
}