using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFade : MonoBehaviour
{
    public Action EndAnimationAction;
    public static BlackFade instance;
    [SerializeField] private Animator _animator;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void ActivateTrigger()
    {
        _animator.SetTrigger("Fade");
    }

    public void CallActionOnEnd(){
        EndAnimationAction?.Invoke();
        EndAnimationAction = null;
    }
}
