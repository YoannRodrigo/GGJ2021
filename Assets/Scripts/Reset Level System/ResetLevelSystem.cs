using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevelSystem : MonoBehaviour
{
    [SerializeField] private Mechanism[] _mechanisms = default;
    [SerializeField] private Mechanism[] _originalStateMechanism = default;
    public void GetAllMechanisms(){
        _mechanisms = Resources.FindObjectsOfTypeAll(typeof(Mechanism)) as Mechanism[];
    }
    public void ResetMechanismsToOriginalState(){
        foreach(Mechanism mechanism in _mechanisms){
            mechanism.Reset();
        }
    }
}
