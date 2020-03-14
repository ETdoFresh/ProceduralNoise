using System;
using System.Collections.Generic;
using System.Linq;

namespace WaveFunctionCollapse.Scripts
{
    [Serializable]
    public class MinEntropyQueue
    {
        private const double TOLERANCE = 0.000001;

        public  List<Entropies.Item> queue = new List<Entropies.Item>();

        public int Count => queue.Count;
        
        public void Clear() => queue.Clear();
        public void Add(Entropies.Item item) => queue.Add(item);
        public void AddRange(IEnumerable<Entropies.Item> collection) => queue.AddRange(collection);
        
        public Entropies.Item Dequeue()
        {
            var minEntropy = queue.Min(i => i.entropy);
            var item = queue.First(i => Math.Abs(i.entropy - minEntropy) < TOLERANCE);
            queue.Remove(item);
            return item;
        }
    }
}