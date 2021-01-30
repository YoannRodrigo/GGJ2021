using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wisp : MonoBehaviour
{
    [SerializeField] private PlayerManager _player = default;
    [SerializeField] private FloorManager _floorManager = default;
    [SerializeField] private GroundTile _currentTile = default;
    [SerializeField] private ObjectPooler _objectPooler;
    [SerializeField] List<GroundTile> _path = new List<GroundTile>();
    private float _floatYDistance = .3f;
    private float _floatDuration = 1f; //in seconds

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        FloatingYLoop();
    }

    private void FloatingYLoop()
    {
        transform.DOMoveY(transform.position.y + _floatYDistance, _floatDuration)
        .SetEase(Ease.InOutSine)
        .SetLoops(-1, LoopType.Yoyo);
    }
    public void MoveToTile(GroundTile tile)
    {
        _path = _floorManager.GetBfs(_currentTile, tile);
        if (_path != null)
        {
            _path.RemoveAt(0);
            Sequence moveSequence = DOTween.Sequence();
            foreach (GroundTile currentTile in _path)
            {
                Vector3 normalizedYPosition = NormalizeYPosition(currentTile.transform.position);
                moveSequence.Append(transform.DOMove(normalizedYPosition, .3f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    ReachTile(currentTile);
                }));
            }
            moveSequence.Play();
        }
    }

    public void SetPosition(GroundTile tile)
    {
        _currentTile = tile;
    }

    private Vector3 NormalizeYPosition(Vector3 position)
    {
        return new Vector3(position.x, transform.position.y, position.z);
    }
    private void ReachTile(GroundTile tile)
    {

        DeactivateMechanismOnTile(_currentTile);
        _currentTile = tile;
        ActivateMechanismOnTile(tile);
    }

    private void ActivateMechanismOnTile(GroundTile tile)
    {
        tile.ActivateMechanisms();
    }
    private void DeactivateMechanismOnTile(GroundTile tile)
    {
        tile.DeactivateMechanisms();
    }
}