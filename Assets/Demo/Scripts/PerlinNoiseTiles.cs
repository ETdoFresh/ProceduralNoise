﻿using UnityEngine;
using UnityEngine.Tilemaps;

namespace ETdoFresh.PerlinNoise.Demo
{
    [RequireComponent(typeof(Tilemap))]
    public class PerlinNoiseTiles : MonoBehaviour
    {
        public Tilemap tilemap;
        public int width;
        public int height;

        private void OnEnable()
        {
            if (!tilemap) tilemap = GetComponent<Tilemap>();
            width = tilemap.size.x;
            height = tilemap.size.y;

            for (int x = 0; x < tilemap.size.x; x++)
                for (int y = 0; y < tilemap.size.y; y++)
                    tilemap.SetColor(new Vector3Int(x, y, 0), GetColor(x/200f, y/200f));
        }

        public Color GetColor(float x, float y)
        {
            var value = PerlinNoise.Get(x, y);
            return new Color(value, value, value);
        }
    }
}