using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZoomedCardController : MonoBehaviour
{

    public UICardsController uICardsController;

    public void DisplayZoomedCard(){
        if(uICardsController.currentHoveredCardID == -1){
            gameObject.SetActive(false);
        }else{
            uICardsController.SetTargetVisual(gameObject, MasterManager.Instance.cardsManager.GetCards()[uICardsController.currentHoveredCardID]);
            gameObject.SetActive(true);
        }
    }
}
