using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
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
            _soundManager.PlaySound("CardCollect");
        }
    }

    public void PlayCard(int cardIndex){
        cardToPlay = cards[cardIndex];
        _soundManager.PlayRandomSound(new string[]{"CardUse_1","CardUse_2","CardUse_3","CardUse_4","CardUse_5"});
    }

    public Card GetCardToPlay()
    {
        return cardToPlay;
    }
}
