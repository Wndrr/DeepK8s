using Xunit;

namespace UnitTests.Services.KubernetesCommandLineBuilder
{
    public class BuildPodLogsCommandTest
    {
        [Fact]
        public void BuildPodLogsCommand_command_is_valid()
        {
            var builder = new Desktop.Services.KubernetesCommandLineBuilder();
            var cmd = builder.PodLogs("podname", "namespace");

            Assert.Equal("kubectl logs podname --namespace=namespace", cmd);
        }
    }
}