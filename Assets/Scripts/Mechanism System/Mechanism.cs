using UnityEngine;

public class Mechanism : MonoBehaviour
{
    [SerializeField] private bool isOn = default;
    public bool IsOn
    {
        get
        {
            return isOn;
        }
        set
        {
            isOn = value;
        }
    }
    private bool _originalState = default;

    private void Awake(){
        _originalState = isOn;
    }
    public virtual void Start(){
        if(IsOn){
            ActivateMechanism();
        }
        else{
            DeactivateMechanism();
        }
    }
    public void Reset(){
        isOn = _originalState;
        if(isOn){
            ActivateMechanism();
            return;
        } 
        DeactivateMechanism();
    }

    public virtual void ActivateMechanism()
    {
        IsOn = true;
    }
    public virtual void DeactivateMechanism(){
        IsOn = false;
    }
}