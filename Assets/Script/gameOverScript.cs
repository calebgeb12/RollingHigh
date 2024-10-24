using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Schepens.UtilScripts;

public class gameOverScript : MonoBehaviour
{
    public SceneManagerScript scene;
    public AudioSource source;
    public AudioClip gameOverAudio;
    private PointData pointdata;

    //UI
    public Text scoreText;
    public Text highScoreText;
    public Text coinText;
    public Text totalCoinText;

    public GameObject pauseButton;
    public GameObject inGameObjects;

    //for intersitial ads
    public InterstitalAds interstitalAds;
    private int playsPerAd = 5; //sets frequency of interstitial ads
    public Initialize initialize; 
    private bool adsDisabled = false; //will be used later to implement ad disabling buying option

    //for dealing with double coins
    private int dbCoinCount = 0;
    private bool coinsDoubled = false;

    public void gameOver(int score, int coins)
    {
        //check if ads are disabled
        IAPData adData = SaveSystem.getIAPData();
        adsDisabled = adData.getNoAds();

        source = GetComponent<AudioSource>();
        gameObject.SetActive(true);
        scoreText.text = "> Final Score: " + score;
        source.PlayOneShot(gameOverAudio);

        //control behavior of high score (setting and retrieving)
        //this will all be removed or at the very least highly modified later

        pauseButton.SetActive(false);
        inGameObjects.SetActive(false);

        PointData pointData = SaveSystem.getPointData();
        int highScore = pointData.getOriginalHighScore();

        //play ad every x plays
        int plays = PlayerPrefs.GetInt("numOfPlays", 0) + 1;
        PlayerPrefs.SetInt("numOfPlays", plays);
        if (plays % playsPerAd == 0  && !adsDisabled) { //play ad every three plays'
            // interstitalAds.Awake();
            interstitalAds.LoadThenShow();
        }

        // Debug.Log(PlayerPrefs.GetInt("numOfPlays", 0));
        

        //make sure to test if the updated high score is returned or if anything is returned at all

        if (score > highScore)
        {
            highScoreText.text = "> NEW High Score: " + score;
        }

        else
        {
            highScoreText.text = "> High Score: " + highScore;
        }

        if (coinsDoubled) {
            int coins2 = coins - (dbCoinCount / 2);
            coinText.text = "> Coins Collected: " + (dbCoinCount + coins2) + " (Doubled)";
        }

        else {
            coinText.text = "> Coins Collected: " + coins;

        }

        totalCoinText.text = "> Total Coins: " + pointData.getCoins();
    }

    public void doubleCoinsText(int coins){
        PointData pointData = SaveSystem.getPointData();
        coinText.text = "> Coins Collected: " + (coins * 2) + " (Doubled)";
        totalCoinText.text = "> Total Coins: " + pointData.getCoins();
        dbCoinCount = coins * 2;
        coinsDoubled = true;
    }
}
