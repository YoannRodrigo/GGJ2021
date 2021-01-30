using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField] private GameObject prefabRed;
    [SerializeField] private GameObject prefabGreen;
    [SerializeField] private int xSize;
    [SerializeField] private int zSize;
    private bool isRed;
    private int index;

    public void GenerateFloor()
    {
        index = 0;
        for(int i = 0 ; i <= xSize; i++)
        {
            for (int j = 0; j <= zSize; j++)
            {
                GameObject spawnTile = Instantiate(isRed ? prefabRed : prefabGreen, new Vector3(i,0,j), Quaternion.identity, transform);
                spawnTile.name = "Floor"+index;
                spawnTile.GetComponent<GroundTile>().SetID(index);
                index++;
                isRed = !isRed;
            }
        }
    }

}
