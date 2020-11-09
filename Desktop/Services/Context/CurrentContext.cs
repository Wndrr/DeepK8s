using System.Threading.Tasks;
using k8s;
using Stl.Fusion;

namespace Desktop.Services.Context
{
    [ComputeService]
    public class CurrentContext
    {
        public CurrentContext()
        {
            CurrentSelectedContext = KubernetesClientConfiguration.LoadKubeConfig().CurrentContext;
        }

        private string CurrentSelectedContext { get; set; }
        
        public virtual void Set(string name)
        {
            CurrentSelectedContext = name;
            Computed.Invalidate(Get);
        }

        [ComputeMethod]
        public virtual async Task<string> Get()
        {
            return await Task.FromResult(CurrentSelectedContext);
        }
    }

    [ComputeService]
    public class KubernetesClientFactory
    {
        private readonly CurrentContext _currentContext;

        public KubernetesClientFactory(CurrentContext currentContext)
        {
            _currentContext = currentContext;
        }

        [ComputeMethod]
        public virtual async Task<Kubernetes> Get()
        {
            var configFile = await KubernetesClientConfiguration.LoadKubeConfigAsync();

            configFile.CurrentContext = await _currentContext.Get();
            var config = KubernetesClientConfiguration.BuildConfigFromConfigObject(configFile);

            return new Kubernetes(config);
        }
    }
}