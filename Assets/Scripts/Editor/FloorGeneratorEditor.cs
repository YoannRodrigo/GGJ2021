using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloorGenerator))]
[CanEditMultipleObjects]
public class FloorGeneratorEditor : Editor
{
    private FloorGenerator floor;
#if UNITY_EDITOR
    public override void OnInspectorGUI()
    {
        floor = (FloorGenerator) target;
        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("GenerateFloor"))
        {
           floor.GenerateFloor();
        }
        
        if (GUILayout.Button("Destroy Floor"))
        {
            foreach (GroundTile componentsInChild in floor.GetComponentsInChildren<GroundTile>())
            {
                if(componentsInChild.GetComponent<GroundTile>())
                {
                    DestroyImmediate(componentsInChild.gameObject);
                }
            }
        }
        base.OnInspectorGUI();
    }
    
    
#endif
}
