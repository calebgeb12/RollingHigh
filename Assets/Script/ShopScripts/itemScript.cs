using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemScript : MonoBehaviour
{
    public GameObject buyButton;
    public GameObject selectButton;
    public GameObject selectedButton;
    public int price;

    private int bitMapIndex;
    private int status; //change to private later
    public int itemType; //determines what item this is
    public bool defaultType;

    //get reference to image
    public Image image;
    private PointData pointData;
    private int coinAmount;

    private ShopStatus shopStatus;

    public void Start(){
        shopStatus = FindObjectOfType<ShopStatus>();
        getItemIndex();
        getItemStatus();
        pointData = SaveSystem.getPointData();
        coinAmount = pointData.getCoins();

        setButtonStatus();
    }

//sets button status upon opening store
    private void setButtonStatus(){
        //if item hasn't been bought yet
        if (status == 0){
            buyButton.SetActive(true);
            selectButton.SetActive(false);
            selectedButton.SetActive(false);
        }

        //if item has been bought but not seelcted
        else if (status == 1){
            buyButton.SetActive(false);
            selectButton.SetActive(true);
            selectedButton.SetActive(false);
        }

        //if item has been selected
        else if (status == 2){
            buyButton.SetActive(false);
            selectButton.SetActive(false);
            selectedButton.SetActive(true);
        }
    }

//changes button based on status of item after buying or selecting
    public void activateButton(int buttonIndex){
        coinAmount = SaveSystem.getPointData().getCoins(); 
        //sets select button to active and saves purchase
        if (buttonIndex == 0 && coinAmount >= price){
            buyButton.SetActive(false);
            selectButton.SetActive(true);
            setItemStatus(1);
        }

        //sets 'selected' button to active and saves selection and deselects another item
        else if (buttonIndex == 1){
            selectButton.SetActive(false);
            selectedButton.SetActive(true);
            setItemStatus(2);
        }

    }

//for getting the item's index
    private void getItemIndex(){
        if (itemType == 0){ //for ball skins
            bitMapIndex = shopStatus.getSkinItemIndex(this.gameObject);
        }

        else if (itemType == 1){ //for ball types
            bitMapIndex = shopStatus.getBallTypeIndex(this.gameObject);
        }

        else if (itemType == 2){//currently for extra item type

        }

    }

//for getting the status based on item type
    public void getItemStatus(){
        if (itemType == 0){ //for ball skins
            status = shopStatus.getSkinItemStatus(bitMapIndex);
        }

        else if (itemType == 1){ //for ball types
            status = shopStatus.getBallTypeStatus(bitMapIndex);
        }

        else if (itemType == 2){ //currently for extra item type

        }

        if (defaultType && status == 0){ //status can only be zero if player starts game for first time, so set status to selected initially
            status = 2;
            setItemStatus(2);
        }
    }

//for setting the status based on item type in 'shopdata'
    public void setItemStatus(int value){
        if (itemType == 0){ //for ball skins
            shopStatus.setSkinItemStatus(bitMapIndex, value);
        }

        else if (itemType == 1){ //for ball types
            shopStatus.setBallTypeStatus(bitMapIndex, value);
        }

        else if (itemType == 2){

        }

        status = value;
        setButtonStatus();
    }

}
