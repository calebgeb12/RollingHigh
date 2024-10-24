using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsScript : MonoBehaviour
{
    private volumeManagerScript volumescript;

    //audio
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    public AudioSource backgroundMusic;

    //graphics
    private Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    
    public GameObject settingsMenu;   
    public Toggle toggle;

    void Start()
    {
        volumescript = GetComponent<volumeManagerScript>();

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

        //resolution stuff
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>(); 

        if (PlayerPrefs.HasKey("instrVisibility")){
            if (PlayerPrefs.GetInt("instrVisibility") == 1){
                toggle.isOn = true;
            }

            else {
                toggle.isOn = false;
            }
        }

        else {
            toggle.isOn = true;
            PlayerPrefs.SetInt("instrVisibility", 1);
        }
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
//activation settings
    public void activateSettings(){
        settingsMenu.SetActive(true);
    }

    public void deactivateSettings(){
        settingsMenu.SetActive(false);
    }

//graphcis quality settings

    //graphics quality
    public void setQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex, false);
        // Debug.Log(QualitySettings.GetQualityLevel());
    }

    //resolution

    public void setResolution(int resolutionIndex){
        // Debug.Log(resolutionIndex);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setToggle(){
        if (toggle.isOn){
            PlayerPrefs.SetInt("instrVisibility", 1);
        }

        else {
            PlayerPrefs.SetInt("instrVisibility", 0);
        }
    }
    

//audio settings
    public void changeVolume(){
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("gameVolume", volumeSlider.value);
    }

    public void changeMusicVolume(){
        backgroundMusic.volume = musicVolumeSlider.value / 10;
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value / 10);
    }




    


  
}
