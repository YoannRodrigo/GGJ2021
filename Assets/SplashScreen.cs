using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Canvas canvas;
    private bool isVideoOver;
    private int isFirstFrame;

    public Sequencer sequencer;
    public GameObject image;

    private void Start(){
        SoundManager.instance.PlaySoundLoop("Music");
    }

    private void Update()
    {
        if(isFirstFrame > 5)
        {
            if (!videoPlayer.isPlaying && !isVideoOver)
            {
                isVideoOver = true;
                canvas.gameObject.SetActive(true);
                sequencer.SQ_BlinkText(image,3);
            }

            if (isVideoOver && Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(1);
            }
        }

        isFirstFrame++;
    }
}
