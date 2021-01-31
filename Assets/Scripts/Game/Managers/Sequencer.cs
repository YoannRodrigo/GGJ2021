using DG.Tweening;
using UnityEngine;

public class Sequencer : MonoBehaviour
{

    public Sequence SQ_Move(GameObject target, float heightPos, int direction, float duration){
        Sequence sq_moveUp = DOTween.Sequence();

        sq_moveUp
            .Append(target.GetComponent<RectTransform>().DOMoveY(heightPos * direction, duration)).SetEase(Ease.OutCubic);
        
        sq_moveUp.Play();
        return sq_moveUp;
    }

    public void SQ_RotateLoop(GameObject target, float rotation, float duration){
        Sequence sq_rotateLoop = DOTween.Sequence();

        sq_rotateLoop
            .Append(target.transform.DOLocalRotate(new Vector3(0, -rotation, 0), duration)).SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
        
        sq_rotateLoop.Play();
    }

    public void SQ_UpDown(GameObject target, float height, float duration){
        Sequence sq_upDown = DOTween.Sequence();

        sq_upDown
            .Append(target.transform.DOLocalMoveY(target.transform.position.y + height, duration)).SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
        
        sq_upDown.Play();
    }
}
