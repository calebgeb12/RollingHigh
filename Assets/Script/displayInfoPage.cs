using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayInfoPage : MonoBehaviour
{
    public GameObject infoPage;
    public GameObject bountyInfo;
    public GameObject rewardScreen;

    public void enable(){
        infoPage.SetActive(true);
    }

    public void disable(){
        infoPage.SetActive(false);
    }

    public void enableBountyInfo() {
        bountyInfo.SetActive(true);
    }

    public void disableBountyInfo() {
        bountyInfo.SetActive(false);
    }

    public void enableRewardScreen() {
        rewardScreen.SetActive(true);
    }

    public void disableRewardScreen() {
        rewardScreen.SetActive(false);
    }
}
