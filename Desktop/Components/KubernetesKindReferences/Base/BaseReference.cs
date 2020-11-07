using System;
using Desktop.Services;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Desktop.Components.KubernetesKindReferences.Base
{
    public abstract class BaseReference<T> : ComponentBase where T : k8s.IMetadata<k8s.Models.V1ObjectMeta>
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public T Entity { get; set; }

        [Inject] public EntityReferenceUrlBuilder EntityReferenceUrlBuilder { get; set; }

        [Inject] public ILogger<BaseReference<T>> Logger { get; set; }

        protected string ColorClass = string.Empty;
        protected string IconClass = string.Empty;
        protected string PageName = string.Empty;
        private V1ObjectMeta Metadata => Entity?.Metadata;

        protected void Construct(string colorClass, string pagename, string iconClass = null)
        {
            PageName = pagename;
            ColorClass = colorClass;
            IconClass = iconClass;
        }

        protected string Url
        {
            get
            {
                try
                {
                    if (Metadata == null)
                    {
                        return string.Empty;
                    }

                    if (!string.IsNullOrEmpty(PageName) && !string.IsNullOrEmpty(Metadata.Name))
                    {
                        var uri = EntityReferenceUrlBuilder.BuildReferenceUri(PageName, Metadata.Name, Metadata.Namespace());
                        return uri.ToString();
                    }

                    Logger.LogWarning("Entity Uri could not be built for parameters", PageName, Metadata.Name, Metadata.Namespace());
                    return string.Empty;
                }
                catch (ArgumentException e)
                {
                    Logger.LogError(e, "Entity Uri could not be built for parameters", PageName, Metadata.Name, Metadata.Namespace());
                    return string.Empty;
                }
            }
        }
    }
}