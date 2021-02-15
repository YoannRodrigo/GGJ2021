using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sequencer : MonoBehaviour
{

    public void Move(GameObject target, float heightPos, float duration){

        target.transform.DOMoveY(heightPos, duration).SetEase(Ease.OutCubic)
        .OnComplete(()=>{
            target.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(target.GetComponent<RectTransform>().anchoredPosition3D.x ,target.GetComponent<RectTransform>().anchoredPosition3D.y, target.GetComponent<RectTransform>().anchoredPosition3D.z);
        });

        
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

    public void SQ_BlinkText(GameObject target, float duration){
        Sequence sq_blText = DOTween.Sequence();
        Image img = target.GetComponent<Image>();

        sq_blText
            .Join(img.DOFade(0,duration)).SetEase(Ease.InSine)
            .SetLoops(-1, LoopType.Yoyo);
        
        sq_blText.Play();
    }
}
