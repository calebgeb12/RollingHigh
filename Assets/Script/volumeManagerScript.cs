using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class volumeManagerScript : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    public AudioSource backgroundMusic;
    

    void Start()
    {
        //determine game volume upon start
        if (!PlayerPrefs.HasKey("gameVolume")){
            PlayerPrefs.SetFloat("gameVolume", 1);
            volumeSlider.value = 1;
        }

        else{
            volumeSlider.value = PlayerPrefs.GetFloat("gameVolume");
            changeVolume();
        }

        //determine muisc volume upon start
        if (!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", .1f);
            musicVolumeSlider.value = 1;
        }

        else{
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume") * 10;
            changeMusicVolume();
        }
    }

    public void changeVolume(){
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("gameVolume", volumeSlider.value);
    }

    public void changeMusicVolume(){
        backgroundMusic.volume = musicVolumeSlider.value / 10;
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value / 10);
    }
}
