using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{

    public PlayerManager player;
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
}
