using System;
using CertManagerDefinitions;
using k8s;
using Microsoft.Extensions.Logging;

namespace Desktop.Services.StateContainers.CertManager
{
    public class OrderStateContainer : GenericStateContainer<Order, OrderList>
    {
        public OrderStateContainer(Kubernetes kubernetesClient, ILogger<GenericStateContainer<Order, OrderList>> logger, IServiceProvider serviceProvider) : base(kubernetesClient, logger, serviceProvider)
        {
        }
    }
}