using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stl.Fusion;

namespace Desktop.Services
{
    [ComputeService]
    public class SelectedNamespacesState
    {
        private List<string> Namespaces { get; set; } = GetDefaultState();

        [ComputeMethod]
        public virtual Task<List<string>> ToList()
        {
            return Task.FromResult(Namespaces.ToList());
        }

        public void Add(string ns)
        {
            if (ns == null) throw new ArgumentNullException(nameof(ns));
            Namespaces.Add(ns);
            Invalidate();
        }

        public void Remove(string ns)
        {
            if (ns == null) throw new ArgumentNullException(nameof(ns));
            Namespaces.Remove(ns);
            Invalidate();
        }

        private void Invalidate()
        {
            Computed.Invalidate(ToList);
        }

        public void Toggle(string ns)
        {
            if (ns == null) throw new ArgumentNullException(nameof(ns));

            if (Namespaces.Contains(ns))
                Remove(ns);
            else
                Add(ns);

            Invalidate();
        }

        public bool Contains(string ns)
        {
            return Namespaces.Contains(ns);
        }

        public void Clear()
        {
            Namespaces.Clear();
            Invalidate();
        }
        public void ResetToDefault()
        {
            Namespaces.Clear();
            Namespaces.AddRange(GetDefaultState());
            Invalidate();
        }

        public void AddRange(IEnumerable<string> range)
        {
            Namespaces.AddRange(range);
            Invalidate();
        }

        private static List<string> GetDefaultState()
        {
            return new List<string> {"default"};
        }

    }
}