
using System;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Mechanism
{

    [SerializeField] private List<Mechanism> _mechanisms = default;
    [SerializeField] private List<Switch> othersSwitch;
    [SerializeField] private GroundTile currentTile;
    [SerializeField] private GameObject lever;
    [SerializeField] private GameObject thisLight;
    [SerializeField] private bool isActive;
    private Animator leverAnim;
    private static readonly int ACTIVATE = Animator.StringToHash("Activate");

    private void Start()
    {
        if (lever)
        {
            leverAnim = lever.GetComponent<Animator>();
        }
    }

    private void Activate()
    {
        isActive = true;
    }
    
    private void Update()
    {
        if (isActive && lever)
        {
            thisLight.SetActive(true);
            currentTile.ForceEnlighten();
        }
    }

    public override void ActivateMechanism()
    {
        base.ActivateMechanism();
        ActivateMechanisms();
        foreach (Switch sSwitch in othersSwitch)
        {
            sSwitch.Activate();
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            leverAnim.SetTrigger(ACTIVATE);
            ActivateMechanism();
        }
    }
}