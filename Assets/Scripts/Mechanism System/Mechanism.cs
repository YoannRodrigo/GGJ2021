using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mechanism
{
    public class Mechanism : MonoBehaviour
    {
        [SerializeField] private bool _isOn = default;
        public bool IsOn
        {
            get
            {
                return _isOn;
            }
            set
            {
                _isOn = value;
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
}