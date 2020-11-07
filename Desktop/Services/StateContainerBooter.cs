using System;
using System.Threading;
using System.Threading.Tasks;
using Desktop.Services.StateContainers;
using Microsoft.Extensions.Hosting;

namespace Desktop.Services
{
    public class StateContainerBooter : IHostedService
    {
        public StateContainerBooter(StateContainerLoadingSupervisor stateContainerLoadingSupervisor)
        {
            StateContainerLoadingSupervisor = stateContainerLoadingSupervisor;
        }

        private StateContainerLoadingSupervisor StateContainerLoadingSupervisor { get; set; }
        
        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await StateContainerLoadingSupervisor.LoadAllAsync(cancellationToken);
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(cancellationToken.IsCancellationRequested);
        }
    }
}