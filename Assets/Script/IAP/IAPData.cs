using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class IAPData
{
    //types of purchases to save
    private bool noAds  = false;
    private int[] codeStatus = new int[100]; //change based off number of rewards

    private void Start() {
        codeStatus = new int[100];
    }

//getter methods
    public bool getNoAds() {
        return noAds;
    }

    public bool getCodeStatus(int index) {
        if (codeStatus == null) {
            Start();
        }
        
        if (codeStatus[index] ==  0) {
            return true;
        }

        return false;
    }

//setter methods
    public void setNoAds() {
        noAds = true;
        SaveSystem.saveIAPData(this);
    }

    public void setCodeStatus(int index) {
        codeStatus[index] = 1;
    }
}
