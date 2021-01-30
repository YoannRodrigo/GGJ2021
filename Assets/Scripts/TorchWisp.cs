using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchWisp : MonoBehaviour
{
    [SerializeField] private Light thisLight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Wisp"))
        {
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

