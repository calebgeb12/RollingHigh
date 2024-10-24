using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class freezePlatformScript : MonoBehaviour
{
    public healthScript freezescript;
    private float xPos, yPos, zPos;
    private float yRot; 

    public float xOffset;
    public float yOffset;
    public float zOffset;

    private float yOffset2 = 0f;
    private float yPos2;


    //matching platform rotation component
    private GameObject currentPlatform;
    private worldTiltScript currentPlatformScript;
    private int colorIndex;
    public Material[] platformMaterials;
    public newPlatformScript newplatformscript;
    private bool seperate = false;

    void Start(){
        //determine how to find color of new platform
        newplatformscript = FindObjectOfType<newPlatformScript>();
        currentPlatform = newplatformscript.getCurrentPlatform(transform.position.z);
        currentPlatformScript = currentPlatform.GetComponent<worldTiltScript>();
        colorIndex = currentPlatformScript.color;
        gameObject.GetComponent<MeshRenderer>().material = platformMaterials[colorIndex];
        gameObject.transform.parent = currentPlatform.transform;
        gameObject.transform.parent = currentPlatform.transform;
    }

    void Update()
    {
        // if (Input.GetKeyDown("e")){
        //     Start();
        // }


    //update position of barrel platform based on obstacle to mimic having the obstacle as the parent (I'm assuming complexity arised when making obstacle parent of its platform)
        
        if (!seperate) {
            if (freezescript == null) {
                seperate = true;
                Start();
            }

            else {
                xPos = freezescript.getXPosition();
                yPos = freezescript.getYPosition();
                zPos = freezescript.getZPosition();
                transform.localPosition = new Vector3(xPos - xOffset, yOffset2, zPos - zOffset);
            }

        }


        yPos2 = currentPlatform.GetComponent<worldTiltScript>().getYPosition();

        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

    }

    public void destroyPlatform(){
        Destroy(gameObject);
    }

    public void seperateChildPlatform(){
        seperate = true;

        xPos = freezescript.getXPosition();
        Start(); //needed when barrel hits platform and player at the same time
        yPos2 = currentPlatform.GetComponent<worldTiltScript>().getYPosition();
        zPos = freezescript.getZPosition();


        transform.localPosition = new Vector3(xPos - xOffset, yPos2, zPos - zOffset);

        freezescript.destroy();
    }

    public void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "newPlatformTrigger"){
            Start();
        }
    }


    /**conceptual trigger: platform about to leave platform piece
        - possible ways of checking
            1) us ontriggerexit to know when barrel exits platform piece
                issue: activates many times 
            2) somehow make platform pieces leave behind trigger when moving
                issue: hard to implement
            3) have trigger platform that is lower than physical platforms, activating this activates barrel platform
                issue: can't be triggered
            4) rays
                issue: don't know how to do this
        **instead of setting floating platform active and inactive, change it's color because then null reference error will arise
**/


}
