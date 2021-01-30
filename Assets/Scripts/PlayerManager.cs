using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GroundTile currentTile;
    [SerializeField] private GroundTile target;
    [SerializeField] private bool canMove;
    [SerializeField] private List<GroundTile> path;
    [SerializeField] private Wisp wisp;
    private Rigidbody thisRigidbody;
    //Cards
    [Space]
    [Header("Cards")]
    public List<Card> playerCards = new List<Card>();


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
            if (wisp != null)
            {
                wisp.SetPosition(tile);
            }
        }
    }

    public void SetPath(List<GroundTile> path)
    {
        this.path = path;
    }

    private void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ActivateSwitchOnTile();
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (path.Count != 0)
        {
            target = path[path.Count - 1];
            if (target.IsActive())
            {
                if (Vector3.Distance(target.transform.position, transform.position) < 0.01f)
                {
                    currentTile = target;
                    path.RemoveAt(path.Count - 1);
                }

                transform.DOMove(target.transform.position, 0.2f).SetEase(Ease.Linear);
            }
            else
            {
                path.Clear();
            }
        }
    }

    private void ActivateSwitchOnTile()
    {
        currentTile.ActivateSwitches();
    }

    private void DeactivateSwitchOnTile()
    {
        currentTile.DeactivateSwitches();
    }

    public void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
