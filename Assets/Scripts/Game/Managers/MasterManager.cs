﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    private static MasterManager _instance;

    public static MasterManager Instance { get { return _instance; } }

    //Linked Managers
    [Header("Managers")]
    public CardsManager cardsManager;
    public PlayerManager playerManager;
    public FloorManager floorManager;

    private Card cardToPlay;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
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
                    break;
                case Card.CardFamily.DICE:
                    floorManager.ShowPath(minorArcanaCard.value + 1);
                    break;
                case Card.CardFamily.TBD2:
                    break;
                case Card.CardFamily.TBD3:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
