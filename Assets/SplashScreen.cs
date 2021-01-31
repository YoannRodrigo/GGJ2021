﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Canvas canvas;
    private bool isVideoOver;
    private void Update()
    {
        if (!videoPlayer.isPlaying && !isVideoOver)
        {
            isVideoOver = true;
            canvas.gameObject.SetActive(true);
        }

        if (isVideoOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
        }
    }
}
