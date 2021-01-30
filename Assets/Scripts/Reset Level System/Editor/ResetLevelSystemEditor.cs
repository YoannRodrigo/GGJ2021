using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResetLevelSystem))]
public class ResetLevelSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Gets Mechanisms in Scene"))
        {
            ResetLevelSystem resetSystem = (ResetLevelSystem)target;
            resetSystem.GetAllMechanisms();
        }
        if (GUILayout.Button("Reset Scene"))
        {
            ResetLevelSystem resetSystem = (ResetLevelSystem)target;
            resetSystem.ResetToOriginalState();
        }
        base.OnInspectorGUI();
    }
}
