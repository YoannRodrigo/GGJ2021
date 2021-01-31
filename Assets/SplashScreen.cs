using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    private bool isVideoOver;
    private void Update()
    {
        if (!videoPlayer.isPlaying && !isVideoOver)
        {
            isVideoOver = true;
            SceneManager.LoadScene(1);
        }
    }
}
