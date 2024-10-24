using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class rightMovementScript : MonoBehaviour
{
    public bool getInput(){
        return Input.GetMouseButton(0) && gameObject.tag == "rightButton";
    }

    public void OnButtonClick(){
        string buttonName = gameObject.name;
    
        // Debug.Log("Button clicked: " + buttonName);
    }
}
