using System.Threading.Tasks;
using k8s;
using k8s.KubeConfigModels;
using Stl.Fusion;

namespace Desktop.Services.Config
{
    [ComputeService]
    public class KubeConfigLoader : IKubeConfigLoader
    {
        [ComputeMethod]
        public virtual async Task<K8SConfiguration> Get()
        {
            return await KubernetesClientConfiguration.LoadKubeConfigAsync();
        }
    }
}