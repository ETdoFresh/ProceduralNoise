using System.Collections.Generic;
using System.Linq;

namespace WaveFunctionCollapse.Scripts
{
    public class CollapseNeighbors : MonoBehaviourWithPopulateButton
    {
        public GatherRules gatherRules;
        public MinEntropy minEntropy;
        public OutputGridPossibilities outputGrid;

        private void OnValidate()
        {
            if (!gatherRules) gatherRules = FindObjectOfType<GatherRules>();
            if (!minEntropy) minEntropy = FindObjectOfType<MinEntropy>();
            if (!outputGrid) outputGrid = FindObjectOfType<OutputGridPossibilities>();
        }

        public override void Populate()
        {
            var queue = new Queue<OutputGridPossibilities.OutputCell>();
            var visited = new List<OutputGridPossibilities.OutputCell>();
            queue.Enqueue(minEntropy.minCell.gridCell);

            while (queue.Count > 0)
            {
                var cell = queue.Dequeue();
                visited.Add(cell);

                var combinedRule = new GatherRules.Rule();
                foreach (var possibility in cell.value.possible)
                {
                    var rule = gatherRules.rules.First(r => r.pattern == possibility);
                    foreach (var neighbor in rule.possibilities.Keys)
                        combinedRule[neighbor].AddRange(rule[neighbor].Except(combinedRule[neighbor]));
                }

                foreach (var neighbor in combinedRule.possibilities.Keys)
                {
                    var previousCount = cell[neighbor].value.possible.Count;
                    var tempList = new List<PatternIds.Item>(cell[neighbor].value.possible);
                    cell[neighbor].value.possible.RemoveAll(item => !combinedRule[neighbor].Contains(item));
                    var count = cell[neighbor].value.possible.Count;
                    if (count != previousCount)
                        if (!visited.Contains(cell[neighbor]))
                            queue.Enqueue(cell[neighbor]);
                    if (count == 0)
                        queue.Clear();
                }
            }

            foreach (var cell in visited)
                outputGrid.grid.SetValue(cell.value, cell.x, cell.y);

            outputGrid.RefreshCounts();
        }
    }
}