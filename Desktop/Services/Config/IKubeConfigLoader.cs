using System.Threading.Tasks;
using k8s.KubeConfigModels;
using Stl.Fusion;

namespace Desktop.Services.Config
{
    public interface IKubeConfigLoader
    {
        [ComputeMethod]
        public Task<K8SConfiguration> Get();
    }
}