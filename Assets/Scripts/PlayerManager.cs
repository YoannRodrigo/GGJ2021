using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool canMove;
    private Rigidbody thisRigidbody;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && target)
        {
            canMove = true;
            
        }

        if (canMove)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.DOMove(target.position, 1f).SetEase(Ease.Linear);
            if (Vector3.Distance(target.position, transform.position) < 0.01f)
            {
                canMove = false;
            }
        }
    }
}
