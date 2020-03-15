using System;
using UnityEngine;

namespace WaveFunctionCollapse.Scripts
{
    public class ChangePatternToTileIds : MonoBehaviourWithPopulateButton
    {
        public OutputGridPossibilities outputGridPossibilities;

        private void OnValidate()
        {
            if (!outputGridPossibilities) outputGridPossibilities = FindObjectOfType<OutputGridPossibilities>();
        }

        public override void Populate()
        {
            outputGridPossibilities.ShowTiles();
        }
    }
}
