﻿using ETdoFresh.PerlinNoise;
using UnityEngine;

public class TestRandomTable : MonoBehaviour
{
    public RandomValueTable randomValueTable = new RandomValueTable();

    private void OnEnable()
    {
        randomValueTable = new RandomValueTable();
        Debug.Log($"Random Value Table: {randomValueTable}");
    }
}
