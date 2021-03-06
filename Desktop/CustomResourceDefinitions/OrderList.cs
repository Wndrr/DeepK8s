﻿using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace Desktop.CustomResourceDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "acme.cert-manager.io", Kind = "OrderList", PluralName = "orders")]
    public class OrderList : IKubernetesObject<V1ListMeta?>, IItems<Order>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; } = null!; 

        /// <inheritdoc />
        public string Kind { get; set; } = null!; 

        /// <inheritdoc />
        public V1ListMeta? Metadata { get; set; }

        /// <inheritdoc />
        public IList<Order>? Items { get; set; }
    }
}