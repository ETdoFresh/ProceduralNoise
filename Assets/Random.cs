public static class Random
{
    public static float Get(float min = 0, float max = 1)
    {
        return Range(min, max);
    }

    public static float Range(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}