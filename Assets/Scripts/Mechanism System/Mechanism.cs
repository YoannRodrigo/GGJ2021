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

    public virtual void ActivateMechanism()
    {
        IsOn = true;
    }
    public virtual void DeactivateMechanism(){
        IsOn = false;
    }
}