using DG.Tweening;
using UnityEngine;

public class Movable : MonoBehaviour
{
    private GroundTile currentGroundTile;
    private GroundTile lastGroundTile;

    private void OnTriggerEnter(Collider other)
    {
        GroundTile groundTile = other.GetComponent<GroundTile>();
        if (groundTile)
        {
            currentGroundTile = groundTile;
            currentGroundTile.ForceActivate(false);
            currentGroundTile.ForceEnlighten();
            transform.parent = currentGroundTile.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GroundTile groundTile = other.GetComponent<GroundTile>();
        if (groundTile)
        {
            lastGroundTile = groundTile;
            lastGroundTile.ForceActivate(false);
        }
    }

    public void MoveToDirection(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, 1).SetEase(Ease.InQuad);
    }
}
