using System;
using k8s;
using k8s.Models;

namespace CertManagerDefinitions
{
    [KubernetesEntity(ApiVersion = "v1alpha1", Group = "certmanager.k8s.io", Kind = "Issuer", PluralName = "issuers")]
    public class CertManagerIssuer : IKubernetesObject, IMetadata<V1ObjectMeta>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; }

        /// <inheritdoc />
        public string Kind { get; set; }

        /// <inheritdoc />
        public V1ObjectMeta Metadata { get; set; }
    }
}