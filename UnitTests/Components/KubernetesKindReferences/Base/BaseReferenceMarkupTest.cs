using Bunit;
using Desktop.Components.KubernetesKindReferences.Base;
using Xunit;

namespace UnitTests.Components.KubernetesKindReferences.Base
{
    public class BaseReferenceMarkupTest : TestContext
    {
        private const string ColorclassName= "ColorClassName";
        private const string EntityName= "EntityName";

        [Fact]
        public void HasSpanWithColorClass()
        {
            var component = BuildComponent();

            var childSpan = component.Find("span");
            Assert.Contains(ColorclassName, childSpan.ClassList);
        }   
        
        [Fact]
        public void HasSpanWithEntityNameContent()
        {
            var component = BuildComponent();

            var childSpan = component.Find("span");
            Assert.Contains(EntityName, childSpan.TextContent);
        }

        private IRenderedComponent<BaseReferenceMarkup> BuildComponent()
        {
            var component = RenderComponent<BaseReferenceMarkup>
            (
                ComponentParameterFactory.ChildContent(EntityName),
                ComponentParameter.CreateParameter("ColorClass", ColorclassName)
            );
            return component;
        }
    }
}