using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse.Scripts
{
    public class PatternIds : MonoBehaviourWithPopulateButton
    {
        public int n = 1;
        public Tilemap patternTilemap;
        public TileIds tileIds;
        public List<Item> all = new List<Item>();
        public Grid<Item> grid = new Grid<Item>();

        private void OnValidate()
        {
            if (!patternTilemap) patternTilemap = FindObjectOfType<Tilemap>();
            if (!tileIds) tileIds = FindObjectOfType<TileIds>();
        }

        public override void Populate()
        {
            ClearTilemap(patternTilemap);

            var width = tileIds.width - (n - 1);
            var height = tileIds.height - (n - 1);

            all.Clear();
            var tiles = tileIds.all;
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                List<TileIds.Item> sequence = GetNSequence(x, y);
                if (!all.Any(existing => sequence.SequenceEqual(existing.sequence)))
                    all.Add(new Item {id = all.Count, sequence = sequence, tile = tiles[all.Count].tile});
            }

            grid.Reset(width, height);
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                List<TileIds.Item> sequence = GetNSequence(x, y);
                var item = all.Where(i => sequence.SequenceEqual(i.sequence)).First();
                grid.SetValue(item, x, y);
            }

            var cell = patternTilemap.cellBounds;
            cell.min = Vector3Int.zero;
            cell.size = new Vector3Int(width, height, 1);
            patternTilemap.size = cell.size;

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                patternTilemap.SetTile(new Vector3Int(x, y, 0), grid[x, y].value.tile);
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

        private List<TileIds.Item> GetNSequence(int x, int y)
        {
            var tileIdsGrid = tileIds.grid;
            var sequence = new List<TileIds.Item>();
            for (int nY = 0; nY < n; nY++)
            for (int nX = 0; nX < n; nX++)
                sequence.Add(tileIdsGrid[x + nX, y + nY].value);
            return sequence;
        }

        [Serializable]
        public class Item
        {
            public int id;
            public List<TileIds.Item> sequence = new List<TileIds.Item>();
            public TileBase tile;
        }
    }
}