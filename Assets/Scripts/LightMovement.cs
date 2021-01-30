using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    [SerializeField] private MovementAxis movementAxis;
    [SerializeField] private float minBound;
    [SerializeField] private float maxBound;
    [SerializeField] private float duration;

    private Quaternion minTarget;
    private Quaternion maxTarget;
    private Sequence sq;
    private bool isPlayingSequence;

    private enum MovementAxis
    {
        X_AXIS,
        Y_AXIS,
        Z_AXIS,
    }

    private void Start()
    {
        switch (movementAxis)
        {
            case MovementAxis.X_AXIS:
                minTarget = Quaternion.Euler(minBound, 0, 0);
                maxTarget = Quaternion.Euler(maxBound, 0, 0);
                break;
            case MovementAxis.Y_AXIS:
                minTarget = Quaternion.Euler(0, minBound, 0);
                maxTarget = Quaternion.Euler(0, maxBound, 0);
                break;
            case MovementAxis.Z_AXIS:
                minTarget = Quaternion.Euler(0, 0, minBound);
                maxTarget = Quaternion.Euler(0, 0, maxBound);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        sq = DOTween.Sequence();
        sq.Append(transform.DORotateQuaternion(maxTarget, duration).SetEase(Ease.Linear))
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if(!isPlayingSequence)
        {
            isPlayingSequence = true;
            sq.Play();
        }
    }

    private void OnDisable()
    {
        isPlayingSequence = false;
        sq.Kill();
    }
}
