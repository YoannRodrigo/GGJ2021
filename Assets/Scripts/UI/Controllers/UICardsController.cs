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
    public GameObject zoomedCard;

    public int currentHoveredCardID = -1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayZoomedCard();
    }

    public void ShowCard(int id, Card card){
        GameObject cardToShow = cards.transform.GetChild(id).gameObject;

        SetTargetVisual(cardToShow, card);
        cardToShow.SetActive(true);
    }

    public void DisplayZoomedCard(){
        if(currentHoveredCardID == -1){
            zoomedCard.SetActive(false);
        }else{
            SetTargetVisual(zoomedCard, MasterManager.Instance.cardsManager.player.playerCards[currentHoveredCardID]);
            zoomedCard.SetActive(true);
        }
    }

    public void SetTargetVisual(GameObject target, Card card){

        //Set card sprite
        target.GetComponent<Image>().sprite = card.sprite;

        switch(card.arcana){
            case Card.CardArcana.MAJOR : 
                target.transform.Find("Text").gameObject.SetActive(false);
            break;
            case Card.CardArcana.MINOR : 
                MinorArcanaCard mCard = (MinorArcanaCard)card;
                target.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = mCard.value.ToString();
                target.transform.Find("Text").gameObject.SetActive(true);
            break;
        }
    }


}
