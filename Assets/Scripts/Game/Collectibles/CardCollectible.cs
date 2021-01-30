using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectible : MonoBehaviour
{

    public string cardName;
    public Card.CardArcana arcana;
    public Card.CardMajorType type;
    public Card.CardFamily family;
    public int value;
    public Sprite sprite;

    private void OnTriggerEnter(Collider other) {
        switch(arcana){
            case Card.CardArcana.MAJOR : 
                MasterManager.Instance.cardsManager.AddCard(new MajorArcanaCard(cardName, arcana, type, sprite));
            break;
            case Card.CardArcana.MINOR : 
                MasterManager.Instance.cardsManager.AddCard(new MinorArcanaCard(cardName, arcana, family, value, sprite));
            break;
        }

        Destroy(gameObject);
        
    }
}
