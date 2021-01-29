public class AkXBoxOneSettings : AkWwiseInitializationSettings.PlatformSettings
{
#if UNITY_EDITOR
	[UnityEditor.InitializeOnLoadMethod]
	private static void AutomaticPlatformRegistration()
	{
		RegisterPlatformSettingsClass<AkXBoxOneSettings>("XboxOne");
	}
#endif // UNITY_EDITOR

	public AkXBoxOneSettings()
	{
		IgnorePropertyValue("UserSettings.m_SampleRate");
		SetUseGlobalPropertyValue("CommsSettings.m_CommandPort", false);
		SetUseGlobalPropertyValue("CommsSettings.m_NotificationPort", false);
		SetUseGlobalPropertyValue("AdvancedSettings.m_RenderDuringFocusLoss", false);
	}

	protected override AkCommonUserSettings GetUserSettings()
	{
		return UserSettings;
	}

	protected override AkCommonAdvancedSettings GetAdvancedSettings()
	{
		return AdvancedSettings;
	}

	protected override AkCommonCommSettings GetCommsSettings()
	{
		return CommsSettings;
	}

	[System.Serializable]
	public class PlatformAdvancedSettings : AkCommonAdvancedSettings
	{
		[UnityEngine.Tooltip("Maximum number of hardware-accelerated XMA voices used at run-time. Default is 128 voices.")]
		public ushort MaximumNumberOfXMAVoices = 128;

		[UnityEngine.Tooltip("Use low latency mode for hardware XMA decoding (default is false). If true, decoding jobs are submitted at the beginning of the Wwise update and it will be necessary to wait for the result.")]
		public bool UseHardwareCodecLowLatencyMode;

		[UnityEngine.Tooltip("APU heap cached size sent to the \"ApuCreateHeap()\" function.")]
		public uint APUHeapCachedSize = 64 * 1024 * 1024;

		[UnityEngine.Tooltip("APU heap non-cached size sent to the \"ApuCreateHeap()\" function.")]
		public uint APUHeapNonCachedSize = 0;

		public override void CopyTo(AkPlatformInitSettings settings)
		{
#if (UNITY_XBOXONE || UNITY_GAMECORE_XBOXONE) && !UNITY_EDITOR
			settings.uMaxXMAVoices = MaximumNumberOfXMAVoices;
			settings.bHwCodecLowLatencyMode = UseHardwareCodecLowLatencyMode;
#endif
		}

		public override void CopyTo(AkUnityPlatformSpecificSettings settings)
		{
#if (UNITY_XBOXONE || UNITY_GAMECORE_XBOXONE) && !UNITY_EDITOR
			settings.ApuHeapCachedSize = APUHeapCachedSize;
			settings.ApuHeapNonCachedSize = APUHeapNonCachedSize;
#endif
		}
	}

	[UnityEngine.HideInInspector]
	public AkCommonUserSettings UserSettings = new AkCommonUserSettings { m_SamplesPerFrame = 512, };

	[UnityEngine.HideInInspector]
	public PlatformAdvancedSettings AdvancedSettings = new PlatformAdvancedSettings
	{
		m_RenderDuringFocusLoss = true,
	};

	[UnityEngine.HideInInspector]
	public AkCommonCommSettings CommsSettings = new AkCommonCommSettings
	{
		m_DiscoveryBroadcastPort = AkCommonCommSettings.DefaultDiscoveryBroadcastPort,
		m_CommandPort = (ushort)(AkCommonCommSettings.DefaultDiscoveryBroadcastPort + 1),
		m_NotificationPort = (ushort)(AkCommonCommSettings.DefaultDiscoveryBroadcastPort + 2),
	};
}
