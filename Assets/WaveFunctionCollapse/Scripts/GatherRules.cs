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
                    {
                        if (!rule.up.Contains(cell.up.value))
                            rule.up.Add(cell.up.value);
                        if (!rule.down.Contains(cell.down.value))
                            rule.down.Add(cell.down.value);
                        if (!rule.left.Contains(cell.left.value))
                            rule.left.Add(cell.left.value);
                        if (!rule.right.Contains(cell.right.value))
                            rule.right.Add(cell.right.value);
                    }
                rules.Add(rule);
            }
        }

        [Serializable]
        public class Rule
        {
            public PatternIds.Item pattern;
            public List<PatternIds.Item> up = new List<PatternIds.Item>();
            public List<PatternIds.Item> down = new List<PatternIds.Item>();
            public List<PatternIds.Item> left = new List<PatternIds.Item>();
            public List<PatternIds.Item> right = new List<PatternIds.Item>();
        }
    }
}
