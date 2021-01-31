using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class OnMouseOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public UICardsController uICardsController;
    private Sequence sequence;
    private Vector2 boundaries;
    private void Start()
    {
        boundaries = new Vector2(gameObject.transform.position.y, gameObject.transform.position.y + uICardsController.onMouseHoverHeight);
    }

    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(MasterManager.Instance.cardsManager.activeCardID != gameObject.transform.GetSiblingIndex()){
            MasterManager.Instance.sequencer.Move(gameObject, boundaries.y, uICardsController.onMouseHoverDuration);
        }
        uICardsController.currentHoveredCardID = gameObject.transform.GetSiblingIndex();
        uICardsController.zoomedCardController.DisplayZoomedCard();

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(MasterManager.Instance.cardsManager.activeCardID != gameObject.transform.GetSiblingIndex()){
            MasterManager.Instance.sequencer.Move(gameObject, boundaries.x, uICardsController.onMouseHoverDuration);
            uICardsController.currentHoveredCardID = -1;
        }
    }
}
