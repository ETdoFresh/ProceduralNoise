using UnityEngine;
using UnityEngine.Tilemaps;

namespace Demo.Scripts
{
    [RequireComponent(typeof(Tilemap))]
    public class RandomTiles : MonoBehaviour
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
                    tilemap.SetColor(new Vector3Int(x, y, 0), RandomColor());
        }

        public Color RandomColor()
        {
            var value = Random.Get();
            return new Color(value, value, value);
        }
    }
}