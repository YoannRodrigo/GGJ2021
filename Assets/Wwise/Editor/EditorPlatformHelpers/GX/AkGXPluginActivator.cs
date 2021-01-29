#if UNITY_EDITOR
[UnityEditor.InitializeOnLoad]
public class AkGXPluginActivator
{
	static AkGXPluginActivator()
	{
		AkPluginActivator.BuildTargetToPlatformName.Add((UnityEditor.BuildTarget)43 /* GameCoreXboxOne */, "GX");
		AkBuildPreprocessor.BuildTargetToPlatformName.Add((UnityEditor.BuildTarget)43 /* GameCoreXboxOne */, "XboxOne");
	}
}
#endif