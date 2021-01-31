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

    public void GetResetables()
    {
        
        _mechanisms = Resources.FindObjectsOfTypeAll(typeof(Mechanism)) as Mechanism[];
        _player = FindObjectOfType<PlayerManager>();
        _wisp = FindObjectOfType<Wisp>();
        _floorManager = FindObjectOfType<FloorManager>();
        _soundManager = FindObjectOfType<SoundManager>();
    }


    private void Awake()
    {
        PlayerSaveValues();
        WispSaveValues();
        _soundManager.PlayMusic("MainMusic");
        GetResetables();
    }

    private void PlayerSaveValues()
    {
        _playerOriginalPosition = _player.transform.position;
        _playerOriginalTile = _player.currentTile;
        _player.InitCurrentTile += SetPlayerOriginalTile;
    }

    private void WispSaveValues()
    {
        if (_wisp == null)
        {
            return;
        }
        _wispOriginalPosition = _wisp.transform.position;
        _wispOriginalTile = _wisp.CurrentTile;
        _wisp.InitCurrentTile += SetWispOriginalTile;
    }

    private void SetPlayerOriginalTile(GroundTile tile)
    {
        _playerOriginalTile = tile;
    }

    private void SetWispOriginalTile(GroundTile tile)
    {
        _wispOriginalTile = tile;
    }

    public void ResetToOriginalState()
    {
        MechanismsReset();
        WispReset();
        PlayerReset();
        _soundManager.FadeAllSounds();
    }

    private void MechanismsReset()
    {
        foreach (Mechanism mechanism in _mechanisms)
        {
            mechanism.Reset();
        }
    }



    private void WispReset()
    {
        if (_wisp == null)
        {
            return;
        }
        _wisp.CurrentTile = _wispOriginalTile;
        _wisp._path.Clear();
        _wisp.transform.position = _wispOriginalPosition;
    }

    private void PlayerReset()
    {
        _player.transform.position = _playerOriginalPosition;
        _player.currentTile = _playerOriginalTile;
        _player.target = null;
        _player.path.Clear();
    }

    private void PathReset()
    {
        _floorManager.UnselectTile();
        _floorManager.ResetPathColor();
    }
}
