using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileIds : MonoBehaviour
{
    public Tilemap inputTilemap;
    public List<Item> all = new List<Item>();
    public List<Item> grid = new List<Item>();
    public int gridWidth;
    public int gridHeight;

    private void OnValidate()
    {
        if (!inputTilemap) inputTilemap = FindObjectOfType<Tilemap>();
    }

    private void Populate()
    {
        RefreshCellBounds();

        all.Clear();
        foreach (var tile in inputTilemap.GetTilesBlock(inputTilemap.cellBounds).OrderBy(t => t.name))
            if (!all.Any(existing => existing.tile == tile))
                all.Add(new Item { id = all.Count, tile = tile });

        grid.Clear();
        foreach (var tile in inputTilemap.GetTilesBlock(inputTilemap.cellBounds))
        {
            var item = all.Where(i => i.tile == tile).First();
            grid.Add(item);
        }
    }

    private void RefreshCellBounds()
    {
        gridWidth = inputTilemap.size.x;
        gridHeight = inputTilemap.size.y;
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
        gridWidth = cell.size.x;
        gridHeight = cell.size.y;
    }

    [Serializable]
    public class Item
    {
        public int id;
        public TileBase tile;

        public override bool Equals(object obj)
        {
            if (obj is Item other)
                return other.id == id && other.tile == tile;
            else
                return false;
        }
    }

    [UnityEditor.CustomEditor(typeof(TileIds))]
    public class TileIdsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(UnityEditor.EditorGUIUtility.singleLineHeight);
            if (GUILayout.Button("Populate"))
                ((TileIds)target).Populate();
            GUILayout.Space(UnityEditor.EditorGUIUtility.singleLineHeight);
        }
    }
}
