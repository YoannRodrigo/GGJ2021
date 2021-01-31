using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResetLevelSystem))]
public class ResetLevelSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Gets resetables elements"))
        {
            ResetLevelSystem resetSystem = (ResetLevelSystem)target;
            resetSystem.GetResetables();

        }
        if (GUILayout.Button("Reset Scene"))
        {
            ResetLevelSystem resetSystem = (ResetLevelSystem)target;
            resetSystem.ResetToOriginalState();
        }
        base.OnInspectorGUI();
    }
}
