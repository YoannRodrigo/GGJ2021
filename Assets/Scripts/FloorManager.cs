using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private List<GroundTile> grounds;
    [SerializeField] private GroundTile selectedTile;
    [SerializeField] private List<Vector3> tilesPosition = new List<Vector3>();
    [SerializeField] private List<GroundTile> path;
    [SerializeField] private Wisp wisp;
    private bool isTileSelected;
    [SerializeField] private GroundTile playerTile;
    private Camera mainCamera;
    private GroundTile lastSelectedTile;
    private bool isPlayerMoving;
    [SerializeField] private List<GroundTile> wispGroundTile = new List<GroundTile>();
    private bool isWispMoving;
    private GroundTile wispTile;
    public int size {get;set;}

    public void SetPlayerTile(GroundTile playerTile)
    {
        this.playerTile = playerTile;
        playerManager.InitPlayerTile(playerTile);
    }

    public void AddATile(GroundTile tile)
    {
        if (!grounds.Contains(tile))
        {
            grounds.Add(tile);
            tilesPosition.Add(tile.transform.position);
        }
        tile.SetFloorManager(this);
    }

    public bool IsTileSelected()
    {
        return isTileSelected;
    }

    private void TileSelect(GroundTile selectedTile)
    {
        isTileSelected = true;
        this.selectedTile = selectedTile;
        ResetPathColor();
        PrintShortestDistance(playerTile.GetAllNeighbors(), playerTile, selectedTile, grounds.Count,size);
    }

    public void ResetPathColor()
    {
        foreach (GroundTile groundTile in path)
        {
            groundTile.SetDefaultColor();
        }
        path.Clear();
    }

    private void ValidatePath(List<GroundTile> path)
    {
        if(path.Count!=0)
        {
            playerManager.SetPath(path);
        }
    }
    public void InitTiles()
    {
        foreach (GroundTile tile in grounds)
        {
            tile.FindNeighbors();
        }
    }

    public GroundTile GetTileByPosition(Vector3 position)
    {
        int index = tilesPosition.IndexOf(position);
        return index >= 0 && index < grounds.Count ? grounds[index] : null;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(path.Count!=0)
            {
                if(!isPlayerMoving)
                {
                    isPlayerMoving = true;
                    ValidatePath(path);
                }
            }
            else if (wispGroundTile.Count != 0)
            {
                if(!isWispMoving)
                {
                    isWispMoving = true;
                    foreach (GroundTile groundTile in wispGroundTile)
                    {
                        groundTile.SetDefaultColor();
                    }

                    ValidateWispTarget(selectedTile);
                }
            }
        }

        if (wispTile && Vector3.Distance(Vector3.Scale(wisp.transform.position, new Vector3(1, 0, 1)), wispTile.transform.position) < 0.01f)
        {
            wispTile.StopParticle();
            isWispMoving = false;
            wispGroundTile.Clear();
        }
        
        if (path.Count == 0)
        {
            isPlayerMoving = false;
        }
        foreach (GroundTile groundTile in path)
        {
            groundTile.SetPreSelectedColor();
        }
    }

    private void ValidateWispTarget(GroundTile tile)
    {
        if(tile)
        {
            wispTile = tile;
            tile.StopParticle();
            wisp.SetTargetTile(tile);
        }
    }

    private void PrintShortestDistance(List<GroundTile> adj, GroundTile start, GroundTile dest, int v, int size)
    {
        GroundTile[] pred = new GroundTile[v];
        int []dist = new int[v];
 
        if (Bfs( start, dest, v, pred, dist) == false)
        {
            Console.WriteLine("Given source and destination" + 
                              "are not connected");
            return;
        }
 
        List<GroundTile> path = new List<GroundTile>();
        GroundTile crawl = dest;
        path.Add(crawl);
   
        while (pred[crawl.GetId()] != null) 
        {
            path.Add(pred[crawl.GetId()]);
            crawl = pred[crawl.GetId()];
        }

        if(path.Count == size)
        {
            this.path = path;
        }
        
    }
    
    private bool Bfs(GroundTile start, GroundTile dest, int v, GroundTile[] previousTile, int []dist)
    {
        List<GroundTile> queue = new List<GroundTile>();
 
        bool []visited = new bool[v];
 
        for (int i = 0; i < v; i++) 
        {
            visited[i] = false;
            dist[i] = int.MaxValue;
            previousTile[i] = null;
        }
        visited[start.GetId()] = true;
        dist[start.GetId()] = 0;
        queue.Add(start);
 
        while (queue.Count != 0) 
        {
            GroundTile u = queue[0];
            queue.RemoveAt(0);
            
            for (int i = 0; i < u.GetAllNeighbors().Count; i++) 
            {
                GroundTile currentNeighbor = u.GetAllNeighbors()[i];
                if (visited[currentNeighbor.GetId()] == false)
                {
                    visited[currentNeighbor.GetId()] = true;
                    dist[currentNeighbor.GetId()] = dist[u.GetId()] + 1;
                    previousTile[currentNeighbor.GetId()] = u;
                    queue.Add(currentNeighbor);
                    if (currentNeighbor.GetId() == dest.GetId())
                        return true;
                }
            }
        }
        return false;
    }

    public void ResetList()
    {
        grounds.Clear();
        tilesPosition.Clear();
    }
    public List<GroundTile> GetBfs(GroundTile start, GroundTile dest)
    {
        int v = grounds.Count;
        GroundTile[] previousTile = new GroundTile[v];
        int[] dist = new int[v];
        List<GroundTile> queue = new List<GroundTile>();

        bool[] visited = new bool[v];

        for (int i = 0; i < v; i++)
        {
            visited[i] = false;
            dist[i] = int.MaxValue;
            previousTile[i] = null;
        }
        visited[start.GetId()] = true;
        dist[start.GetId()] = 0;
        queue.Add(start);

        while (queue.Count != 0)
        {
            GroundTile u = queue[0];
            queue.RemoveAt(0);

            for (int i = 0; i < u.GetAllNeighbors().Count; i++)
            {
                GroundTile currentNeighbor = u.GetAllNeighbors()[i];
                if (visited[currentNeighbor.GetId()] == false)
                {
                    visited[currentNeighbor.GetId()] = true;
                    dist[currentNeighbor.GetId()] = dist[u.GetId()] + 1;
                    previousTile[currentNeighbor.GetId()] = u;
                    queue.Add(currentNeighbor);
                    if (currentNeighbor.GetId() == dest.GetId())
                    {
                        List<GroundTile> path = new List<GroundTile>();
                        GroundTile crawl = dest;
                        path.Add(crawl);

                        while (previousTile[crawl.GetId()] != null)
                        {
                            path.Add(previousTile[crawl.GetId()]);
                            crawl = previousTile[crawl.GetId()];
                        }
                        path.Reverse();
                        return path;
                    }
                }
            }
        }
        return null;
    }

    public void ShowPlayerPath(int value)
    {
        ClearWispChoice();
        if(!isPlayerMoving)
        {
            selectedTile = GetHighlightedGroundTile();
            if (!lastSelectedTile)
            {
                lastSelectedTile = selectedTile;
            }

            if (selectedTile)
            {
                if (lastSelectedTile.GetInstanceID() != selectedTile.GetInstanceID())
                {
                    ClearPath();
                    lastSelectedTile.StopParticle();
                    selectedTile.PlayParticle();
                    lastSelectedTile = selectedTile;
                }

                if (path.Count == 0)
                {
                    size = value;
                    TileSelect(selectedTile);
                }
            }
            else
            {
                if (lastSelectedTile)
                {
                    ClearPath();
                    lastSelectedTile.StopParticle();
                }
            }
        }
    }

    public void ShowWispChoices(int value)
    {
        ClearPath();
        if(!isWispMoving)
        {
            Vector3 wispOnGroundPos = Vector3.Scale(wisp.transform.position, new Vector3(1, 0, 1));
            List<Vector3> targetPosition = new List<Vector3>()
            {
                wispOnGroundPos + new Vector3(0, 0, value),
                wispOnGroundPos + new Vector3(value, 0, 0),
                wispOnGroundPos + new Vector3(0, 0, -value),
                wispOnGroundPos + new Vector3(-value, 0, 0),
            };
            GroundTile tile = GetHighlightedGroundTile();
            
            foreach (GroundTile groundTile in targetPosition.Select(GetTileByPosition).Where(groundTile => groundTile))
            {
                if (!wispGroundTile.Contains(groundTile))
                {
                    wispGroundTile.Add(groundTile);
                    groundTile.SetPreSelectedColor();
                }
            }

            if (wispGroundTile.Contains(tile))
            {
                selectedTile = tile;
            }
            else
            {
                selectedTile = null;
            }
            
            if (selectedTile && wispGroundTile.Contains(selectedTile))
            {
                if (!lastSelectedTile)
                {
                    lastSelectedTile = selectedTile;
                }
                if (lastSelectedTile.GetId() != selectedTile.GetId())
                {
                    lastSelectedTile.StopParticle();
                    selectedTile.PlayParticle();
                }
            }
            else
            {
                lastSelectedTile.StopParticle();
            }
        }
    }

    private void ClearPath()
    {
        foreach (GroundTile groundTile in path)
        {
            groundTile.SetDefaultColor();
        }
        path.Clear();
    }

    private void ClearWispChoice()
    {
        foreach (GroundTile groundTile in wispGroundTile)
        {
            groundTile.SetDefaultColor();
        }
        wispGroundTile.Clear();
    }
    
    private GroundTile GetHighlightedGroundTile()
    {
        int layer_mask = LayerMask.GetMask("Floor");
        Ray screenRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, out RaycastHit resultHit, 100, layer_mask))
        {
            return resultHit.collider.transform.GetComponent<GroundTile>();
        }
        return null;
    }
}