
using System.Collections.Generic;
using UnityEngine;

public class Switch : Mechanism
{

    [SerializeField] private List<Mechanism> _mechanisms = default;

    public override void ActivateMechanism()
    {
        base.ActivateMechanism();
        ActivateMechanisms();
    }

    public override void DeactivateMechanism()
    {
        base.DeactivateMechanism();
        DeactivateMechanisms();
    }
    private void ActivateMechanisms()
    {
        foreach (Mechanism mechanism in _mechanisms)
        {
            mechanism.ActivateMechanism();
        }
    }
    private void DeactivateMechanisms()
    {
        foreach (Mechanism mechanism in _mechanisms)
        {
            mechanism.DeactivateMechanism();
        }
    }
}