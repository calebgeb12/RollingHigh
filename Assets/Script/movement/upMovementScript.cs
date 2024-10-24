using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upMovementScript : MonoBehaviour
{
    public Button button;
    public KeyCode upKey;

    void Start(){
        button.onClick.AddListener(HandleButtonClick);
    }


    public bool getInput(){
        return Input.GetMouseButton(0) && gameObject.tag == "upButton";

    }

    public void OnButtonClick(){
        string buttonName = gameObject.name;
    
        // Debug.Log("Button clicked: " + buttonName);
    }

    void HandleButtonClick()
    {
        // Code to execute when the button is clicked
    }

    void Update(){
        if (Input.GetKeyDown(upKey) && Input.GetMouseButtonDown(0))
        {
            // Trigger the button click event
            button.onClick.Invoke();
            Debug.Log("Button clicked instantly!");
            
        }
    }






}


