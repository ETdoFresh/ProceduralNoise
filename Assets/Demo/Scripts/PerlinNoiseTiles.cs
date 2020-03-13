using UnityEngine;
using UnityEngine.Tilemaps;

namespace Demo.Scripts
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
            var size = tilemap.size;
            width = size.x;
            height = size.y;

            for (int x = 0; x < tilemap.size.x; x++)
                for (int y = 0; y < tilemap.size.y; y++)
                    tilemap.SetColor(new Vector3Int(x, y, 0), GetColor(x/(float)width, y/(float)height));
        }

        public Color GetColor(float x, float y)
        {
            var value = PerlinNoise.Get(x, y, 10);
            return new Color(value, value, value);
        }
    }
}