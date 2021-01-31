using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevelSystem : MonoBehaviour
{
    [SerializeField] private Mechanism[] _mechanisms = default;
    [SerializeField] private PlayerManager _player = default;
    [SerializeField] private Wisp _wisp = default;
    [SerializeField] private FloorManager _floorManager = default;
    [SerializeField] private SoundManager _soundManager = default;
    private Vector3 _playerOriginalPosition = default;
    private Vector3 _wispOriginalPosition = default;
    private GroundTile _playerOriginalTile = default;
    private GroundTile _wispOriginalTile = default;

    public void GetAllMechanisms()
    {
        _mechanisms = Resources.FindObjectsOfTypeAll(typeof(Mechanism)) as Mechanism[];
    }

    public void ResetToOriginalState()
    {
        foreach (Mechanism mechanism in _mechanisms)
        {
            mechanism.Reset();
        }
        _player.transform.position = _playerOriginalPosition;
        _wisp.transform.position = _wispOriginalPosition;
        _player.currentTile = _playerOriginalTile;
        _wisp.CurrentTile = _wispOriginalTile;
        _wisp._path.Clear();
        _player.path.Clear();
        _floorManager.UnselectTile();
        _floorManager.ResetPathColor();
        _soundManager.FadeAllSounds();
    }

    private void Awake()
    {
        _playerOriginalPosition = _player.transform.position;
        _wispOriginalPosition = _wisp.transform.position;
        _playerOriginalTile = _player.currentTile;
        _wispOriginalTile = _wisp.CurrentTile;
        _player.InitCurrentTile += SetPlayerOriginalTile;
        _wisp.InitCurrentTile += SetWispOriginalTile;
        _soundManager.PlayMusic("MainMusic");
    }

    private void SetPlayerOriginalTile(GroundTile tile){
        _playerOriginalTile = tile;
    }

        private void SetWispOriginalTile(GroundTile tile){
        _wispOriginalTile = tile;
    }
}
