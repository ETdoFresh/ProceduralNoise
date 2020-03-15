using System;
using UnityEngine;

namespace WaveFunctionCollapse.Scripts
{
    public class ChangeOutputGrid : MonoBehaviourWithPopulateButton
    {
        public OutputGridPossibilities outputGrid;

        private void OnValidate()
        {
            if (!outputGrid) outputGrid = FindObjectOfType<OutputGridPossibilities>();
        }

        public override void Populate()
        {
            outputGrid.ShowPatterns();
        }
    }
}
