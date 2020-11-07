using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace CertManagerDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "acme.cert-manager.io", Kind = "OrderList", PluralName = "orders")]
    public class OrderList : IKubernetesObject<V1ListMeta>, IItems<Order>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; }

        /// <inheritdoc />
        public string Kind { get; set; }

        /// <inheritdoc />
        public V1ListMeta Metadata { get; set; }

        /// <inheritdoc />
        public IList<Order> Items { get; set; }
    }
}