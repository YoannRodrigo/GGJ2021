using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnMouseOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public UICardsController uICardsController;

    private Vector2 boundaries;
    private void Start(){
        boundaries = new Vector2(gameObject.transform.position.y, gameObject.transform.position.y + uICardsController.onMouseHoverHeight);
    }

    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        MasterManager.Instance.sequencer.SQ_Move(gameObject, boundaries.y, 1, uICardsController.onMouseHoverDuration);
        uICardsController.currentHoveredCardID = gameObject.transform.GetSiblingIndex();
        uICardsController.zoomedCardController.DisplayZoomedCard();

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MasterManager.Instance.sequencer.SQ_Move(gameObject, boundaries.x, -1, uICardsController.onMouseHoverDuration);
        uICardsController.currentHoveredCardID = -1;
    }
}
