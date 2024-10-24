using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthScript : MonoBehaviour
{
    public Rigidbody rb;
    public float xRotate;
    public float yRotate;
    public float zRotate;
    private float playerPosition;
    private ballScript ballscript;

    //for floating platform
    public newPlatformScript newplatformscript;
    private worldTiltScript currentPlatformScript;
    public freezePlatformScript freezeplatformscript;
    public GameObject floatingPlatform;

    private GameObject currentPlatform;
    private int currentPlatformColor;
    private bool floatingPlatformActive = false;
    private bool seperating = false;

    //for freeze powerup
    private bool freeze;
    public bool freezeXPos;
    public bool freezeYPos;
    public bool freezeZPos;
    public bool freezeXRot;
    public bool freezeYRot;
    public bool freezeZRot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballscript = FindObjectOfType<ballScript>();

        newplatformscript = FindObjectOfType<newPlatformScript>();
        currentPlatform = newplatformscript.getCurrentPlatform(transform.position.z);
        currentPlatformScript = currentPlatform.GetComponent<worldTiltScript>();
        currentPlatformColor = currentPlatformScript.color;

        gameObject.transform.parent = currentPlatform.transform;
    }

    void FixedUpdate()
    {
        freeze = ballscript.getFreeze();

        if (freeze){
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.freezeRotation = true;
        }

        else {

            //unfreeze specific axis based on object
            if (!freezeXPos){
                rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            }

            if (!freezeYPos){
                rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            }

            if (!freezeZPos){
                rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            }

            if (!freezeXRot){
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
            }

            if (!freezeYRot){
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
            }

            if (!freezeZRot){
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
            }

            rb.useGravity = true;
            this.transform.Rotate(xRotate, yRotate, zRotate);
        }

        playerPosition = ballscript.getPosition();

        //destroy if player is far away
        if (playerPosition - gameObject.transform.position.z > 200|| gameObject.transform.position.z < -50)
        {
            freezeplatformscript.destroyPlatform();
            Destroy(gameObject);
        }


    }

    void OnCollisionEnter(Collision other)
    {        
        if (other.gameObject.tag == "Player")
        {
            if (!floatingPlatformActive && !seperating) {
                Destroy(gameObject);
            }

            else {
                seperating = true;
                freezeplatformscript.seperateChildPlatform();
            }
        }        

        if (other.gameObject.tag == "barrelPlatform" || other.gameObject.tag == "platformPiece"){
            floatingPlatform.SetActive(true);
            floatingPlatformActive = true;
        }
    }

    public void destroy(){
        Destroy(gameObject);
    }

    public float getXPosition()
    {
        // return transform.position.x;
        return transform.localPosition.x;
    }

    public float getYPosition()
    {
        // return transform.position.y;
        return transform.localPosition.y;
    }

    public float getZPosition()
    {
        // return transform.position.z;
        return transform.localPosition.z;
    }

    public int getPlatformColor(){
        return currentPlatformColor;
    }
}
