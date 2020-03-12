public static class Range
{
    public static float Map(float value, float minValue, float maxValue, float newMinValue, float newMaxValue)
    {
        var range = maxValue - minValue;
        var normalized = (value - minValue) / range;
        var newRange = newMaxValue - newMinValue;
        return normalized * newRange + newMinValue;
    }
}
