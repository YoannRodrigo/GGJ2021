
using System.Collections.Generic;
using UnityEngine;
namespace mechanism
{
    public class Switch : Mechanism
    {

        [SerializeField] private List<Mechanism> _mechanisms = default;

        public override void ActivateMechanism()
        {
            base.ActivateMechanism();
            if (_mechanisms != null && _mechanisms.Count > 0)
            {
                ActivateMechanisms();
            }
        }
        private void ActivateMechanisms()
        {
            foreach (Mechanism mechanism in _mechanisms)
            {
                mechanism.ActivateMechanism();
            }
        }
    }
}