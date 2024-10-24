using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pauseScript : MonoBehaviour
{
    //scripts
    private volumeManagerScript volumescript;

    //rand pause stuff
    public GameObject pauseMenu;
    private bool allowPause = true;
    private bool allowSlowDown = true;

    //sound stuff
    public List<AudioSource> inGameAudioList = new List<AudioSource>();
    private List<float> volumeList = new List<float>();
    public AudioSource staticSound;

    //pause and resume buttons
    public GameObject pauseButton;
    public GameObject resumeButton;

    //things to hide during pause
    public GameObject inGameScore;
    public GameObject inGameLives;
    public GameObject inGameCoins;
    public GameObject fpsCounter; 
    public GameObject joyStick; 
    public GameObject startInstructions; 
    public GameObject dropInstructions; 

    public GameObject inGameObjects;
    public GameObject pauseObjects;
    public GameObject settingsObjects;

        //things that might already be hidden (need to check if they are hidden to avoid making them active when they should be inactive)    
        private bool fpsCounterOn;
        private bool startInstructionsOn; //check instance where instructions are removed from screen b/c of time limit
        private bool dropInstructionsOn;



 

    //decided against use of awake due to issues with sound resetting
    // void Awake(){
    //     //indirectly used in 'scenemanagerscript.start()'
    //     volumescript = GetComponent<volumeManagerScript>();
        
    //     //need to have these in awake because they are iterated in 'scenemanagerscript.start()'
    //     foreach (AudioSource audio in inGameAudioList){
    //         volumeList.Add(audio.volume);
    //         Debug.Log(audio.volume);
    //     }
    // }

    void Start()
    {
        volumescript = GetComponent<volumeManagerScript>();
        
        foreach (AudioSource audio in inGameAudioList){
            volumeList.Add(audio.volume);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (allowPause)
            {
                pauseGame();
                allowPause = false;
            }

            else
            {
                continueGame();
                allowPause = true;
            }
        }

         
        if (Input.GetKeyDown("f")){
            if (allowSlowDown){
                slowDown();
                allowSlowDown = false;
            }

            else{
                continue2();
                allowSlowDown = true;
            }
        }
    }

    public void pauseGame()
    {        
    
    //old implementation
        ////set objects to on or off
        // pauseMenu.SetActive(true);
        // pauseButton.SetActive(false);
        // resumeButton.SetActive(true); 
        // inGameScore.SetActive(false);
        // inGameLives.SetActive(false);
        // inGameCoins.SetActive(false);
        // joyStick.SetActive(false);

             ////determine if certain objects were initially on or off (later returned to their original on off state)
        // if (fpsCounter.activeSelf)
        //     fpsCounterOn = true;
        // if (startInstructions.activeSelf)
        //     startInstructionsOn = true;
        // if (dropInstructions.activeSelf)
        //     dropInstructionsOn = true;

        //// previous if statements needed to determine their original on off state
        // fpsCounter.SetActive(false);   
        // startInstructions.SetActive(false);
        // dropInstructions.SetActive(false);


    //new implementation
        inGameObjects.SetActive(false);
        pauseObjects.SetActive(true);
        settingsObjects.SetActive(false);


        staticSound.volume = .1f;
        StartCoroutine(freezeGameCoroutine());
  }

    public void goToSettings(){
        inGameObjects.SetActive(false);
        pauseObjects.SetActive(false);
        settingsObjects.SetActive(true);
    }

    public void continueGame()
    {
        staticSound.volume = 0f;
        Time.timeScale = 1;

    //old implementation
        // pauseMenu.SetActive(false);
        // pauseButton.SetActive(true);
        // resumeButton.SetActive(false);

        // inGameScore.SetActive(true);
        // inGameLives.SetActive(true);
        // inGameCoins.SetActive(true);  
        // joyStick.SetActive(true);

        int index = 0;
        foreach (AudioSource audio in inGameAudioList){
            audio.volume = volumeList[index];
            index++;
        }

        staticSound.volume = 0f;

        //if these are meant to be on, they are turned on, else, they stay off  
        // if (fpsCounterOn)
        //     fpsCounter.SetActive(true);
        // if (startInstructionsOn)
        //     startInstructions.SetActive(true);
        // if (dropInstructionsOn)
        //     dropInstructions.SetActive(true);

        //changes volume to slider value in pause menu after unpausing

        inGameObjects.SetActive(true);
        pauseObjects.SetActive(false);
        settingsObjects.SetActive(false);
      
        volumescript.changeVolume();
        volumescript.changeMusicVolume();
    }

    public void slowDown(){
        Time.timeScale = .05f;
    }

    public void continue2(){
        Time.timeScale = 1f;
    }

    IEnumerator freezeGameCoroutine(){
        foreach (AudioSource audio in inGameAudioList){
            // volumeList.Add(audio.volume);
            audio.volume = 0f;
            yield return null;
        }

        Time.timeScale = 0f;
    }
}
