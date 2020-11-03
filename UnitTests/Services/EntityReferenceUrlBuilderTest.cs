using System;
using Desktop.Services;
using Xunit;

namespace UnitTests.Services
{
    public class EntityReferenceUrlBuilderTest
    {
        [Fact]
        public void NullPageNameThrows()
        {
            var builder = new EntityReferenceUrlBuilder();

            Assert.Throws<ArgumentException>(() => builder.BuildReferenceUri(null, "entityName", "entityNamespace"));
        }
        
        [Fact]
        public void EmptyPageNameThrows()
        {
            var builder = new EntityReferenceUrlBuilder();

            Assert.Throws<ArgumentException>(() => builder.BuildReferenceUri(string.Empty, "entityName", "entityNamespace"));
        }
        
        [Fact]
        public void NullEntityNameThrows()
        {
            var builder = new EntityReferenceUrlBuilder();

            Assert.Throws<ArgumentException>(() => builder.BuildReferenceUri("pageName", null, "entityNamespace"));
        }
        
        [Fact]
        public void EmptyEntityNameThrows()
        {
            var builder = new EntityReferenceUrlBuilder();

            Assert.Throws<ArgumentException>(() => builder.BuildReferenceUri("pageName", string.Empty, "entityNamespace"));
        }
        
        [Fact]
        public void UriCanBeBuiltWithoutNamespace()
        {
            var builder = new EntityReferenceUrlBuilder();
            var uri = builder.BuildReferenceUri("pageName", "entityName");
            
            Assert.Equal("/pageName/entityName", uri.ToString());
        }
        
        [Fact]
        public void UriCanBeBuiltWithNamespace()
        {
            var builder = new EntityReferenceUrlBuilder();
            var uri = builder.BuildReferenceUri("pageName", "entityName", "entityNamespace");
            
            Assert.Equal("/entityNamespace/pageName/entityName", uri.ToString());
        }
        
    }
}