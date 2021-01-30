using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mechanism
{
    public class MechanismLight : Mechanism
    {

        [SerializeField] private Light _light = default;

        public override void ActivateMechanism()
        {
            base.ActivateMechanism();
            _light.gameObject.SetActive(true);
        }

        public override void DeactivateMechanism()
        {
            base.DeactivateMechanism();
            _light.gameObject.SetActive(false);
        }
    }
}