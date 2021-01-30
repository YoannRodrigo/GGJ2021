using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GroundTile currentTile;
    [SerializeField] private GroundTile target;
    [SerializeField] private bool canMove;
    [SerializeField] public List<GroundTile> path;
    [SerializeField] private Wisp wisp;
    private Rigidbody thisRigidbody;
    [SerializeField] private Animator animator;
    public event Action<GroundTile> InitCurrentTile;
    //Cards
    [Space]
    [Header("Cards")]
    public List<Card> playerCards = new List<Card>();

    private float speed;

    private static readonly int SPEED = Animator.StringToHash("Speed");


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
            InitCurrentTile?.Invoke(currentTile);
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
        //ActivateSwitchOnTile();
        MovePlayer();
        animator.SetFloat(SPEED,speed);
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

                transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
                speed = Mathf.Lerp(speed, 1, 5f * Time.deltaTime);
                print(speed);
                transform.DOMove(target.transform.position, 0.2f).SetEase(Ease.Linear);
            }
            else
            {
                
                path.Clear();
            }
        }
        else
        {
            speed = Mathf.Lerp(speed, 0, 5f * Time.deltaTime);
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
        int buildIndex = SceneManager.sceneCountInBuildSettings;
        if(SceneManager.GetActiveScene().buildIndex + 1 < buildIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        { 
            Application.Quit();
        }
    }

    public Wisp GetWisp()
    {
        return wisp;
    }
}
