using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse.Scripts
{
    public class Entropies : MonoBehaviourWithPopulateButton
    {
        public RelativeFrequency relativeFrequency;
        public OutputGrid outputGrid;
        public List<Item> all = new List<Item>();

        private void OnValidate()
        {
            if (!relativeFrequency) relativeFrequency = FindObjectOfType<RelativeFrequency>();
            if (!outputGrid) outputGrid = FindObjectOfType<OutputGrid>();
        }

        public override void Populate()
        {
            all.Clear();
            foreach (var cell in outputGrid.grid)
                all.Add(new Item { gridCell = cell, entropy = ComputeEntropy(cell.possible) });
        }

        private float ComputeEntropy(List<PatternIds.Item> possible)
        {
            var sumOfAllWeights = 0f;
            for (int i = 0; i < possible.Count; i++)
                sumOfAllWeights += relativeFrequency.frequencies[possible[i].id];

            var sumOfLog2OfEachWeight = 0f;
            for (int i = 0; i < possible.Count; i++)
                sumOfLog2OfEachWeight += Mathf.Log(relativeFrequency.frequencies[possible[i].id], 2);

            var log2OfSumOfAllWeights = Mathf.Log(sumOfAllWeights, 2);

            var smallRandomValue = UnityEngine.Random.Range(0f, 0.001f);
            return -log2OfSumOfAllWeights - sumOfLog2OfEachWeight / sumOfAllWeights + smallRandomValue;
        }

        [Serializable]
        public class Item
        {
            public OutputGrid.Item gridCell;
            public float entropy;
        }
    }
}
