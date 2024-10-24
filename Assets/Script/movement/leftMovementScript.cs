using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class leftMovementScript : MonoBehaviour
{
    public bool getInput(){
        return Input.GetMouseButton(0) && gameObject.tag == "leftButton";
    }

    public void OnButtonClick(){
        string buttonName = gameObject.name;
    
        Debug.Log("Button clicked: " + buttonName);
    }
}
