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
    public Sequencer sequencer;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
}
