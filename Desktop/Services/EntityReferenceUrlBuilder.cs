using System;

namespace Desktop.Services
{
    public class EntityReferenceUrlBuilder
    {
        public Uri BuildEntityReferenceUri(string entityKind, string entityName, string? entityNamespace = null)
        {
            if(string.IsNullOrEmpty(entityKind))
                throw new ArgumentNullException("Can't be null or empty", nameof(entityKind));
            if(string.IsNullOrEmpty(entityName))
                throw new ArgumentNullException("Can't be null or empty", nameof(entityName));
            
            var entityReferenceUri = entityNamespace == null ? $"/{entityKind}/{entityName}" : $"/{entityNamespace}/{entityKind}/{entityName}";

            return new Uri(entityReferenceUri, UriKind.Relative);
        }
    }
}