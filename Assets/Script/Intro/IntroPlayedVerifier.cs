using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class IntroPlayedVerifier
{
    private bool introPlayed = false;

    public IntroPlayedVerifier(){

    }

    public IntroPlayedVerifier(bool val){
        introPlayed = val;
    }

    public bool getIntroPlayed(){
        return introPlayed;
    }

    public void setIntroPlayed(){
        introPlayed = true;
    }
}
