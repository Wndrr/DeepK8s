using System.Threading.Tasks;
using Desktop.Services.Config;
using k8s;
using Stl.Fusion;

namespace Desktop.Services.Context
{
    [ComputeService]
    public class KubernetesClientFactory
    {
        private readonly KubeConfigLoader _kubeConfigLoader;
        private readonly CurrentContext _currentContext;

        public KubernetesClientFactory(CurrentContext currentContext, KubeConfigLoader kubeConfigLoader)
        {
            _currentContext = currentContext;
            _kubeConfigLoader = kubeConfigLoader;
        }

        [ComputeMethod]
        public virtual async Task<Kubernetes> Get()
        {
            var configFile = await _kubeConfigLoader.Get();

            configFile.CurrentContext = await _currentContext.Get();
            var config = KubernetesClientConfiguration.BuildConfigFromConfigObject(configFile);

            return new Kubernetes(config);
        }
    }
}