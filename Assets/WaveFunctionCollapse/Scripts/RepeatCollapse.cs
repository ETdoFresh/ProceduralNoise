using UnityEngine;

namespace WaveFunctionCollapse.Scripts {
    public class RepeatCollapse : MonoBehaviourWithPopulateButton
    {
        public MinEntropyQueue minEntropyQueue = new MinEntropyQueue();
        public OutputGridPossibilities outputGrid;
        public Entropies entropies;
        public MinEntropy minEntropy;
        public CollapseCell collapseCell;
        public CollapseNeighbors collapseNeighbors;
        public Entropies.Item first;

        private void OnValidate()
        {
            if (!outputGrid) outputGrid = FindObjectOfType<OutputGridPossibilities>();
            if (!entropies) entropies = FindObjectOfType<Entropies>();
            if (!minEntropy) minEntropy = FindObjectOfType<MinEntropy>();
            if (!collapseCell) collapseCell = FindObjectOfType<CollapseCell>();
            if (!collapseNeighbors) collapseNeighbors = FindObjectOfType<CollapseNeighbors>();
        }

        public override void Populate()
        {
            for (var i = 0; i < 100; i++)
            {
                minEntropyQueue.Clear();

                outputGrid.Populate();
                entropies.Populate();
                minEntropyQueue.AddRange(entropies.all);

                while (minEntropyQueue.Count > 0)
                {
                    first = minEntropyQueue.Dequeue();
                    minEntropy.minCell = first;
                    minEntropy.entropy = first.entropy;
                    minEntropy.x = first.gridCell.x;
                    minEntropy.y = first.gridCell.y;
                    collapseCell.Populate();
                    collapseNeighbors.Populate();
                }

                var hasCollision = false;
                foreach(var cell in outputGrid.grid)
                    if (cell.value.possible.Count == 0)
                    {
                        hasCollision = true;
                        break;
                    }

                if (!hasCollision)
                {
                    outputGrid.ShowPatterns();
                    break;
                }
            }
        }
    }
}
