using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStatus : MonoBehaviour
{
    public GameObject[] skinItems;
    public GameObject[] ballTypeItems;
    private int[] ballSkinBitMap;
    private int[] ballTypeBitMap;
    private ShopData shopData;
    private AbilitiesStatus abilitiesData;

    private int currentSkin;
    private int currentBallType;


    void Awake()
    {
        abilitiesData = FindObjectOfType<AbilitiesStatus>();
        shopData = SaveSystem.getShopData();
        shopData.Start();
        ballSkinBitMap = shopData.geSkinItemBitMap();
        ballTypeBitMap = shopData.getBallTypeBitMap();
        setCurrentlySelected();
   }


//get index
    public int getSkinItemIndex(GameObject item){
        for (int i = 0; i < skinItems.Length; i++){
            if (item == skinItems[i]){
                return i;
            }
        }

        return -1;
    }

    public int getBallTypeIndex(GameObject item){
        for (int i = 0; i < ballTypeItems.Length; i++){
            if (item == ballTypeItems[i]){
                return i;
            }
        }

        return -1;
    }

//get status
    public int getSkinItemStatus(int index){
        return ballSkinBitMap[index];
    }

    public int getBallTypeStatus(int index){
        return ballTypeBitMap[index];
    }

//select item
    public void selectSkin(int index){
        for (int i = 0; i < ballSkinBitMap.Length; i++){
            if (ballSkinBitMap[i] == 2){
                ballSkinBitMap[i] = 1;
                shopData.setSkinItemStatus(i, 1);
                skinItems[i].GetComponent<itemScript>().setItemStatus(1);
                break;
            }
        }
    }

    public void selectBallType(int index){
        for (int i = 0; i < ballTypeBitMap.Length; i++){
            if (ballTypeBitMap[i] == 2){
                ballTypeBitMap[i] = 1;
                shopData.setBallTypeStatus(i, 1);
                ballTypeItems[i].GetComponent<itemScript>().setItemStatus(1);
                break;
            }
        }
    }

//set status
    public void setSkinItemStatus(int index, int value){

        if (value == 2){
            selectSkin(index);
            currentSkin = index;
        }

        ballSkinBitMap[index] = value;
        shopData.setSkinItemStatus(index, value);
    }

    public void setBallTypeStatus(int index, int value){

        if (value == 2){
            selectBallType(index);
            currentBallType = index;
        }

        
        ballTypeBitMap[index] = value;
        shopData.setBallTypeStatus(index, value);

        if (value == 1){
            abilitiesData.setUpgradeStatus();
        }
    }


//get current item
    public int getCurrentSkin(){
        Awake(); //make sure values have been initialized
        return currentSkin;
    }

    
    public int getCurrentBallType(){
        Awake(); //make sure values have been initialized
        return currentBallType;
    }

//set current selected
    public void setCurrentlySelected(){
        for (int i = 0; i < ballSkinBitMap.Length; i++){
            if (ballSkinBitMap[i] == 2){
                currentSkin = i;
                break;
            }
        }

        for (int i = 0; i < ballTypeBitMap.Length; i++){
            if (ballTypeBitMap[i] == 2){
                currentBallType = i;
                break;
            }
        }
    }

    public void resetItems(){
        //reset data
        SaveSystem.resetShopData();
        SaveSystem.resetAbilitiesData();
        SaveSystem.resetIAPData();

        //update data in this script
        Awake();

        //update data and status for all items
        foreach (GameObject item in skinItems){
            item.GetComponent<itemScript>().Start();
        }

        foreach (GameObject item in ballTypeItems){
            item.GetComponent<itemScript>().Start();
        }

        abilitiesData.GetComponent<AbilitiesStatus>().Awake();


    }
}
