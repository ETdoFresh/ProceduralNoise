using UnityEngine;

namespace WaveFunctionCollapse.Scripts
{
    public class AllSteps : MonoBehaviourWithPopulateButton
    {
        public TileIds tileIds;
        public PatternIds patternIds;
        public GatherRules gatherRules;
        public OutputGridPossibilities outputGridPossibilities;
        public RepeatCollapse repeatCollapse;
        public ChangeOutputGrid changeOutputGrid;
        public ChangePatternToTileIds changePatternToTileIds;

        private void OnValidate()
        {
            if (!tileIds) tileIds = FindObjectOfType<TileIds>();
            if (!patternIds) patternIds = FindObjectOfType<PatternIds>();
            if (!gatherRules) gatherRules = FindObjectOfType<GatherRules>();
            if (!outputGridPossibilities) outputGridPossibilities = FindObjectOfType<OutputGridPossibilities>();
            if (!repeatCollapse) repeatCollapse = FindObjectOfType<RepeatCollapse>();
            if (!changeOutputGrid) changeOutputGrid = FindObjectOfType<ChangeOutputGrid>();
            if (!changePatternToTileIds) changePatternToTileIds = FindObjectOfType<ChangePatternToTileIds>();
        }

        public override void Populate()
        {
            tileIds.Populate();
            patternIds.Populate();
            gatherRules.Populate();
            outputGridPossibilities.Populate();
            repeatCollapse.Populate();
            changeOutputGrid.Populate();
            changePatternToTileIds.Populate();
        }
    }
}
