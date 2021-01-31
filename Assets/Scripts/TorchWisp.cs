using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchWisp : MonoBehaviour
{
    [SerializeField] private Light thisLight;
    SoundManager _soundManager;

    private void Start(){
        _soundManager = SoundManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Wisp"))
        {
            _soundManager.PlayRandomSound(new string[]{"Candle_1","Candle_2","Candle_3","Candle_4","Candle_5"});
            thisLight.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Wisp"))
        {
            thisLight.gameObject.SetActive(false);
        }
    }
}

