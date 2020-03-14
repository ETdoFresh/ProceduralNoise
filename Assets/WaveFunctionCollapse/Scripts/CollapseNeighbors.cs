using System.Linq;

namespace WaveFunctionCollapse.Scripts {
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
            var cell = minEntropy.minCell.gridCell;
        
            var combinedRule = new GatherRules.Rule();
            foreach (var possibiilty in cell.value.possible)
            {
                var rule = gatherRules.rules.First(r => r.pattern == possibiilty);
                combinedRule.up.AddRange(rule.up.Except(combinedRule.up));
                combinedRule.down.AddRange(rule.down.Except(combinedRule.down));
                combinedRule.left.AddRange(rule.left.Except(combinedRule.left));
                combinedRule.right.AddRange(rule.right.Except(combinedRule.right));
            }

            cell.up.value.possible.RemoveAll(item => !combinedRule.up.Contains(item));
            cell.down.value.possible.RemoveAll(item => !combinedRule.down.Contains(item));
            cell.left.value.possible.RemoveAll(item => !combinedRule.left.Contains(item));
            cell.right.value.possible.RemoveAll(item => !combinedRule.right.Contains(item));
            
            outputGrid.grid.SetValue(cell.value, cell.x, cell.y);
            outputGrid.grid.SetValue(cell.up.value, cell.up.x, cell.up.y);
            outputGrid.grid.SetValue(cell.down.value, cell.down.x, cell.down.y);
            outputGrid.grid.SetValue(cell.left.value, cell.left.x, cell.left.y);
            outputGrid.grid.SetValue(cell.right.value, cell.right.x, cell.right.y);
        
            outputGrid.RefreshTiles();
        }
    }
}
