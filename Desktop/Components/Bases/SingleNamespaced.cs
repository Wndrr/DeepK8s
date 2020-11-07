using Microsoft.AspNetCore.Components;

namespace Desktop.Components.Bases
{
    public abstract class SingleNamespaced : Namespaced
    {
        [Parameter]
        public string RequestedEntityNamespace { get; set; }
        protected string RequestedNamespace => RequestedEntityNamespace;
    }
}