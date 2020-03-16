using System.Linq;

namespace WaveFunctionCollapse.Scripts
{
    public class CollapseCell : MonoBehaviourWithPopulateButton
    {
        public MinEntropy minEntropy;
        public RelativeFrequency relativeFrequency;
        public OutputGridPossibilities outputGridPossibilities;
        public PatternIds.Item selection;

        private void OnValidate()
        {
            if (!minEntropy) minEntropy = FindObjectOfType<MinEntropy>();
            if (!relativeFrequency) relativeFrequency = FindObjectOfType<RelativeFrequency>();
            if (!outputGridPossibilities) outputGridPossibilities = FindObjectOfType<OutputGridPossibilities>();
        }

        public override void Populate()
        {
            var cell = minEntropy.minCell;
            var total = 0f;
            for (int i = 0; i < relativeFrequency.frequencies.Count; i++)
                if (cell.gridCell.value.possible.Any(p => p.id == i))
                    total += relativeFrequency.frequencies[i];

            var random = Random.Range(0f, 1f);
            var selectionValue = Range.Map(random, 0, 1, 0, total);
            var currentSum = 0f;
            for (int i = 0; i < relativeFrequency.frequencies.Count; i++)
                if (cell.gridCell.value.possible.Any(p => p.id == i))
                {
                    currentSum += relativeFrequency.frequencies[i];
                    if (selectionValue <= currentSum)
                    {
                        selection = cell.gridCell.value.possible.Where(p => p.id == i).First();
                        break;
                    }
                }

            cell.gridCell.value.possible.Clear();
            cell.gridCell.value.possible.Add(selection);
            outputGridPossibilities.RefreshCounts();
        }
    }
}
