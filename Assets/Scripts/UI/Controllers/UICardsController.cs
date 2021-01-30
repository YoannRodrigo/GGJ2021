using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICardsController : MonoBehaviour
{

    public GraphicRaycaster graphicRaycaster;

    [Header("Ui elements")]
    public GameObject cards;
    public ZoomedCardController zoomedCardController;

    public int currentHoveredCardID = -1;

    [Header("Game feel")]
    public float onMouseHoverHeight;
    public float onMouseHoverDuration;
    public float zoomedCardRotationRange;
    public float zoomedCardRotationDuration;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCard(int id, Card card){
        GameObject cardToShow = cards.transform.GetChild(id).gameObject;

        SetTargetVisual(cardToShow, card);
        cardToShow.SetActive(true);
    }

    public void SetTargetVisual(GameObject target, Card card){

        //Set card sprite
        target.transform.Find("Card/CardImage").GetComponent<Image>().sprite = card.sprite;

        //Check for desc then fill
        if(target.transform.Find("Dialogue")){
            target.transform.Find("Dialogue/Title").GetComponent<TextMeshProUGUI>().text = card.cardName;
            target.transform.Find("Dialogue/Text").GetComponent<TextMeshProUGUI>().text = FillCardDescription(card);
        }

        switch(card.arcana){
            case Card.CardArcana.MAJOR : 
                target.transform.Find("Card/Value").gameObject.SetActive(false);
            break;
            case Card.CardArcana.MINOR : 
                MinorArcanaCard mCard = (MinorArcanaCard)card;
                target.transform.Find("Card/Value").GetComponent<TextMeshProUGUI>().text = mCard.value.ToString();
                target.transform.Find("Card/Value").gameObject.SetActive(true);
            break;
        }
    }

    public string FillCardDescription(Card card){
        switch(card.arcana){
            case Card.CardArcana.MAJOR : 
                return card.description;
            break;
            case Card.CardArcana.MINOR : 
                MinorArcanaCard mCard = (MinorArcanaCard)card;
                string baseDesc = mCard.description;

                return baseDesc.Replace("{VALUE}", mCard.value.ToString());
            break;
            default :
                return card.description;
            break;
        }
    }


}
