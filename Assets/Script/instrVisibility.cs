using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class instrVisibility : MonoBehaviour
{
    public Toggle toggle;

    void Start()
    {
        if (PlayerPrefs.HasKey("instrVisibility")){
            if (PlayerPrefs.GetInt("instrVisibility") == 1){
                gameObject.SetActive(true);
                toggle.isOn = true;
            }
                
            else{
                gameObject.SetActive(false);
                toggle.isOn = false;
            }
        }

        else 
            PlayerPrefs.SetInt("instrVisibility", 1);
    }

    public void changeVisibility(){
        if (!toggle.isOn){
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("instrVisibility", 0);
        }

        else{
            gameObject.SetActive(true);
            PlayerPrefs.SetInt("instrVisibility", 1);
        }
    }
    


}
