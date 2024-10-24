using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopData
{
    private int[] itemBitMap = new int[100]; //probably won't have more than 100 items
    private int[] ballSkinBitMap = new int[100];
    private int[] ballTypeBitMap = new int[100];

    public ShopData(){

    }

    public void Start(){ 
        ShopData initialData = SaveSystem.getShopData();
        if (initialData != null){
            this.ballSkinBitMap = initialData.ballSkinBitMap;
            this.ballTypeBitMap = initialData.ballTypeBitMap;
        }

        if (ballSkinBitMap == null){
            ballSkinBitMap = new int[100];
        }

        if (ballTypeBitMap == null){
            ballTypeBitMap = new int[100];
        }
    }


    //value ranges from 0 to 2, 0 = not bought, 1 = bought, 2 = selected
    public void setSkinItemStatus(int index, int value){
        ballSkinBitMap[index] = value;
        SaveSystem.saveShopData(this);
    }

    public int getSkinItemStatus(int index){
        Start();
        return ballSkinBitMap[index];
    }

    public int[] geSkinItemBitMap(){
        return ballSkinBitMap;
    }

    public void setBallTypeStatus(int index, int value){
        ballTypeBitMap[index] = value;
        SaveSystem.saveShopData(this);
    }

    public int getBallTypeStatus(int index){
        Start();
        return ballTypeBitMap[index];
    }

    public int[] getBallTypeBitMap(){
        return ballTypeBitMap;
    }
}
