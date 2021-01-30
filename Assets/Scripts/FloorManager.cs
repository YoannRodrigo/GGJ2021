using System;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private List<GroundTile> grounds;
    [SerializeField] private GroundTile selectedTile;
    private bool isTileSelected;
    public void AddATile(GroundTile tile)
    {
        if (!grounds.Contains(tile))
        {
            grounds.Add(tile);
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
    }
    public void UnSelectTile()
    {
        isTileSelected = false;
        selectedTile.Unselect();
    }

    public void ValidateTile()
    {
        playerManager.SetTarget(selectedTile);
    }

    public GameObject GetSelectedTile()
    {
        return selectedTile.gameObject;
    }
}
