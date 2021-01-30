using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GroundTile currentTile;
    [SerializeField] private bool canMove;
    [SerializeField] private List<GroundTile> path;
    private Rigidbody thisRigidbody;

    public void SetTarget(GroundTile target)
    {
        DeactivateSwitchOnTile();
        currentTile = target;
    }

    public void InitPlayerTile(GroundTile tile)
    {
        if (currentTile == null)
        {
            currentTile = tile;
        }
    }

    public void SetPath(List<GroundTile> path)
    {
        this.path = new List<GroundTile>(path);
    }
    //Cards
    [Space]
    [Header("Cards")]
    public List<Card> playerCards = new List<Card>();

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

                if (path.Count != 0)
                {
                    target = path[path.Count - 1].transform;
                    if (Vector3.Distance(target.position, transform.position) < 0.01f)
                    {
                        path.RemoveAt(path.Count - 1);
                    }
                    transform.DOMove(target.position, 0.2f).SetEase(Ease.Linear);

                }
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
