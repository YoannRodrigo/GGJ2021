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
            foreach (Transform componentsInChild in floor.GetComponentsInChildren<Transform>())
            {
                if(!componentsInChild.GetComponent<FloorGenerator>())
                {
                    DestroyImmediate(componentsInChild.gameObject);
                }
            }
        }
        base.OnInspectorGUI();
    }
    
    
#endif
}
