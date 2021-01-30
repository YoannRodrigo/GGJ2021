using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public UICardsController uICardsController;
    public int maxCards;
    private readonly List<Card> cards = new List<Card>();
    private Card cardToPlay;

    public List<Card> GetCards()
    {
        return cards;
    }
    
    public void AddCard(Card card){
        if(cards.Count < maxCards){
            cards.Add(card);
            uICardsController.ShowCard(cards.IndexOf(card), card);
        }
    }

    public void PlayCard(int cardIndex){
        cardToPlay = cards[cardIndex];
    }

    public Card GetCardToPlay()
    {
        return cardToPlay;
    }
}
