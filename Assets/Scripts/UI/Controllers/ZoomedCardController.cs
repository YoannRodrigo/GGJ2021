using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZoomedCardController : MonoBehaviour
{

    public UICardsController uICardsController;
    public GameObject zoomedCardGO;

    // Start is called before the first frame update
    void Start()
    {
        MasterManager.Instance.sequencer.SQ_RotateLoop(zoomedCardGO, uICardsController.zoomedCardRotationRange, uICardsController.zoomedCardRotationDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayZoomedCard(){
        if(uICardsController.currentHoveredCardID == -1){
            gameObject.SetActive(false);
        }else{
            uICardsController.SetTargetVisual(gameObject, MasterManager.Instance.cardsManager.GetCards()[uICardsController.currentHoveredCardID]);
            gameObject.SetActive(true);
        }
    }
}
