using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class abilities
{

    //freeze ball abilities
    private float freezeTime = .2f;
    private float freezeCooldown = 12f;

    private int freezeTimeIncreaseCost = 5;
    private int freezeCooldownIncreaseCost = 5;

    //spike ball 
    private int numOfSpikes = 1;
    private int spikeCost = 3;


    //spike ball abilities



//freeze getter methods
    public float getFreezeTime(){
        return freezeTime;
    }

    public float getFreezeCooldown(){
        return freezeCooldown;
    }

    public int getFreezeTimeCost(){
        return freezeTimeIncreaseCost;
    }

    public int getFreezeCooldownCost(){
        return freezeCooldownIncreaseCost;
    }

//freeze setter methods
    public void setFreezeTime(float time){
        freezeTime = time;
        SaveSystem.saveAbilitiesData(this);
    }
    
    public void setFreezeCooldown(float cooldown){
        freezeCooldown = cooldown;
        SaveSystem.saveAbilitiesData(this);
    }

    public void setFreezeTimeCost(){
        freezeTimeIncreaseCost += (int) (freezeTimeIncreaseCost * .3); 
        SaveSystem.saveAbilitiesData(this);
    }

    public void setFreezeCooldownCost(){
        freezeCooldownIncreaseCost += (int) (freezeCooldownIncreaseCost * .5); 
        SaveSystem.saveAbilitiesData(this);
    }

//spike getter method
    public int getNumOfSpikes(){
        return numOfSpikes;
    }

    public int getSpikeCost(){
        return spikeCost;
    }


//spike setter methods
    public void setNumOfSpikes(int spikes){
        numOfSpikes = spikes;
        SaveSystem.saveAbilitiesData(this);
    }

    public void setSpikeCost(){
        spikeCost += (int) (spikeCost * .4);
        SaveSystem.saveAbilitiesData(this);
    }


}
