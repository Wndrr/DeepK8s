using System.Threading.Tasks;
using Desktop.Services;
using k8s;
using Microsoft.AspNetCore.Components;

namespace Desktop.Components.Bases
{
    public abstract class Namespaced : ComponentBase
    {
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected Kubernetes KubernetesClient { get; set; }
        [Inject] protected KubernetesHelper KubernetesHelper { get; set; }
        [Inject] protected SelectedNamespacesState SelectedNamespacesState { get; set; }

        /// <inheritdoc />
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}