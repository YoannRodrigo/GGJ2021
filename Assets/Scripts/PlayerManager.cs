using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GroundTile currentTile;
    [SerializeField] private bool canMove;
    private Rigidbody thisRigidbody;

    public void SetTarget(GroundTile target)
    {
        DeactivateSwitchOnTile();
        currentTile = target;
    }

    private void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentTile)
        {
            canMove = true;
        }
        if (canMove)
        {
            Transform target = currentTile.transform;
            Vector3 direction = (target.position - transform.position).normalized;
            transform.DOMove(target.position, 1f).SetEase(Ease.Linear);
            if (Vector3.Distance(target.position, transform.position) < 0.01f)
            {
                ActivateSwitchOnTile();
                canMove = false;
            }
        }
    }
    private void ActivateSwitchOnTile()
    {
        currentTile.ActivateSwitch();
    }
    private void DeactivateSwitchOnTile()
    {
        currentTile.DeactivateSwitch();
    }
}
