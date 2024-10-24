using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class AbilitiesStatus : MonoBehaviour
{
    private abilities abilitiesData;
    public ShopStatus shopStatus;
    private int coins;

//ball specific variables
    
    //freeze variables
    private float freezeTime;
    private float freezeCooldown;

    private float freezeTimeIncreaseAmount = .1f;
    private float maxFreezeTime = 2.2f;
    private float freezeCooldownIncreaseAmount = .5f;
    private float maxFreezeCooldownTime = 5f;


    private int freezeTimeIncreaseCost;
    private int freezeCooldownIncreaseCost;

    //spike ball variables
    private int numOfSpikes;
    private int maxSpikes = 20;


//UI
    public TextMeshProUGUI secondCoinsText;

    //freeze ball UI
    private bool theWorldBought = false;
    public TextMeshProUGUI freezeCoinsText;
    public TextMeshProUGUI freezeTimeText;
    public TextMeshProUGUI freezeCooldownText;

    public TextMeshProUGUI freezeTimeCostText;
    public TextMeshProUGUI freezeCooldownCostText;


    public Button freezeTimeButton;
    public TextMeshProUGUI freezeTimeMaxText;
    public Button freezeCooldownButton; 
    public TextMeshProUGUI freezeCooldownMaxText;

    //spike ball UI
    private bool spikeyMikeyBought = false; 
    public TextMeshProUGUI spikeCoinsText;
    public TextMeshProUGUI numOfSpikesText;
    public TextMeshProUGUI spikeCostText;


    public Button buySpikeButton;
    public TextMeshProUGUI maxNumOfSpikesText;

        //for upgrade status
    public Button freezeUpgradeButton;
    public Button spikeUpgradeButton;
    public TextMeshProUGUI freezeUpgradeText;
    public TextMeshProUGUI spikeUpgradeText;
    // private float disabledColor = .2f;
    // private float enabledColor = .6f;

    private Color enabledColor = new Color(.424f, .424f, .424f, .6f);
    private Color disabledColor = new Color(.424f, .424f, .424f, .2f);

    private Color enabledColor2 = new Color(1, 1, 1, 1f);
    private Color disabledColor2 = new Color(.424f, .424f, .424f, 1);




    

 

    public void Awake()
    {
        abilitiesData = SaveSystem.getAbilitiesData();
        coins = SaveSystem.getPointData().getCoins(); 

        //set freeze values
        freezeTime = abilitiesData.getFreezeTime();
        freezeCooldown = abilitiesData.getFreezeCooldown();

        //set freeze text values
        freezeCoinsText.text = "Coins: " + coins.ToString();

        freezeTimeText.text = "Freeze Duration: " + freezeTime.ToString();
        freezeTimeCostText.text = abilitiesData.getFreezeTimeCost().ToString();

        freezeCooldownText.text = "Freeze Cooldown: " + freezeCooldown.ToString();
        freezeCooldownCostText.text = abilitiesData.getFreezeCooldownCost().ToString();

        //set spike values
        numOfSpikes = abilitiesData.getNumOfSpikes();
        
        numOfSpikesText.text = "Number of Spikes: " + numOfSpikes.ToString();
        spikeCostText.text = abilitiesData.getSpikeCost().ToString();

        setUpgradeStatus();
    }
    
    public void Start(){
        coins = SaveSystem.getPointData().getCoins(); 
        freezeCoinsText.text = "Coins: " + coins.ToString();

        spikeCoinsText.text = "Coins: " + coins.ToString();
    }

//setter methods


    //freeze setter methods
    public void increaseFreezeTime(){
        int cost = SaveSystem.getAbilitiesData().getFreezeTimeCost();
        coins = SaveSystem.getPointData().getCoins(); 

        if (coins > cost && freezeTime < maxFreezeTime){
            freezeTime += freezeTimeIncreaseAmount;
            SaveSystem.savePointData(coins - cost);
            abilitiesData.setFreezeTime(freezeTime);
            abilitiesData.setFreezeTimeCost();

            //update text values
            coins = coins - cost;
            freezeCoinsText.text = "Coins: " + coins.ToString();
            freezeTimeText.text = "Freeze Duration: " + Math.Round(freezeTime, 1).ToString();
            freezeTimeCostText.text = SaveSystem.getAbilitiesData().getFreezeTimeCost().ToString();
            secondCoinsText.text = "Total Coins: " + coins.ToString();
        }


        else {
            //set text to active and disable after a certain amount of time
            //print not enough coins error
        }


        if (freezeTime >= maxFreezeTime){ //show player max level has bene reached for freeze duration
            freezeTimeButton.gameObject.SetActive(false);
            freezeTimeCostText.enabled = false;
            freezeTimeMaxText.gameObject.SetActive(true);
        }
    }

    public void increaseCooldownTime(){
        int cost = SaveSystem.getAbilitiesData().getFreezeCooldownCost();
        coins = SaveSystem.getPointData().getCoins(); 

        if (coins > cost && freezeCooldown > maxFreezeCooldownTime){
            freezeCooldown -= freezeCooldownIncreaseAmount;
            SaveSystem.savePointData(coins - cost);
            abilitiesData.setFreezeCooldown(freezeCooldown);
            abilitiesData.setFreezeCooldownCost();

            //update text values
            coins = coins - cost;
            freezeCoinsText.text = "Coins: " + coins.ToString();
            freezeCooldownText.text = "Freeze Duration: " + freezeCooldown.ToString();
            freezeCooldownCostText.text = SaveSystem.getAbilitiesData().getFreezeCooldownCost().ToString();
            secondCoinsText.text = "Total Coins: " + coins.ToString();
        }

        else {
            //set text to active and disable after a certain amount of time
            //print not enough coins error
        }

        if (freezeCooldown <= maxFreezeCooldownTime){ //show player max level has bene reached for freeze cooldown
            freezeCooldownButton.gameObject.SetActive(false);
            freezeCooldownCostText.enabled = false;
            freezeCooldownMaxText.gameObject.SetActive(true);
        }

    }

    //spike setter method
    public void increaseNumOfSpikes(){
        int cost = SaveSystem.getAbilitiesData().getSpikeCost();
        coins = SaveSystem.getPointData().getCoins(); 

        if (coins > cost && maxSpikes > numOfSpikes){
            numOfSpikes++;
            SaveSystem.savePointData(coins - cost);
            abilitiesData.setNumOfSpikes(numOfSpikes);
            abilitiesData.setSpikeCost();

            //update text values
            coins = coins - cost;
            spikeCoinsText.text = "Coins: " + coins.ToString();
            numOfSpikesText.text = "Number of Spikes: " + numOfSpikes.ToString();
            spikeCostText.text = SaveSystem.getAbilitiesData().getSpikeCost().ToString();
            secondCoinsText.text = "Total Coins: " + coins.ToString();
        }

        else {
            //set text to active and disable after a certain amount of time
            //print not enough coins error
        }

        if (numOfSpikes >= maxSpikes){ //show player has purhcased the max num of spikes
            buySpikeButton.gameObject.SetActive(false);
            spikeCostText.enabled = false;
            maxNumOfSpikesText.gameObject.SetActive(true);
        }

    }

    public void setUpgradeStatus(){
        int freezeStatus = shopStatus.getBallTypeStatus(4);
        int spikeStatus = shopStatus.getBallTypeStatus(2);


        //set status of upgrade buttons
        if (freezeStatus == 1 || freezeStatus == 2){
            theWorldBought = true;
        }

        else {
            theWorldBought = false;
        }

        if (spikeStatus == 1 || spikeStatus == 2){
            spikeyMikeyBought = true;
        }

        else {
            spikeyMikeyBought = false;
        }

        if (!theWorldBought){
            Image freezeUpgradeImage = freezeUpgradeButton.GetComponent<Image>();
            Color newColor = freezeUpgradeImage.color;
            newColor = disabledColor;
            freezeUpgradeImage.color = newColor;
            freezeUpgradeText.color = disabledColor2;
            freezeUpgradeButton.enabled = false;
        }

        else {
            Image freezeUpgradeImage = freezeUpgradeButton.GetComponent<Image>();
            Color newColor = freezeUpgradeImage.color;
            newColor = enabledColor;
            freezeUpgradeImage.color = newColor;
            freezeUpgradeText.color = enabledColor2;
            freezeUpgradeButton.enabled = true;     
        }

        if (!spikeyMikeyBought){
            Image spikeUpgradeImage = spikeUpgradeButton.GetComponent<Image>();
            Color newColor = spikeUpgradeImage.color;
            newColor = disabledColor;
            spikeUpgradeImage.color = newColor; 
            spikeUpgradeText.color = disabledColor2;  
            spikeUpgradeButton.enabled = false;
        }

        
        else {
            Image spikeUpgradeImage = spikeUpgradeButton.GetComponent<Image>();
            Color newColor = spikeUpgradeImage.color;
            newColor = enabledColor;
            spikeUpgradeImage.color = newColor;   
            spikeUpgradeText.color = enabledColor2;  
            spikeUpgradeButton.enabled = true;
        }

        if (freezeTime >= maxFreezeTime){
            freezeTimeButton.gameObject.SetActive(false);
            freezeTimeCostText.enabled = false;
            freezeTimeMaxText.gameObject.SetActive(true);
        }

        else {
            freezeTimeButton.gameObject.SetActive(true);
            freezeTimeCostText.enabled = true;
            freezeTimeMaxText.gameObject.SetActive(false);
        }

        if (freezeCooldown <= maxFreezeCooldownTime){
            freezeCooldownButton.gameObject.SetActive(false);
            freezeCooldownCostText.enabled = false;
            freezeCooldownMaxText.gameObject.SetActive(true);
        }

        else {
            freezeCooldownButton.gameObject.SetActive(true);
            freezeCooldownCostText.enabled = true;
            freezeCooldownMaxText.gameObject.SetActive(false);
        }



        if (numOfSpikes >= maxSpikes){
            buySpikeButton.gameObject.SetActive(false);
            spikeCostText.enabled = false;
            maxNumOfSpikesText.gameObject.SetActive(true);
        }

        else {
            buySpikeButton.gameObject.SetActive(true);
            spikeCostText.enabled = true;
            maxNumOfSpikesText.gameObject.SetActive(false);
        }
    }
    

    //getter methods
    public float getFreezeTime(){
        return freezeTime;
    }

    public float getFreezeCooldown(){
        return freezeCooldown;
    }
}
