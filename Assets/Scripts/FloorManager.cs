using System;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private List<GroundTile> grounds;
    [SerializeField] private GroundTile selectedTile;
    [SerializeField] private List<Vector3> tilesPosition = new List<Vector3>();
    [SerializeField] private List<GroundTile> path;
    private bool isTileSelected;
    [SerializeField] private GroundTile playerTile;

    public void SetPlayerTile(GroundTile playerTile)
    {
        this.playerTile = playerTile;
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

    public void TileSelect(GroundTile selectedTile)
    {
        isTileSelected = true;
        this.selectedTile = selectedTile;
        ResetPathColor();
        PrintShortestDistance(playerTile.GetAllNeighbors(), playerTile, selectedTile, grounds.Count);
    }

    private void ResetPathColor()
    {
        foreach (GroundTile groundTile in path)
        {
            groundTile.UnSetPathPathColor();
        }
        path.Clear();
    }

    public void UnSelectTile()
    {
        isTileSelected = false;
        selectedTile.Unselect();
    }

    public void ValidateTile()
    {
        playerManager.SetTarget(selectedTile);
        if(path.Count!=0)
        {
            playerManager.SetPath(path);
        }
    }

    public GameObject GetSelectedTile()
    {
        return selectedTile.gameObject;
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

    private void Update()
    {
        foreach (GroundTile groundTile in path)
        {
            groundTile.SetIsPath();
        }
    }
    
    private void PrintShortestDistance(List<GroundTile> adj, GroundTile start, GroundTile dest, int v)
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

        this.path = path;
        
        for (int i = path.Count - 1; 
            i >= 0; i--) 
        {
            Console.Write(path[i] + " ");
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
}