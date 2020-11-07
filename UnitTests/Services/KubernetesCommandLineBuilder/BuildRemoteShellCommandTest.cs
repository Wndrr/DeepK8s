using Xunit;

namespace UnitTests.Services.KubernetesCommandLineBuilder
{
    public class BuildRemoteShellCommandTest
    {
        [Fact]
        public void BuildPodRemoteShellCommand_command_is_valid()
        {
            var builder = new Desktop.Services.KubernetesCommandLineBuilder();
            var cmd = builder.RemoteShell("podname", "namespace");

            Assert.Equal("kubectl exec --stdin --tty podname --namespace=namespace -- /bin/bash", cmd);
        }
    }
}