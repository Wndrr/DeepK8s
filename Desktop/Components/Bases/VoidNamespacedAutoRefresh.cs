using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;

namespace Desktop.Components.Bases
{
    public abstract class VoidNamespacedAutoRefresh : Namespaced
    {
        protected override Task OnInitializedAsync()
        {
            var timer = new Timer(1000);
            timer.Elapsed += PerformRefresh;
            timer.Start();

            return base.OnInitializedAsync();
        }

        private async void PerformRefresh(object source, ElapsedEventArgs e)
        {
            await RefreshHandler(SelectedNamespacesState.ToList());
            await InvokeAsync(StateHasChanged);
        }

        public abstract Task RefreshHandler(ReadOnlyCollection<string> ns);
    }
}