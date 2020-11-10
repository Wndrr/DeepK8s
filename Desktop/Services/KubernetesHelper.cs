using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;

namespace Desktop.Services
{
    public class KubernetesHelper
    {
        public async Task<List<T>> ListNamespacedItemsAsync<T>(Func<string, Task<IItems<T>>> lambda, IEnumerable<string> namespaces)
        {
            var listEntitiesPerNamespaceTask = namespaces.Select(lambda).ToList();
            var listEntitiesPerNamespaceTaskCompleted = await Task.WhenAll(listEntitiesPerNamespaceTask);
            var result = listEntitiesPerNamespaceTaskCompleted.SelectMany(e => e.Items).ToList();
            return result;
        }
    }
}