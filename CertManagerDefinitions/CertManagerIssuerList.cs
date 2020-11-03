using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace CertManagerDefinitions
{
    [KubernetesEntity(ApiVersion = "v1alpha1", Group = "certmanager.k8s.io", Kind = "IssuerList", PluralName = "issuers")]
    public class CertManagerIssuerList : IKubernetesObject<V1ListMeta>, IItems<CertManagerIssuer>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; }

        /// <inheritdoc />
        public string Kind { get; set; }

        /// <inheritdoc />
        public V1ListMeta Metadata { get; set; }

        /// <inheritdoc />
        public IList<CertManagerIssuer> Items { get; set; }
    }
}