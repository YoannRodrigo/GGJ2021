
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    
    private static readonly int ROTATION = Shader.PropertyToID("_Rotation");

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update ()
    {
        RenderSettings.skybox.SetFloat(ROTATION, Time.time);
        //To set the speed, just multiply Time.time with whatever amount you want.
    }
}
