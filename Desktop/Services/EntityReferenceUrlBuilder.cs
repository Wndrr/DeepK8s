using System;

namespace Desktop.Services
{
    public class EntityReferenceUrlBuilder
    {
        public Uri BuildEntityReferenceUri(string entityKind, string entityName, string? entityNamespace = null)
        {
            if(string.IsNullOrEmpty(entityKind)) throw new ArgumentNullException(nameof(entityKind), "Can't be null or empty");
            if(string.IsNullOrEmpty(entityName)) throw new ArgumentNullException(nameof(entityKind), "Can't be null or empty");
            
            var entityReferenceUri = entityNamespace == null ? $"/{entityKind}/{entityName}" : $"/{entityNamespace}/{entityKind}/{entityName}";

            return new Uri(entityReferenceUri, UriKind.Relative);
        }
    }
}