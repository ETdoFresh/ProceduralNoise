using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SimpleRPGOverworld.Scripts
{
    public class RandomOverworld : MonoBehaviour, IComponentData
    {
        public bool regenrateNow = true;
        public int width;
        public int height;
        public int frequency = 10;
        public GameObject playerPrefab;
        public Tilemap tilemap;
        public List<Tile> grassTiles = new List<Tile>();
        public List<Tile> mountainTiles = new List<Tile>();
    }
}
