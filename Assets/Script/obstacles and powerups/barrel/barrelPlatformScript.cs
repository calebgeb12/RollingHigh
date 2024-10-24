using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barrelPlatformScript : MonoBehaviour
{
    public obstacleScript obstaclescript;
    private float xPos, yPos, zPos;
    private float yRot; 

    public float xOffset;
    public float yOffset;
    public float zOffset;
    public static int num; //for testing

    private float yOffset2 = 0f;
    private float yPos2;


    //matching platform rotation component
    private GameObject currentPlatform;
    private int colorIndex;
    public Material[] platformMaterials;
    public newPlatformScript newplatformscript;
    private bool barrelChild = true;

    void Awake() {
        num++;
    }

    void Start(){
        //determine how to find color of new platform
        newplatformscript = FindObjectOfType<newPlatformScript>();
        // colorIndex = newplatformscript.getCurrentPlatformColor();
        currentPlatform = newplatformscript.getCurrentPlatform(transform.position.z, num);
        colorIndex = currentPlatform.GetComponent<worldTiltScript>().color;
        gameObject.GetComponent<MeshRenderer>().material = platformMaterials[colorIndex];
        gameObject.transform.parent = currentPlatform.transform;
        gameObject.transform.parent = currentPlatform.transform;

        // Debug.Log(num + ": " + currentPlatform);
    }

    void Update()
    {
        // Debug.Log(transform.position.z);
    //update position of barrel platform based on obstacle to mimic having the obstacle as the parent (I'm assuming complexity arised when making obstacle parent of its platform)
       
        if (barrelChild){ //position only updates if it is part of the barrel

            if (obstaclescript == null) {
                barrelChild = false; 
                Start();
            }

            else {
                xPos = obstaclescript.getXPosition();
                yPos = obstaclescript.getYPosition();
                zPos = obstaclescript.getZPosition();
                transform.localPosition = new Vector3(xPos - xOffset, yOffset2, zPos - zOffset);

            }
        }

        yPos2 = currentPlatform.GetComponent<worldTiltScript>().getYPosition();
        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        // yRot = obstaclescript.getYRotation();
        // Debug.Log(yRot);

        // transform.position = new Vector3(xPos - xOffset, yPos - yOffset, zP7os - zOffset);

        // transform.locaPosition.y = yOffset2;
        // transform.localRotation = Quaternion.Euler(new Vector3(0f, yRot, 0f));
    }

    public void destroyPlatform(){
        Destroy(gameObject);
    }

    public void seperateChildPlatform(){
        barrelChild = false;
        
        xPos = obstaclescript.getXPosition();
        Start(); //needed when barrel hits platform and player at the same time
        yPos2 = currentPlatform.GetComponent<worldTiltScript>().getYPosition();
        zPos = obstaclescript.getZPosition();

        obstaclescript.destroy();
        Start();
        transform.localPosition = new Vector3(xPos - xOffset, yPos2, zPos - zOffset);
    }

    public void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "newPlatformTrigger"){
            obstaclescript.changeParent();
            Start();
        }
    }
}
