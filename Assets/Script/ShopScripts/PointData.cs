using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointData
{
    private int highScore; 
    private int coins;
    private int originalHighScore;
    private bool introPlayed = false;
    
    public PointData(){
        
    }

    //for updating high score and coins
    public PointData(ballScript ballscript){
        Start();

        //compare highs score with new score
        int newScore = (int) ballscript.getScore();
        if (newScore > highScore){
            highScore = newScore;
        }

        //updates coin amount
        coins += ballscript.getCoins();
    }

    //specifically for doubling coins
    public PointData(ballScript ballscript, int coins2){
        Start();

        //compare highs score with new score
        int newScore = (int) ballscript.getScore();
        if (newScore > highScore){
            highScore = newScore;
        }

        //updates coin amount
        coins += coins2;
    }

    //for updating coin amount
    public PointData(int newCoinAmount){
        Start();
        coins = newCoinAmount;
    }

    public PointData(int highScore, int coins){
        this.highScore = highScore;
        this.coins = coins;
    }

    //sets initial values to be used throughout the game
    void Start(){ 
        PointData initialData = SaveSystem.getPointData();
        highScore = initialData.highScore;
        originalHighScore = initialData.highScore;
        coins = initialData.coins;
    }

    public int getHighScore(){
        return highScore;
    }

    public int getOriginalHighScore(){
        return originalHighScore;
    }

    public int getCoins(){
        
        return this.coins;
    }

    public void setCoins(int coins){
        this.coins = coins;
    }

    public void verifyIntroPlayed(){
        introPlayed = true;
    }
}


