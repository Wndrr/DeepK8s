using Xunit;

namespace UnitTests.Services.KubernetesCommandLineBuilder
{
    public class BuildPodPortForwardCommandTest
    {
        [Fact]
        public void BuildPodPortForwardCommand_null_type_is_not_added()
        {
            var builder = new Desktop.Services.KubernetesCommandLineBuilder();
            var cmd = builder.PortForward("podname", null, 8080, 80, "default");

            Assert.Equal("kubectl port-forward podname 8080:80 --namespace=default", cmd);
        }

        [Fact]
        public void BuildPodPortForwardCommand_with_namespace_has_namespace()
        {
            var builder = new Desktop.Services.KubernetesCommandLineBuilder();
            var cmd = builder.PortForward("podname","typeName", 8080, 80, "targetNamespace");

            Assert.Equal("kubectl port-forward typeName/podname 8080:80 --namespace=targetNamespace", cmd);
        }
    }
}