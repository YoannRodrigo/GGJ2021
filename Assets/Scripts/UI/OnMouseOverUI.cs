using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnMouseOverUI : MonoBehaviour, IPointerEnterHandler
{

    public UICardsController uICardsController;

    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        uICardsController.currentHoveredCardID = gameObject.transform.GetSiblingIndex();
        uICardsController.zoomedCardController.DisplayZoomedCard();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uICardsController.currentHoveredCardID = -1;
    }
}
