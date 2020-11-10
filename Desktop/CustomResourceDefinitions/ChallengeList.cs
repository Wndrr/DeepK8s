using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace Desktop.CustomResourceDefinitions
{
    [KubernetesEntity(ApiVersion = "v1alpha1", Group = "certmanager.k8s.io", Kind = "ChallengeList", PluralName = "challenges")]
    public class ChallengeList : IKubernetesObject<V1ListMeta?>, IItems<Challenge>
    {
        /// <inheritdoc />
        public string ApiVersion { get; set; } = null!; 

        /// <inheritdoc />
        public string Kind { get; set; } = null!; 

        /// <inheritdoc />
        public V1ListMeta? Metadata { get; set; }

        /// <inheritdoc />
        public IList<Challenge>? Items { get; set; }
    }
}