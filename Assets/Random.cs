using UnityEngine;

namespace ETdoFresh.PerlinNoise
{
    public static class Random
    {
        public static float Get()
        {
            return Get(0, 1);
        }

        public static float Get(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}
