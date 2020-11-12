using System.Threading.Tasks;
using Desktop.Services.Config;
using Stl.Fusion;

namespace Desktop.Services.Context
{
    [ComputeService]
    public class CurrentContext
    {
        private readonly IKubeConfigLoader _kubeConfigLoader;
        private readonly SelectedNamespacesState SelectedNamespacesState;
        private string? _currentSelectedContext;
        
        public CurrentContext(IKubeConfigLoader kubeConfigLoader, SelectedNamespacesState selectedNamespacesState)
        {
            _kubeConfigLoader = kubeConfigLoader;
            SelectedNamespacesState = selectedNamespacesState;
        }

        public virtual void Set(string name)
        {
            _currentSelectedContext = name;
            SelectedNamespacesState.ResetToDefault();
            Computed.Invalidate(Get);
        }

        [ComputeMethod]
        public virtual async Task<string> Get()
        {
            _currentSelectedContext ??= (await _kubeConfigLoader.Get()).CurrentContext;
            return _currentSelectedContext;
        }
    }
}