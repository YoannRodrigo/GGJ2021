using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{

    public PlayerManager player;
    public FloorManager floorManager;
    public UICardsController uICardsController;
    public int maxCards = 5;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCard(Card card){
        if(player.playerCards.Count < maxCards){
            player.playerCards.Add(card);
            uICardsController.ShowCard(player.playerCards.IndexOf(card), card);
        }
    }

    public void PlayCard(int cardIndex){
        Card cardToPlay = player.playerCards[cardIndex];
        switch(cardToPlay.arcana){
            case Card.CardArcana.MAJOR : 
                MajorArcanaCard mjCard = (MajorArcanaCard)cardToPlay;




            break;
            case Card.CardArcana.MINOR : 
                MinorArcanaCard mnCard = (MinorArcanaCard)cardToPlay;
                int cardValue = mnCard.value;
                switch(mnCard.family){
                    case Card.CardFamily.DICE :
                        floorManager.size = cardValue + 1;
                    break;
                    case Card.CardFamily.WISPS :
                    
                    break;
                }




            break;
        }
    }
}
