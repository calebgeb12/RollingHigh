using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopActivateScript : MonoBehaviour
{
    public GameObject shop;
    public GameObject freezeUpgradeShop;
    public GameObject spikeUpgradeShop;
    public AbilitiesStatus abilitiesScript;
    public List<GameObject> shopTypes = new List<GameObject>();   
    public List<GameObject> ballInfoTypes = new List<GameObject>();
    public GameObject defaultSkinInfo;

    public void activateShopType(int shopIndex){
        for (int i = 0; i < shopTypes.Count; i++){
            shopTypes[i].SetActive(false);
        }

        shopTypes[shopIndex].SetActive(true);   
    }

    public void activateShop(){
        shop.SetActive(true);
    }

    public void deactivateShop(){
        shop.SetActive(false);
    }
    
    public void activateSpikeUpgradeShop(){
        abilitiesScript.Start();
        spikeUpgradeShop.SetActive(true);
    }

    public void deactivateSpikeUpgradeShop(){
        spikeUpgradeShop.SetActive(false);
    }

    public void activateFreezeUpgradeShop(){
        abilitiesScript.Start();
        freezeUpgradeShop.SetActive(true);
    }

    public void deactivateFreezeUpgradeShop(){
        freezeUpgradeShop.SetActive(false);
    }

    public void activateBallTypeInfo(int index){
        ballInfoTypes[index].SetActive(true);
    }

    public void deactivateBallTypeInfo(int index){
        ballInfoTypes[index].SetActive(false);
    }

    public void activateDefaultSkinInfo(){
        defaultSkinInfo.SetActive(true);
    }

    public void deactivateDefaultSkinInfo(){
        defaultSkinInfo.SetActive(false);
    }
}
