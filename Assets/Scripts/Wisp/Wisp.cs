using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace wisp
{
    public class Wisp : MonoBehaviour
    {
        [SerializeField] private PlayerManager _player = default;
        private GroundTile _currentTile = default;

        private void Start()
        {
            _currentTile = _player.GetPosition();
        }

        public void MoveTo(GroundTile tile)
        {
            Vector3 normalizedYPosition = NormalizeYPosition(tile.transform.position);
            transform.DOMove(normalizedYPosition, 1).SetEase(Ease.Linear);
        }
        private Vector3 NormalizeYPosition(Vector3 position)
        {
            return new Vector3(position.x, transform.position.y, position.z);
        }
    }

}