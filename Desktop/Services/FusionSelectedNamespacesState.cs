using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Stl.Fusion;

namespace Desktop.Services
{
    [ComputeService]
    public class FusionSelectedNamespacesState
    {
        private List<string> Namespaces { get; set; } = GetDefaultState();

        [ComputeMethod]
        public virtual async Task<ReadOnlyCollection<string>> ToList()
        {
            return Namespaces.AsReadOnly();
        }
        
        public void Add(string ns)
        {
            ThrowIfNull(ns);
            Namespaces.Add(ns);
            Invalidate(ns);
        }

        public void Remove(string ns)
        {
            ThrowIfNull(ns);
            Namespaces.Remove(ns);
            Invalidate(ns);
        }

        private void Invalidate(string? ns = null)
        {
            Computed.Invalidate(ToList);
        }
        
        public void Toggle(string ns)
        {
            ThrowIfNull(ns);

            if (Namespaces.Contains(ns))
                Remove(ns);
            else
                Add(ns);
            
            Invalidate(ns);
        }

        private static void ThrowIfNull(string ns)
        {
            if (ns == null) 
                throw new ArgumentNullException(nameof(ns));
        }

        [ComputeMethod]
        public virtual async Task<bool> Contains(string ns)
        {
            var all = await ToList();
            return all.Contains(ns);
        }

        public void Clear()
        {
            Namespaces.Clear();
            Invalidate();
        }

        public void AddRange(IEnumerable<string> range)
        {
            Namespaces.AddRange(range);
            Invalidate();
        }

        public static List<string> GetDefaultState()
        {
            return new List<string> {"default"};
        }
    }
}