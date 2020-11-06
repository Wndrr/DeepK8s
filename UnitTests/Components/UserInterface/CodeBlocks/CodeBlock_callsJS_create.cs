using Bunit;
using Bunit.Rendering;
using Bunit.TestDoubles.JSInterop;
using Desktop.Components.KubernetesKindReferences.Base;
using Desktop.Components.UserInterface.CodeBlocks;
using Xunit;

namespace UnitTests.Components.UserInterface.CodeBlocks
{
    public class CodeBlockCallsJsCreate : TestContext
    {
        [Fact]
        public void CodeBlock_Test()
        {
            var mockJs = Services.AddMockJSRuntime();
            mockJs.SetupVoid("blazorMonaco.editor.create");
            var component = RenderComponent<CodeBlock>
            (
                ComponentParameter.CreateParameter(nameof(CodeBlock.Value), "{'test': 'json'}"),
                ComponentParameter.CreateParameter(nameof(CodeBlock.Language), CodeLanguage.Unspecified)
            );
            
            Assert.Equal(1, mockJs.Invocations.Count);
        }
    }
}