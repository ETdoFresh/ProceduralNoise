using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WaveFunctionCollapse.Scripts
{
    public class RelativeFrequency : MonoBehaviour
    {
        public PatternIds patternIds;
        public List<float> frequencies = new List<float>();

        private void OnValidate()
        {
            if (!patternIds) patternIds = FindObjectOfType<PatternIds>();

            frequencies.Clear();
            var count = patternIds.all.Count;
            var gridCount = patternIds.grid.Count;
            for (int i = 0; i < count; i++)
            {
                var cellCount = patternIds.grid.Count(p => p.id == i);
                var frequency = (float)cellCount / gridCount;
                frequencies.Add(frequency);
            }
        }
    }
}
