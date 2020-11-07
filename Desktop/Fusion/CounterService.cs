using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Stl.Fusion;

namespace Desktop.Fusion
{
    [ComputeService] // You don't need this attribute if you manually register such services
    public class CounterService
    {
        private readonly ConcurrentDictionary<string, int> _counters = new ConcurrentDictionary<string, int>();

        [ComputeMethod]
        public virtual async Task<int> GetAsync(string key)
        {
            return _counters.TryGetValue(key, out var value) ? value : 0;
        }

        public async Task Increment(string key)
        {
            _counters.AddOrUpdate(key, k => 1, (k, v) => v + 1);
            Computed.Invalidate(() => GetAsync(key));
        }
    }
}