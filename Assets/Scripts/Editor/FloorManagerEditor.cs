using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloorManager))]
[CanEditMultipleObjects]
public class FloorManagerEditor : Editor
{
    private FloorManager floor;
#if UNITY_EDITOR
    public override void OnInspectorGUI()
    {
        floor = (FloorManager) target;
        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("Find All Tiles"))
        {
            FindAllTile();
        }
        base.OnInspectorGUI();
    }

    private void FindAllTile()
    {
        foreach (GroundTile groundTile in FindObjectsOfType<GroundTile>())
        {
            floor.AddATile(groundTile);
        }
    }
    
#endif
}
