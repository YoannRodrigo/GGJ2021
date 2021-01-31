using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wisp : MonoBehaviour
{
    private const float FADE_TIME = .5f;
    [SerializeField] private PlayerManager _player = default;
    [SerializeField] private FloorManager _floorManager = default;
    [SerializeField] private GroundTile _currentTile = default;
    [SerializeField] private ObjectPooler _objectPooler;
    [SerializeField] public List<GroundTile> _path = new List<GroundTile>();
    [SerializeField] private SoundManager _soundManager;
    private float _floatYDistance = .3f;
    private float _floatDuration = 1f; //in seconds
    private bool _inMovement = false;

    public GroundTile CurrentTile { get => _currentTile; set => _currentTile = value; }
        public event Action<GroundTile> InitCurrentTile;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        FloatingYLoop();
        _soundManager.PlaySoundLoop("WispIdle");
    }

    private void FloatingYLoop()
    {
        transform.DOMoveY(transform.position.y + _floatYDistance, _floatDuration)
        .SetEase(Ease.InOutSine)
        .SetLoops(-1, LoopType.Yoyo);
    }
    private void Update()
    {
        if (_currentTile)
        {
            if(!_inMovement){
                _soundManager.PlaySound("WispStartMovingFeedback");
                _inMovement = true;
            }
            SwapToMovingSound();
            Vector3 target = new Vector3(_currentTile.transform.position.x, transform.position.y, _currentTile.transform.position.z);
            transform.DOMove(target, 1).SetEase(Ease.Linear);
            if (Vector3.Distance(target, transform.position) < 0.01f)
            {
                _currentTile.SetDefaultColor();
                _currentTile = null;
            }
        }
        else{
            SwapToIdleSound();
            _inMovement = false;
        }
    }
    private void SwapToIdleSound(){
        _soundManager.FadeInSound("WispIdle", FADE_TIME);
        _soundManager.FadeOutSound("WispMoving",FADE_TIME);
    }
    private void SwapToMovingSound(){
        _soundManager.FadeInSound("WispMoving", FADE_TIME);
        _soundManager.FadeOutSound("WispIdle",FADE_TIME);
    }
    public void SetTargetTile(GroundTile targetTile)
    {
        _currentTile = targetTile;
    }
    
    private void MoveToTile(GroundTile tile)
    {
        _path = _floorManager.GetBfs(_currentTile, tile);
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

    public void SetPosition(GroundTile tile)
    {
        CurrentTile = tile;
        InitCurrentTile?.Invoke(tile);
    }

    private Vector3 NormalizeYPosition(Vector3 position)
    {
        return new Vector3(position.x, transform.position.y, position.z);
    }
    private void ReachTile(GroundTile tile)
    {

        DeactivateMechanismOnTile(CurrentTile);
        CurrentTile = tile;
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