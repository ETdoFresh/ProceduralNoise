using UnityEngine;
using UnityEngine.Tilemaps;

namespace SimpleRPGOverworld.Scripts
{
    public class GenerateRandomOverworld : MonoBehaviour, ISystem
    {
        private void Update()
        {
            foreach (var entity in Ecs.GetEntities<RandomOverworld>())
            {
                var randomOverworld = entity.Item1;
                var tilemap = randomOverworld.tilemap;
                var playerPrefab = randomOverworld.playerPrefab;
                var frequency = randomOverworld.frequency;

                if (randomOverworld.regenrateNow == false)
                    continue;

                randomOverworld.regenrateNow = false;
                var size = tilemap.size;
                var width = randomOverworld.width = size.x;
                var height = randomOverworld.height = size.y;

                // Reset tilemap
                for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    tilemap.SetColor(new Vector3Int(x, y, 0), Color.white);

                // Generate World
                for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    var value = PerlinNoise.Get(x / (float)randomOverworld.width, y / (float)randomOverworld.height, frequency);
                    if (value < 0.8f)
                    {
                        var position = new Vector3Int(x, y, 0);
                        var randomTile = randomOverworld.grassTiles[UnityEngine.Random.Range(0, randomOverworld.grassTiles.Count)];
                        tilemap.SetTile(position, randomTile);
                    }
                    else
                    {
                        var position = new Vector3Int(x, y, 0);
                        var randomTile = randomOverworld.mountainTiles[UnityEngine.Random.Range(0, randomOverworld.mountainTiles.Count)];
                        tilemap.SetTile(position, randomTile);
                    }
                }

                // Place Character
                GameObject player = null;
                while (!player)
                {
                    var x = UnityEngine.Random.Range(0, width);
                    var y = UnityEngine.Random.Range(0, height);
                    var position = new Vector3Int(x, y, 0);
                    var tile = (Tile)tilemap.GetTile(position);
                    if (tile.sprite.name.Contains("Grass"))
                        player = Instantiate(playerPrefab, tilemap.CellToWorld(position), Quaternion.identity);
                }
            }
        }
    }
}
