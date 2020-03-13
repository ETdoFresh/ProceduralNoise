using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WaveFunctionCollapse.Scripts
{
    public class RelativeFrequency : MonoBehaviourWithPopulateButton
    {
        public PatternIds patternIds;
        public List<float> frequencies = new List<float>();

        private void OnValidate()
        {
            if (!patternIds) patternIds = FindObjectOfType<PatternIds>();
        }

        public override void Populate()
        {
            frequencies.Clear();
            var count = patternIds.all.Count;
            var gridCount = patternIds.grid.Count;
            for (int i = 0; i < count; i++)
            {
                var cellCount = patternIds.grid.Count(p => p.value.id == i);
                var frequency = (float)cellCount / gridCount;
                frequencies.Add(frequency);
            }
        }
    }
}
