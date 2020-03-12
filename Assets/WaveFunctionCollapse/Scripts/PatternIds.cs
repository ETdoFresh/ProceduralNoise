﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PatternIds : MonoBehaviour
{
    public int n = 1;
    public Tilemap patternTilemap;
    public TileIds tileIds;
    public List<Item> all = new List<Item>();
    public List<Item> grid = new List<Item>();

    private void OnValidate()
    {
        if (!patternTilemap) patternTilemap = FindObjectOfType<Tilemap>();
        if (!tileIds) tileIds = FindObjectOfType<TileIds>();
    }

    private void Populate()
    {
        ClearTilemap(patternTilemap);

        all.Clear();
        var tiles = tileIds.all;
        for (var y = 0; y < tileIds.gridHeight - (n - 1); y++)
            for (var x = 0; x < tileIds.gridWidth - (n - 1); x++)
            {
                List<TileIds.Item> sequence = GetNSequence(x, y);
                if (!all.Any(existing => sequence.SequenceEqual(existing.sequence)))
                    all.Add(new Item { id = all.Count, sequence = sequence, tile = tiles[all.Count].tile });
            }

        grid.Clear();
        for (var y = 0; y < tileIds.gridHeight - (n - 1); y++)
            for (var x = 0; x < tileIds.gridWidth - (n - 1); x++)
            {
                List<TileIds.Item> sequence = GetNSequence(x, y);
                var item = all.Where(i => sequence.SequenceEqual(i.sequence)).First();
                grid.Add(item);
            }

        var cell = patternTilemap.cellBounds;
        var width = tileIds.gridWidth;
        var height = tileIds.gridHeight;
        cell.min = Vector3Int.zero;
        cell.size = new Vector3Int(width - (n - 1), height - (n - 1), 1);
        patternTilemap.size = cell.size;

        for (int i = 0; i < grid.Count; i++)
            patternTilemap.SetTile(new Vector3Int(i % cell.size.x, i / cell.size.x, 0), grid[i].tile);
    }

    private void ClearTilemap(Tilemap tilemap)
    {
        var min = tilemap.cellBounds.min;
        var max = tilemap.cellBounds.max;
        for (var y = min.y; y < max.y; y++)
            for (var x = min.x; x < max.x; x++)
                tilemap.SetTile(new Vector3Int(x, y, 0), null);
    }

    private List<TileIds.Item> GetNSequence(int x, int y)
    {
        var grid = tileIds.grid;
        var width = tileIds.gridWidth;
        var sequence = new List<TileIds.Item>();
        for (int nY = 0; nY < n; nY++)
            for (int nX = 0; nX < n; nX++)
                sequence.Add(grid[x + nX + (y + nY) * width]);
        return sequence;
    }

    [Serializable]
    public class Item
    {
        public int id;
        public List<TileIds.Item> sequence = new List<TileIds.Item>();
        public TileBase tile;
    }


    [UnityEditor.CustomEditor(typeof(PatternIds))]
    public class PatternIdsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(UnityEditor.EditorGUIUtility.singleLineHeight);
            if (GUILayout.Button("Populate"))
                ((PatternIds)target).Populate();
            GUILayout.Space(UnityEditor.EditorGUIUtility.singleLineHeight);
        }
    }
}