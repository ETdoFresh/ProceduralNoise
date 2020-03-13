using UnityEditor;
using UnityEngine;

namespace WaveFunctionCollapse.Scripts.Editor
{
    [CustomEditor(typeof(MonoBehaviourWithPopulateButton), true)]
    public class PopulateButtons : UnityEditor.Editor
    {
        private static readonly object[] NoArgs = new object[] { };

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(EditorGUIUtility.singleLineHeight);

            if (GUILayout.Button("Populate"))
                target.GetType().GetMethod("Populate")?.Invoke(target, NoArgs);

            GUILayout.Space(EditorGUIUtility.singleLineHeight);
        }
    }
}