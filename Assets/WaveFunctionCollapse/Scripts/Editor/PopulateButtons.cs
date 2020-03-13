using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviourWithPopulateButton), true)]
public class PopulateButtons : Editor
{
    public static readonly object[] NO_ARGS = new object[] { };

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(EditorGUIUtility.singleLineHeight);

        if (GUILayout.Button("Populate"))
            target.GetType().GetMethod("Populate").Invoke(target, NO_ARGS);

        GUILayout.Space(EditorGUIUtility.singleLineHeight);
    }
}