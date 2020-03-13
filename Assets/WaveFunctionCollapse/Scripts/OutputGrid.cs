using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OutputGrid : MonoBehaviourWithPopulateButton
{
    public int width = 5;
    public int height = 5;
    public Tilemap outputTilemap;
    public PatternIds patternIds;
    public List<Item> grid = new List<Item>();

    private void OnValidate()
    {
        if (!outputTilemap) outputTilemap = FindObjectOfType<Tilemap>();
        if (!patternIds) patternIds = FindObjectOfType<PatternIds>();
    }

    public  override void Populate()
    {
        var allPatterns = patternIds.all;

        grid.Clear();
        for (var i = 0; i < width * height; i++)
        {
            var newItem = new Item();
            newItem.possible.AddRange(allPatterns);
            grid.Add(newItem);
        }
        RefreshTiles();
    }

    public void RefreshTiles()
    {
        ClearTilemap(outputTilemap);
        for (int i = 0; i < grid.Count; i++)
            outputTilemap.SetTile(new Vector3Int(i % width, i / width, 0), patternIds.tileIds.all[grid[i].possible.Count].tile);
    }

    private void ClearTilemap(Tilemap tilemap)
    {
        var min = tilemap.cellBounds.min;
        var max = tilemap.cellBounds.max;
        for (var y = min.y; y < max.y; y++)
            for (var x = min.x; x < max.x; x++)
                tilemap.SetTile(new Vector3Int(x, y, 0), null);
    }

    [Serializable]
    public class Item
    {
        public List<PatternIds.Item> possible = new List<PatternIds.Item>();
    }
}
