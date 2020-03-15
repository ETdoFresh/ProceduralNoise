using System;
using System.Collections.Generic;

namespace WaveFunctionCollapse.Scripts
{
    public class GatherRules : MonoBehaviourWithPopulateButton
    {
        public PatternIds patternIds;
        public List<Rule> rules = new List<Rule>();

        private void OnValidate()
        {
            if (!patternIds) patternIds = FindObjectOfType<PatternIds>();
        }

        public override void Populate()
        {
            rules.Clear();
            foreach (var pattern in patternIds.all)
            {
                var rule = new Rule();
                rule.pattern = pattern;
                foreach (var cell in patternIds.grid)
                    if (cell.value == pattern)
                        foreach (var neighbor in cell.neighbors.Keys)
                            if (!rule[neighbor].Contains(cell[neighbor].value))
                                rule[neighbor].Add(cell[neighbor].value);

                rules.Add(rule);
            }
        }

        [Serializable]
        public class Rule
        {
            public PatternIds.Item pattern;

            public Dictionary<Neighbor, List<PatternIds.Item>> possibilities =
                new Dictionary<Neighbor, List<PatternIds.Item>>();

            public List<PatternIds.Item> this[Neighbor neighbor] => possibilities[neighbor];

            public Rule()
            {
                possibilities.Add(Neighbor.Up, new List<PatternIds.Item>());
                possibilities.Add(Neighbor.Down, new List<PatternIds.Item>());
                possibilities.Add(Neighbor.Left, new List<PatternIds.Item>());
                possibilities.Add(Neighbor.Right, new List<PatternIds.Item>());
            }
        }
    }
}