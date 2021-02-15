using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Tirette : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] ResetLevelSystem reset;

    public float downHeight;
    public float DownDuration;
    public float fadeDuration;
    public TextMeshProUGUI text;

    private float baseY;

    private void Start(){
        baseY = transform.position.y;
    }


    public void Reset(){
        Sequence sq_tirette = DOTween.Sequence();

         SoundManager.instance.PlaySound("CardUse_1");

        sq_tirette
            .Append(transform.DOMoveY(transform.position.y - downHeight, DownDuration)).SetEase(Ease.InSine)
            .Append(transform.DOMoveY(transform.position.y + (downHeight*1.5f), DownDuration/2f)).SetEase(Ease.InSine)
            .OnComplete(()=>{
                reset.ResetToOriginalState();
            })
            .Join(transform.DOMoveY(baseY, DownDuration)).SetEase(Ease.InSine);

        sq_tirette.Play();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        text.DOFade(1, fadeDuration).SetEase(Ease.InSine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.DOFade(0, fadeDuration).SetEase(Ease.InSine);
    }

}
