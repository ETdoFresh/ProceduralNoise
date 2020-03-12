using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinEntropy : MonoBehaviour
{
    public OutputGrid outputGrid;
    public Entropies entropies;
    public Entropies.Item minCell;
    public float entropy;
    public int i;
    public int x;
    public int y;

    private void OnValidate()
    {
        if (!outputGrid) outputGrid = FindObjectOfType<OutputGrid>();
        if (!entropies) entropies = FindObjectOfType<Entropies>();
    }

    private void Populate()
    {
        entropy = entropies.all.Where(e => e.gridCell.possible.Count > 1).Min(e => e.entropy);
        minCell = entropies.all.Where(e => e.entropy == entropy).First();
        i = entropies.all.IndexOf(minCell);
        x = i % outputGrid.width;
        y = i / outputGrid.height;
    }

    [UnityEditor.CustomEditor(typeof(MinEntropy))]
    public class MinEntropyEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(UnityEditor.EditorGUIUtility.singleLineHeight);
            if (GUILayout.Button("Populate"))
                ((MinEntropy)target).Populate();
            GUILayout.Space(UnityEditor.EditorGUIUtility.singleLineHeight);
        }
    }
}
