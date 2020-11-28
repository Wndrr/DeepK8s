using System.Collections.Generic;
using k8s.Models;

namespace Desktop.Services
{
    public static class PodSelectionPredicateHelper
    {

        #region LABELS

        public static bool PodHasMatchingLabels(this V1Pod pod, IDictionary<string, string>? selector)
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

        public static bool PodHasMatchingNamespace(this V1Pod pod, V1ObjectMeta metadata)
        {
            return pod.Metadata.Namespace() == metadata.Namespace();
        }
        
        #endregion

    }
}