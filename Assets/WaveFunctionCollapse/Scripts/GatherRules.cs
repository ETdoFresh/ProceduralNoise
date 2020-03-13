using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherRules : MonoBehaviourWithPopulateButton
{
    public PatternIds patternIds;
    public List<Item> rules = new List<Item>();

    private void OnValidate()
    {
        if (!patternIds) patternIds = FindObjectOfType<PatternIds>();
    }

    public override void Populate()
    {
        rules.Clear();
        foreach(var pattern in patternIds.all)
        {
            var newRule = new Item();
            for (var y = 0; y < patternIds.patternTilemap.size.y; y++)
                for (var x = 0; x < patternIds.patternTilemap.size.x; x++)
                {
                    var i = y * patternIds.patternTilemap.size.x + x;

                }
        }
    }

    [Serializable]
    public class Item
    {
        public List<PatternIds.Item> up = new List<PatternIds.Item>();
        public List<PatternIds.Item> down = new List<PatternIds.Item>();
        public List<PatternIds.Item> left = new List<PatternIds.Item>();
        public List<PatternIds.Item> right = new List<PatternIds.Item>();
    }
}
