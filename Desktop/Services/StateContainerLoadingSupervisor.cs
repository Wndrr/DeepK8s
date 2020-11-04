using System.Threading;
using System.Threading.Tasks;
using Desktop.Services.StateContainers;
using Desktop.Services.StateContainers.CertManager;

namespace Desktop.Services
{
    public class StateContainerLoadingSupervisor
    {
        public StateContainerLoadingSupervisor(NodeStateContainer nodeStateContainer,
            PodStateContainer podStateContainer, DeploymentStateContainer deploymentStateContainer,
            ServiceStateContainer serviceStateContainer, IngressStateContainer ingressStateContainer,
            StatefulSetStateContainer statefulSetStateContainer, DaemonSetStateContainer daemonSetStateContainer,
            PersistentVolumeClaimStateContainer persistentVolumeClaimStateContainer,
            PersistentVolumeStateContainer persistentVolumeStateContainer,
            ConfigMapStateContainer configMapStateContainer, SecretStateContainer secretStateContainer,
            NamespaceStateContainer namespaceStateContainer,
            CustomResourceDefinitionStateContainer customResourceDefinitionStateContainer,
            IssuerStateContainer issuerStateContainer, StorageClassStateContainer storageClassStateContainer,
            CertificateStateContainer certificateStateContainer, OrderStateContainer orderStateContainer, ChallengeStateContainer challengeStateContainer, ClusterIssuerStateContainer clusterIssuerStateContainer)
        {
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
            StorageClassStateContainer = storageClassStateContainer;
            NodeStateContainer = nodeStateContainer;

            #region CERT MANAGER

            IssuerStateContainer = issuerStateContainer;
            ClusterIssuerStateContainer = clusterIssuerStateContainer;
            CertificateStateContainer = certificateStateContainer;
            OrderStateContainer = orderStateContainer;
            ChallengeStateContainer = challengeStateContainer;

            #endregion
        }

        private CustomResourceDefinitionStateContainer CustomResourceDefinitionStateContainer { get; set; }
        private NamespaceStateContainer NamespaceStateContainer { get; set; }
        private StorageClassStateContainer StorageClassStateContainer { get; set; }
        private PodStateContainer PodStateContainer { get; set; }
        private DeploymentStateContainer DeploymentStateContainer { get; set; }
        private ServiceStateContainer ServiceStateContainer { get; set; }
        private IngressStateContainer IngressStateContainer { get; set; }
        private StatefulSetStateContainer StatefulSetStateContainer { get; set; }
        private DaemonSetStateContainer DaemonSetStateContainer { get; set; }
        private PersistentVolumeClaimStateContainer PersistentVolumeClaimStateContainer { get; set; }
        private PersistentVolumeStateContainer PersistentVolumeStateContainer { get; set; }
        private ConfigMapStateContainer ConfigMapStateContainer { get; set; }
        private SecretStateContainer SecretStateContainer { get; set; }
        private NodeStateContainer NodeStateContainer { get; set; }

        #region CERT MANAGER

        private IssuerStateContainer IssuerStateContainer { get; set; }
        private ClusterIssuerStateContainer ClusterIssuerStateContainer { get; set; }
        private CertificateStateContainer CertificateStateContainer { get; set; }
        private OrderStateContainer OrderStateContainer { get; set; }
        private ChallengeStateContainer ChallengeStateContainer { get; set; }

        #endregion

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
            await NodeStateContainer.Load(cancellationToken);
            await CustomResourceDefinitionStateContainer.Load(cancellationToken);
            await StorageClassStateContainer.Load(cancellationToken);

            #region CERT MANAGER

            await IssuerStateContainer.Load(cancellationToken);
            await ClusterIssuerStateContainer.Load(cancellationToken);
            await CertificateStateContainer.Load(cancellationToken);
            await OrderStateContainer.Load(cancellationToken);
            await ChallengeStateContainer.Load(cancellationToken);

            #endregion
        }
    }
}