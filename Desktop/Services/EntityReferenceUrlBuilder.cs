using System;

namespace Desktop.Services
{
    public class EntityReferenceUrlBuilder
    {
        public Uri BuildReferenceUri(string pageName, string entityName, string entityNamespace = null)
        {
            if(string.IsNullOrEmpty(pageName))
                throw new ArgumentException("Can't be null or empty", nameof(pageName));
            if(string.IsNullOrEmpty(entityName))
                throw new ArgumentException("Can't be null or empty", nameof(entityName));
            
            var entityReferenceUri = entityNamespace == null ? $"/{pageName}/{entityName}" : $"/{entityNamespace}/{pageName}/{entityName}";

            return new Uri(entityReferenceUri, UriKind.Relative);
        }
    }
}