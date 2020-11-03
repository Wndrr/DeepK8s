using System.Collections.Generic;
using k8s.Models;

namespace Desktop.Services
{
    public class PodSelectionPredicateHelper
    {

        #region LABELS

        public bool PodHasMatchingLabels(V1Pod pod, V1StatefulSetSpec spec)
        {
            return PodHasMatchingLabels(pod, spec.Selector.MatchLabels);
        }
        public bool PodHasMatchingLabels(V1Pod pod, V1DaemonSetSpec spec)
        {
            return PodHasMatchingLabels(pod, spec.Selector.MatchLabels);
        }
        public bool PodHasMatchingLabels(V1Pod pod, V1DeploymentSpec spec)
        {
            return PodHasMatchingLabels(pod, spec.Selector.MatchLabels);
        }
        
        public bool PodHasMatchingLabels(V1Pod pod, V1ServiceSpec spec)
        {
            return PodHasMatchingLabels(pod, spec.Selector);
        }

        public bool PodHasMatchingLabels(V1Pod pod, IDictionary<string, string>? selector)
        {
            foreach (var (selectorName, selectorValue) in selector ?? new Dictionary<string, string>())
            {
                var podHasLabelKey = pod.Metadata.Labels.TryGetValue(selectorName, out var podLabelValue);
                var labelKeysAreDifferent = podLabelValue != selectorValue;
                
                if (!podHasLabelKey || labelKeysAreDifferent)
                    return false;
            }

            return true;
        }

        #endregion

        #region NAMESPACE

        public bool PodHasMatchingNamespace(V1Pod pod, V1ObjectMeta metadata)
        {
            return pod.Metadata.Namespace() == metadata.Namespace();
        }
        
        #endregion

    }
}