using System;
using k8s;
using k8s.Models;

namespace CertManagerDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "cert-manager.io", Kind = "Issuer", PluralName = "issuers")]
    public class Issuer : IKubernetesObject<V1ObjectMeta>, ISpec<IssuerSpec>, IStatus<IssuerStatus>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; }

        /// <inheritdoc />
        public string Kind { get; set; }

        /// <inheritdoc />
        public V1ObjectMeta Metadata { get; set; }

        public IssuerSpec Spec { get; set; }
        public IssuerStatus Status { get; set; }
    }
}