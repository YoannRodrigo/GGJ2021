using System;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance { get; private set; }

    //Linked Managers
    [Header("Managers")]
    public CardsManager cardsManager;
    public Sequencer sequencer;
    public PlayerManager playerManager;
    public FloorManager floorManager;

    private Card cardToPlay;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
    }


    private void Update()
    {
        cardToPlay = cardsManager.GetCardToPlay();
        MinorArcanaCard minorArcanaCard = (MinorArcanaCard) cardToPlay;
        if (minorArcanaCard)
        {
            switch (minorArcanaCard.family)
            {
                case Card.CardFamily.NONE:
                    break;
                case Card.CardFamily.WISPS:
                    floorManager.ShowWispChoices(minorArcanaCard.value);
                    break;
                case Card.CardFamily.DICE:
                    floorManager.ShowPlayerPath(minorArcanaCard.value + 1);
                    break;
                case Card.CardFamily.SWORDS:
                    floorManager.ShowPlayerAttack(minorArcanaCard.value);
                    break;
                case Card.CardFamily.TBD3:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
