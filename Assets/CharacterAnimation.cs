using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public void FootSteps(){
        SoundManager.instance.PlayRandomSound(new string[] {"Foots_1","Foots_2","Foots_3","Foots_4","Foots_5"});
    }
}
