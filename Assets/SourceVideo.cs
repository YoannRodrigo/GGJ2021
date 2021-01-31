using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class SourceVideo : MonoBehaviour
{
    
    private void Awake()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "WISP_SplashScreen.mp4");
    }
}
