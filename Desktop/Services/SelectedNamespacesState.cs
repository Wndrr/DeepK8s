using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Desktop.Services
{
    public class SelectedNamespacesState
    {
        private List<string> Namespaces { get; set; } = GetDefaultState();

        public ReadOnlyCollection<string> ToList()
        {
            return Namespaces.AsReadOnly();
        }
        
        public void Add(string ns)
        {
            ThrowIfNull(ns);
            Namespaces.Add(ns);
            NotifyUpdate();
        }

        public void Remove(string ns)
        {
            ThrowIfNull(ns);
            Namespaces.Remove(ns);
            NotifyUpdate();
        }


        public EventHandler<EventArgs> StateChanged;

        private void NotifyUpdate()
        {
            if(StateChanged != null)
                StateChanged.Invoke(this, EventArgs.Empty);
        }
        
        public void Toggle(string ns)
        {
            ThrowIfNull(ns);

            if (Namespaces.Contains(ns))
                Remove(ns);
            else
                Add(ns);
        }

        private static void ThrowIfNull(string ns)
        {
            if (ns == null) 
                throw new ArgumentNullException(nameof(ns));
        }

        public bool Contains(string ns)
        {
            return Namespaces.Contains(ns);
        }

        public void Clear()
        {
            Namespaces.Clear();
            NotifyUpdate();
        }

        public void AddRange(IEnumerable<string> range)
        {
            Namespaces.AddRange(range);
            NotifyUpdate();
        }

        public static List<string> GetDefaultState()
        {
            return new List<string> {"default"};
        }
    }
}