using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagedOverlayScript : MonoBehaviour
{
    private float displayTime = .5f;
    private float originalDisplayTime;

    void Start(){
        originalDisplayTime = displayTime;
    }

    public void enable()
    {
        this.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.enabled == true){
            displayTime -= Time.deltaTime;
        }

        if (displayTime <= 0){
            this.enabled = false;
            displayTime = originalDisplayTime;
        }
    }
}
