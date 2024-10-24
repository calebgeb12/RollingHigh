using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class coinManagerScript : MonoBehaviour
{
    public TextMeshProUGUI coinsDisplayText;

    //for not enough coins message
    public GameObject notEnoughCoinsMessage;
    private float notEnoughCoinsMessageTimer = 1f;
    private float originalNotEnoughCoinsMessageTimer;
    private bool notEnoughCoinsMessageOn = false;

    private PointData pointData;



    //for managing developer console
    public GameObject developerConsoleRequest;
    public GameObject developerConsole;
    public TMP_InputField password;
    public TMP_InputField command;
    public GameObject messageObject;
    public Text messageText;
    private float messageDuration = 3f;
    private string developerPassword = "GebCaleb12Dev191";
    // private string getCoinCode = "{s95O";
    // private string resetStoreCode = "zQ041";
    private string getCoinCode = "getCoin";
    private string resetStoreCode = "resetStore";
    private bool commanded = false; //so that a command doesn't execute every single update()
    private bool denied = false; //to prevent user from activating console after getting denied
    private bool _checked = false; //so that passowrd doesn't keep on getting checked


    //for managing bounty rewards
    public TMP_InputField rewardCodeInput;
    public GameObject rewardMessage;
    public TextMeshProUGUI rewardMessageText;
    
    private string[] rewardCodes = new string[]{"Dh]8'w@x2A4L" , "}-4@9A7?e]Jk", "6Nc2q(0<t", "9-p^9R82N", "BFz-l3o1|3n-&E", 
        "Y0£Ai<X!U#@79/7", "g}Z~63B3", "o0jC=b14", "772s%OjSi#%", "X5x^T88]7/["
    };

    private float messageTimer = 0f;


    

    void Start()
    {
        pointData = SaveSystem.getPointData();
        originalNotEnoughCoinsMessageTimer = notEnoughCoinsMessageTimer;
        coinsDisplayText.text = "Total Coins: " + pointData.getCoins();
        PlayerPrefs.SetInt("developerAccessRequest", 0);  
    }

    void Update(){
        if (notEnoughCoinsMessageOn){
            notEnoughCoinsMessageTimer -= Time.deltaTime;

            if (notEnoughCoinsMessageTimer <= 0){
                notEnoughCoinsMessageOn = false;
                notEnoughCoinsMessage.SetActive(false);
                notEnoughCoinsMessageTimer = originalNotEnoughCoinsMessageTimer;
            }
        }

        if (password.text.Length >= developerPassword.Length && !denied && !_checked) {
            _checked = true;
            checkDeveloperPassword(password.text);
        }
        if (command.text.Equals(getCoinCode) && !commanded) { //get 1000 coins
            commanded = true;
            giveMoney();
            deactivateDeveloperConsole();
        }

        else if (command.text.Equals(resetStoreCode) && !commanded) { //reset store
            commanded = true;
            resetStore();
        }

        // Debug.Log(command.text);

    }

    //called when item is bought 
    public void deductCoins(int amount){
        int originalAmount = pointData.getCoins();
        if (originalAmount >= amount){
            int newCoinAmount = originalAmount - amount;
            pointData.setCoins(newCoinAmount);
            SaveSystem.savePointData(newCoinAmount);
            coinsDisplayText.text = "Total Coins: " + newCoinAmount;
        }
        
        else{
            notEnoughCoinsMessage.SetActive(true);
            notEnoughCoinsMessageOn = true;
        }
    
    }

    //for store implementation testing
    public void addCoins(int amount){
        int newCoinAmount = pointData.getCoins() + amount;
        pointData.setCoins(newCoinAmount);
        SaveSystem.savePointData(newCoinAmount);
        coinsDisplayText.text = "Total Coins: " + newCoinAmount;
    }

    public void enableDeveloperConsole() {
        if (!denied) {
            developerConsoleRequest.SetActive(true);
        }
    }

    public void checkDeveloperPassword(string pass) {
        if (pass.Equals(developerPassword)) { //access granted
            developerConsole.SetActive(true);
        }

        else if (!commanded) { //access denied
            denied = true;
            deactivateDeveloperConsole();
            giveMessage("Access has been denied, dear imposter");
        }

        _checked = false;
    }

    public void deactivateDeveloperConsole() {
        developerConsoleRequest.SetActive(false);
    }

    private void giveMoney() {
        addCoins(1000);
        deactivateDeveloperConsole();
        giveMessage("1000 coins have been granted!");
        denied = false;
        commanded = false;
        command.text = "";
    }

    private void resetStore() {
        ShopStatus resetScript = FindObjectOfType<ShopStatus>();
        resetScript.resetItems();
        deactivateDeveloperConsole();
        giveMessage("store has been reset!");
        denied = false;
        commanded = false;
        command.text = "";
    }

    private void giveMessage(string message) {
        messageObject.SetActive(true);
        messageText.text = message;
        // messageText.GetComponent<Text>().text = message;
        StartCoroutine("message");             
    }

    IEnumerator message(){
        yield return new WaitForSeconds(messageDuration);
        messageObject.SetActive(false);
    }


//for bounties
    public void checkRewardCode() {
        IAPData data = SaveSystem.getIAPData();
        for (int i = 0; i < rewardCodes.Length; i++) { //code is correct
            string code = rewardCodeInput.text;
            if (rewardCodes[i].Equals(code)) {
                Debug.Log("works?");
                if (data.getCodeStatus(i)) {
                    data.setCodeStatus(i);
                    //reward message
                    addCoins(100);
                    giveRewardMessage("*100 coins have been granted!");
                    SaveSystem.saveIAPData(data);
                }

                else {
                    giveRewardMessage("*Code has already been used");
                }  

                return;
            }
        }

        giveRewardMessage("*Code is not correct");
    }

    private void giveRewardMessage(string message) {
        rewardMessage.SetActive(true);
        rewardMessageText.text = message;
        // StartCoroutine("rewardMessageC");
    }

    IEnumerator rewardMessageC(){
        yield return new WaitForSeconds(messageDuration);
        rewardMessage.SetActive(false);
    }
}
