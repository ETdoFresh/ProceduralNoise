using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entropies : MonoBehaviour
{
    public RelativeFrequency relativeFrequency;
    public OutputGrid outputGrid;
    public List<Item> all = new List<Item>();

    private void OnValidate()
    {
        if (!relativeFrequency) relativeFrequency = FindObjectOfType<RelativeFrequency>();
        if (!outputGrid) outputGrid = FindObjectOfType<OutputGrid>();
    }

    private void Populate()
    {
        all.Clear();
        foreach(var cell in outputGrid.grid)
        {
            if (!all.Any(i => cell.possible.SequenceEqual(i.possible)))
                all.Add(new Item { possible = new List<PatternIds.Item>(cell.possible), entropy = ComputeEntropy(cell.possible) });
        }
    }

    private float ComputeEntropy(List<PatternIds.Item> possible)
    {
        var sumOfAllWeights = 0f;
        for (int i = 0; i < possible.Count; i++)
            sumOfAllWeights += relativeFrequency.frequencies[possible[i].id];

        var sumOfLog2OfEachWeight = 0f;
        for (int i = 0; i < possible.Count; i++)
            sumOfLog2OfEachWeight += Mathf.Log(relativeFrequency.frequencies[possible[i].id], 2);

        var log2OfSumOfAllWeights = Mathf.Log(sumOfAllWeights, 2);

        var smallRandomValue = Random.Range(0f, 0.001f);
        return -log2OfSumOfAllWeights - sumOfLog2OfEachWeight / sumOfAllWeights + smallRandomValue;
    }

    [Serializable]
    public class Item
    {
        public List<PatternIds.Item> possible;
        public float entropy;
    }

    [UnityEditor.CustomEditor(typeof(Entropies))]
    public class EntropiesEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(UnityEditor.EditorGUIUtility.singleLineHeight);
            if (GUILayout.Button("Populate"))
                ((Entropies)target).Populate();
            GUILayout.Space(UnityEditor.EditorGUIUtility.singleLineHeight);
        }
    }
}
