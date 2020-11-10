using System;
using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace Desktop.CustomResourceDefinitions
{
    [KubernetesEntity(ApiVersion = "v1", Group = "acme.cert-manager.io", Kind = "Order", PluralName = "orders")]
    public class Order : IKubernetesObject<V1ObjectMeta?>, ISpec<OrderSpec?>, IStatus<OrderStatus?>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; } = null!; 

        /// <inheritdoc />
        public string Kind { get; set; } = null!; 

        /// <inheritdoc />
        public V1ObjectMeta? Metadata { get; set; }

        public OrderSpec? Spec { get; set; }
        public OrderStatus? Status { get; set; }
    }

    public class OrderSpec    {
        public List<string> DnsNames { get; set; } = null!;
        public IssuerRef? IssuerRef { get; set; } = null;
        public string Request { get; set; } = null!;  
    }

    public class OrderChallenge    {
        public string Token { get; set; } = null!;  
        public string Type { get; set; } = null!;  
        public string Url { get; set; } = null!;  
    }

    public class OrderAuthorization
    {
        public List<OrderChallenge>? Challenges { get; set; } = null;
        public string Identifier { get; set; } = null!;  
        public string InitialState { get; set; } = null!;  
        public string Url { get; set; } = null!;  
        public bool Wildcard { get; set; } 
    }

    public class OrderStatus
    {
        public List<OrderAuthorization>? Authorizations { get; set; } = null;
        public string Certificate { get; set; } = null!;  
        public string FinalizeUrl { get; set; } = null!;  
        public string State { get; set; } = null!;  
        public string Url { get; set; } = null!;  
    }
}