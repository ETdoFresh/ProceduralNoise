using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse.Scripts
{
    public class OutputGridPossibilities : MonoBehaviourWithPopulateButton
    {
        public int width = 5;
        public int height = 5;
        public Tilemap outputTilemap;
        public PatternIds patternIds;
        public OutputGrid grid = new OutputGrid();

        private void OnValidate()
        {
            if (!outputTilemap) outputTilemap = FindObjectOfType<Tilemap>();
            if (!patternIds) patternIds = FindObjectOfType<PatternIds>();
        }

        public override void Populate()
        {
            var allPatterns = patternIds.all;

            grid.Reset(width, height);
            for (var i = 0; i < grid.Count; i++)
            {
                var newItem = new Item();
                newItem.possible.AddRange(allPatterns);
                grid.SetValue(newItem, i);
                ;
            }

            RefreshTiles();
        }

        public void RefreshTiles()
        {
            ClearTilemap(outputTilemap);
            for (var y = 0; y < grid.height; y++)
            for (var x = 0; x < grid.width; x++)
                outputTilemap.SetTile(new Vector3Int(x, y, 0),
                    patternIds.tileIds.all[grid[x, y].value.possible.Count].tile);
        }

        private void ClearTilemap(Tilemap tilemap)
        {
            var cellBounds = tilemap.cellBounds;
            var min = cellBounds.min;
            var max = cellBounds.max;
            for (var y = min.y; y < max.y; y++)
            for (var x = min.x; x < max.x; x++)
                tilemap.SetTile(new Vector3Int(x, y, 0), null);
        }

        [Serializable]
        public class OutputGrid : Grid<Item, OutputCell> { }

        [Serializable]
        public class OutputCell : Cell<Item, OutputCell> { }

        [Serializable]
        public class Item
        {
            public List<PatternIds.Item> possible = new List<PatternIds.Item>();
        }
    }
}