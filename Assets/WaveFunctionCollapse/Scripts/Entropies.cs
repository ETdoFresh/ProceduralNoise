using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse.Scripts
{
    public class Entropies : MonoBehaviourWithPopulateButton
    {
        public RelativeFrequency relativeFrequency;
        public OutputGridPossibilities outputGridPossibilities;
        public List<Item> all = new List<Item>();

        private void OnValidate()
        {
            if (!relativeFrequency) relativeFrequency = FindObjectOfType<RelativeFrequency>();
            if (!outputGridPossibilities) outputGridPossibilities = FindObjectOfType<OutputGridPossibilities>();
        }

        public override void Populate()
        {
            all.Clear();
            foreach (var cell in outputGridPossibilities.grid)
                all.Add(new Item { gridCell = cell, entropy = ComputeEntropy(cell.value.possible) });
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
            public OutputGridPossibilities.OutputCell gridCell;
            public float entropy;
        }
    }
}
