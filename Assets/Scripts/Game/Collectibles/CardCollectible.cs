using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectible : MonoBehaviour
{

    public Card card;
    public string cardName;
    public Card.CardMajorType type;
    public Card.CardFamily family;
    public int value;

    private void OnTriggerEnter(Collider other) {
        switch(card.arcana){
            case Card.CardArcana.MAJOR : 
                MasterManager.Instance.cardsManager.AddCard(new MajorArcanaCard(cardName, card.arcana, type, card.sprite, card.description));
            break;
            case Card.CardArcana.MINOR : 
                MasterManager.Instance.cardsManager.AddCard(new MinorArcanaCard(cardName, card.arcana, family, value, card.sprite, card.description));
            break;
        }

        Destroy(gameObject);
        
    }
}
