﻿using System;
using Demo.Scripts;
using UnityEngine;

public static class PerlinNoise
{
    public static RandomValueTable randomValue = new RandomValueTable();
    public static PermutationTable permutation = new PermutationTable();
    private const double TOLERANCE = 0.0000001;

    public static float Get(float x, float frequency)
    {
        x *= frequency;
        var intPart = Mathf.FloorToInt(x);
        var floatPart = x - intPart;
        var inverse = floatPart - 1;
        var t = Fade(floatPart);
        var num1 = Grad(permutation[intPart], floatPart);
        var num2 = Grad(permutation[intPart + 1], inverse);
        var value = Mathf.Lerp(num1, num2, t);
        return Map(value, -1, 1, 0, 1);
    }

    public static float Get(float x, float y, float frequency)
    {
        // TEMP
        if (Math.Abs(x) < TOLERANCE && Math.Abs(y) < TOLERANCE)
            permutation = new PermutationTable(UnityEngine.Random.Range(int.MinValue, int.MaxValue));

        x *= frequency;
        y *= frequency;
        var intPartX = Mathf.FloorToInt(x);
        var intPartY = Mathf.FloorToInt(y);
        var floatPartX = x - intPartX;
        var floatPartY = y - intPartY;
        var inverseX = floatPartX - 1;
        var inverseY = floatPartY - 1;

        var tX = Fade(floatPartX);
        var tY = Fade(floatPartY);

        var num1 = Grad(permutation[intPartX, intPartY], floatPartX, floatPartY);
        var num2 = Grad(permutation[intPartX, intPartY + 1], floatPartX, inverseY);
        var value1 = Mathf.Lerp(num1, num2, tY);

        var num3 = Grad(permutation[intPartX + 1, intPartY], inverseX, floatPartY);
        var num4 = Grad(permutation[intPartX + 1, intPartY + 1], inverseX, inverseY);
        var value2 = Mathf.Lerp(num3, num4, tY);

        var value = Mathf.Lerp(value1, value2, tX);
        return Map(value, -1, 1, 0, 1);
    }

    private static float Map(float value, float originalMin, float originalMax, float min, float max)
    {
        var delta = (value - originalMin) / (originalMax - originalMin);
        var newDelta = delta * (max - min);
        return newDelta + min;
    }

    private static float Fade(float t)
    {
        return t * t * t * (t * (t * 6.0f - 15.0f) + 10.0f);
    }

    private static float Grad(int hash, float x)
    {
        int h = hash & 15;
        float grad = 1.0f + (h & 7);    // Gradient value 1.0, 2.0, ..., 8.0
        if ((h & 8) != 0) grad = -grad; // Set a random sign for the gradient
        return (grad * x);              // Multiply the gradient with the distance
    }

    private static float Grad(int hash, float x, float y)
    {
        int h = hash & 7;           // Convert low 3 bits of hash code
        float u = h < 4 ? x : y;  // into 8 simple gradient directions,
        float v = h < 4 ? y : x;  // and compute the dot product with (x,y).
        return ((h & 1) != 0 ? -u : u) + ((h & 2) != 0 ? -2.0f * v : 2.0f * v);
    }
}