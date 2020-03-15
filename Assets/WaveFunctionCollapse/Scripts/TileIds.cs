using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse.Scripts
{
    public class TileIds : MonoBehaviourWithPopulateButton
    {
        public Tilemap inputTilemap;
        public List<Item> all = new List<Item>();
        public TileGrid grid = new TileGrid();

        private void OnValidate()
        {
            if (!inputTilemap) inputTilemap = FindObjectOfType<Tilemap>();
        }

        public override void Populate()
        {
            RefreshCellBounds();

            // all.Clear();
            // foreach (var tile in inputTilemap.GetTilesBlock(inputTilemap.cellBounds).OrderBy(t => t.name))
            //     if (all.All(existing => existing.tile != tile))
            //         all.Add(new Item { id = all.Count, tile = tile });

            var size = inputTilemap.cellBounds.size;
            grid.Reset(size.x, size.y);
            var min = inputTilemap.cellBounds.min;
            for (var y = 0; y < grid.height; y++)
            for (var x = 0; x < grid.width; x++)
            {
                var tile = inputTilemap.GetTile(min + new Vector3Int(x, y, 0));
                var item = all.First(i => i.tile == tile);
                grid[x, y].value = item;
            }
        }

        private void RefreshCellBounds()
        {
            var notSet = new Vector3Int(-1, -1, -1);
            var topLeft = notSet;
            var bottomRight = notSet;
            var cell = inputTilemap.cellBounds;

            // Reset cells
            for (int y = cell.yMin; y < cell.yMax; y++)
            for (int x = cell.xMin; x < cell.xMax; x++)
                if (inputTilemap.GetTile(new Vector3Int(x, y, 0)))
                {
                    if (topLeft == notSet)
                    {
                        topLeft = new Vector3Int(x, y, 0);
                        bottomRight = new Vector3Int(x, y, 1);
                    }

                    if (bottomRight.x < x + 1)
                        bottomRight.x = x + 1;
                    if (bottomRight.y < y + 1)
                        bottomRight.y = y + 1;
                }

            cell.min = topLeft;
            cell.max = bottomRight;
            inputTilemap.size = cell.size;
        }
        
        [Serializable] public class TileGrid : Grid<Item, TileCell> { }
        [Serializable] public class TileCell : Cell<Item, TileCell> { }
        
        [Serializable]
        public class Item
        {
            public int id;
            public TileBase tile;

            private int Hash => id.GetHashCode();
            public override int GetHashCode() => Hash;

            public override bool Equals(object obj)
            {
                if (obj is Item other)
                    return other.id == id && other.tile == tile;
                else
                    return false;
            }
        }
    }
}
