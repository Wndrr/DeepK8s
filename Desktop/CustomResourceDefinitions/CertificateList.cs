﻿using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace Desktop.CustomResourceDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "cert-manager.io", Kind = "CertificateList", PluralName = "certificates")]
    public class CertificateList : IKubernetesObject<V1ListMeta?>, IItems<Certificate>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; } = null!; 

        /// <inheritdoc />
        public string Kind { get; set; } = null!; 

        /// <inheritdoc />
        public V1ListMeta? Metadata { get; set; }

        /// <inheritdoc />
        public IList<Certificate>? Items { get; set; }
    }
}