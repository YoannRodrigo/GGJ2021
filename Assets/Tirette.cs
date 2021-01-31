using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Tirette : MonoBehaviour
{
    [SerializeField] ResetLevelSystem reset;
    public void Reset(){
        transform.DOMoveY(transform.position.y - 100,2f).OnComplete(() => 
            reset.ResetToOriginalState()
        );
    }
}
