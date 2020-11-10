using k8s;
using k8s.Models;

namespace Desktop.Services
{
    public class KubernetesCommandLineBuilder
    {
        public string PortForward(string name, string? type, int localPort, int remotePort, string targetNamespace)
        {
            if(type == null)
                return $"kubectl port-forward {name} {localPort}:{remotePort} --namespace={targetNamespace}";
            
            return $"kubectl port-forward {type}/{name} {localPort}:{remotePort} --namespace={targetNamespace}";
        }

        public string PortForward(IKubernetesObject<V1ObjectMeta> entity, int localPort, int remotePort)
        {
            return PortForward(entity.Metadata.Name, entity.Kind, localPort, remotePort, entity.Metadata.Namespace());
        }

        public string RemoteShell(V1Pod pod)
        {
            return RemoteShell(pod.Metadata.Name, pod.Metadata.Namespace());
        }
        
        public string RemoteShell(string name, string targetNamespace)
        {
            return $"kubectl exec --stdin --tty {name} --namespace={targetNamespace} -- /bin/bash";
        }

        public string PodLogs(V1Pod pod)
        {
            return PodLogs(pod.Metadata.Name, pod.Metadata.Namespace());
        }
        
        public string PodLogs(string name, string targetNamespace)
        {
            return $"kubectl logs {name} --namespace={targetNamespace}";
        }
    }
}