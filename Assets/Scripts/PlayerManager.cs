using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool canMove;
    [SerializeField] private List<GroundTile> path;
    private Rigidbody thisRigidbody;

    public void SetPath(List<GroundTile> path)
    {
        this.path = new List<GroundTile>(path);
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

        if (path.Count!=0)
        {
            target = path[path.Count-1].transform;
            if (Vector3.Distance(target.position, transform.position) < 0.01f)
            {   
                path.RemoveAt(path.Count-1);
            }
            transform.DOMove(target.position, 0.2f).SetEase(Ease.Linear);
            
        }
    }
}
