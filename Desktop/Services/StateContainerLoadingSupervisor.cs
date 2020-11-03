using System.Threading;
using System.Threading.Tasks;
using Desktop.Services.StateContainers;

namespace Desktop.Services
{
    public class StateContainerLoadingSupervisor
    {
        public StateContainerLoadingSupervisor(NodeStateContainer nodeStateContainer,PodStateContainer podStateContainer, DeploymentStateContainer deploymentStateContainer, ServiceStateContainer serviceStateContainer, IngressStateContainer ingressStateContainer, StatefulSetStateContainer statefulSetStateContainer, DaemonSetStateContainer daemonSetStateContainer, PersistentVolumeClaimStateContainer persistentVolumeClaimStateContainer, PersistentVolumeStateContainer persistentVolumeStateContainer, ConfigMapStateContainer configMapStateContainer, SecretStateContainer secretStateContainer, NamespaceStateContainer namespaceStateContainer, CustomResourceDefinitionStateContainer customResourceDefinitionStateContainer, CertManagerIssuerStateContainer certManagerIssuerStateContainer, StorageClassStateContainer storageClassStateContainer) {
            PodStateContainer = podStateContainer;
            DeploymentStateContainer = deploymentStateContainer;
            ServiceStateContainer = serviceStateContainer;
            IngressStateContainer = ingressStateContainer;
            StatefulSetStateContainer = statefulSetStateContainer;
            DaemonSetStateContainer = daemonSetStateContainer;
            PersistentVolumeClaimStateContainer = persistentVolumeClaimStateContainer;
            PersistentVolumeStateContainer = persistentVolumeStateContainer;
            ConfigMapStateContainer = configMapStateContainer;
            SecretStateContainer = secretStateContainer;
            NamespaceStateContainer = namespaceStateContainer;
            CustomResourceDefinitionStateContainer = customResourceDefinitionStateContainer;
            CertManagerIssuerStateContainer = certManagerIssuerStateContainer;
            StorageClassStateContainer = storageClassStateContainer;
            NodeStateContainer = nodeStateContainer;
        }

        private CustomResourceDefinitionStateContainer CustomResourceDefinitionStateContainer { get; set; }
        private NamespaceStateContainer NamespaceStateContainer { get; set; }
        private StorageClassStateContainer StorageClassStateContainer { get; set; }
        private PodStateContainer PodStateContainer { get; set; }
        private DeploymentStateContainer DeploymentStateContainer {get;set;}
        private ServiceStateContainer ServiceStateContainer {get;set;}
        private IngressStateContainer IngressStateContainer {get;set;}
        private StatefulSetStateContainer StatefulSetStateContainer {get;set;}
        private DaemonSetStateContainer DaemonSetStateContainer {get;set;}
        private PersistentVolumeClaimStateContainer PersistentVolumeClaimStateContainer {get;set;}
        private PersistentVolumeStateContainer PersistentVolumeStateContainer {get;set;}
        private ConfigMapStateContainer ConfigMapStateContainer {get;set;}
        private SecretStateContainer SecretStateContainer {get;set;}
        private CertManagerIssuerStateContainer CertManagerIssuerStateContainer {get;set;}
        private NodeStateContainer NodeStateContainer {get;set;}

        public async Task LoadAllAsync(CancellationToken cancellationToken)
        {
            await NamespaceStateContainer.Load(cancellationToken);
            await PodStateContainer.Load(cancellationToken);
            await DeploymentStateContainer.Load(cancellationToken);
            await ServiceStateContainer.Load(cancellationToken);
            await IngressStateContainer.Load(cancellationToken);
            await StatefulSetStateContainer.Load(cancellationToken);
            await DaemonSetStateContainer.Load(cancellationToken);
            await PersistentVolumeClaimStateContainer.Load(cancellationToken);
            await PersistentVolumeStateContainer.Load(cancellationToken);
            await ConfigMapStateContainer.Load(cancellationToken);
            await SecretStateContainer.Load(cancellationToken);
            await CertManagerIssuerStateContainer.Load(cancellationToken);
            await NodeStateContainer.Load(cancellationToken);
            await CustomResourceDefinitionStateContainer.Load(cancellationToken);
            await StorageClassStateContainer.Load(cancellationToken);
        }
    }
}