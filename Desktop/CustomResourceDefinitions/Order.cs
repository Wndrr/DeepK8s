using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace Desktop.CustomResourceDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "acme.cert-manager.io", Kind = "Order", PluralName = "orders")]
    public class Order : IKubernetesObject<V1ObjectMeta>, ISpec<OrderSpec>, IStatus<OrderStatus>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; }

        /// <inheritdoc />
        public string Kind { get; set; }

        /// <inheritdoc />
        public V1ObjectMeta Metadata { get; set; }

        public OrderSpec Spec { get; set; }
        public OrderStatus Status { get; set; }
    }

    public class OrderSpec    {
        public List<string> DnsNames { get; set; } 
        public IssuerRef IssuerRef { get; set; } 
        public string Request { get; set; } 
    }

    public class OrderChallenge    {
        public string Token { get; set; } 
        public string Type { get; set; } 
        public string Url { get; set; } 
    }

    public class OrderAuthorization    {
        public List<OrderChallenge> Challenges { get; set; } 
        public string Identifier { get; set; } 
        public string InitialState { get; set; } 
        public string Url { get; set; } 
        public bool Wildcard { get; set; } 
    }

    public class OrderStatus    {
        public List<OrderAuthorization> Authorizations { get; set; } 
        public string Certificate { get; set; } 
        public string FinalizeUrl { get; set; } 
        public string State { get; set; } 
        public string Url { get; set; } 
    }
}