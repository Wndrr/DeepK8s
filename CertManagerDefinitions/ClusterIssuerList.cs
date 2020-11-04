using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace CertManagerDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "cert-manager.io", Kind = "ClusterIssuerList", PluralName = "clusterissuers")]
    public class ClusterIssuerList : IKubernetesObject<V1ListMeta>, IItems<ClusterIssuer>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; }

        /// <inheritdoc />
        public string Kind { get; set; }

        /// <inheritdoc />
        public V1ListMeta Metadata { get; set; }

        /// <inheritdoc />
        public IList<ClusterIssuer> Items { get; set; }
    }
}