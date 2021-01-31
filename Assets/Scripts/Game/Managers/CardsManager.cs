using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    public UICardsController uICardsController;
    public int maxCards;
    private readonly List<Card> cards = new List<Card>();
    private Card cardToPlay;
    public int activeCardID = -1;

    public List<Card> GetCards()
    {
        return cards;
    }
    
    public void AddCard(Card card){
        if(cards.Count < maxCards){
            cards.Add(card);
            uICardsController.ShowCard(cards.IndexOf(card), card);
            _soundManager.PlaySound("CardCollect");
            if(uICardsController.currentHoveredCardID == -1){
                PlayCard(cards.IndexOf(card));
                MasterManager.Instance.sequencer.Move(uICardsController.cards.transform.GetChild(cards.IndexOf(card)).gameObject, uICardsController.onMouseHoverHeight, uICardsController.onMouseHoverDuration);
                uICardsController.currentHoveredCardID = cards.IndexOf(card);
                uICardsController.zoomedCardController.DisplayZoomedCard();
            }
        }
    }

    public void PlayCard(int cardIndex){
        if(activeCardID != -1){
            MasterManager.Instance.sequencer.Move(uICardsController.cards.transform.GetChild(activeCardID).gameObject, 0, uICardsController.onMouseHoverDuration);
        }
        activeCardID = cardIndex;
        cardToPlay = cards[cardIndex];
        _soundManager.PlayRandomSound(new string[]{"CardUse_1","CardUse_2","CardUse_3","CardUse_4","CardUse_5"});
    }

    public Card GetCardToPlay()
    {
        return cardToPlay;
    }
}
