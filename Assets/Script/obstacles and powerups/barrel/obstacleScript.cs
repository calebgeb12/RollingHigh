using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class obstacleScript : MonoBehaviour
{
    //scripts
    private ballScript ballscript;
    private newPlatformScript newplatformscript;

    private Rigidbody rb;
    private BoxCollider bc;
    private bool freeze;
    private float playerPosition;
    private float destroyTime = 70f;
    private float time;
    private float speedTowardPlayer;
    private bool freezeRotation = false;

    private bool parentPlatformFound = false;

    /////attempt at matching barrel and platform rotation
    private List<float> rotationParameters = new List<float>();
    private bool left = false;
    
    public barrelPlatformScript barrelplatformscript;
    public GameObject floatingPlatform;

    private GameObject currentPlatform;
    private worldTiltScript currentPlatformScript;
    private int currentPlatformColor;
    private bool floatingPlatformActive = false;

    //y position spawn
    private float yPos = 5f;


    void Start()
    {
        ballscript = FindObjectOfType<ballScript>();
        // barrelplatformscript = FindObjectOfType<barrelPlatformScript>();

        speedTowardPlayer = Random.Range(-200f, -50f); 
        // speedTowardPlayer = -50;
        
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        
        ////sucessful attempt at matching barrel rotation with platform rotation
        newplatformscript = FindObjectOfType<newPlatformScript>();
        currentPlatform = newplatformscript.getCurrentPlatform(transform.position.z);
        
        gameObject.transform.parent = currentPlatform.transform;
        transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);

        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
    }

    void Update()
    {
        // Debug.Log(transform.localEulerAngles.y);
        time += Time.deltaTime;
        if (time > destroyTime)
        {
            startDestroy();
        }
        // Debug.Log(transform.position.z);
    }

    void FixedUpdate()
    {   

        freeze = ballscript.getFreeze();
        if (freeze)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.freezeRotation = true;
        }

        else
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.freezeRotation = false; 

            rb.AddForce(new Vector3(0f, 0f, speedTowardPlayer));
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
           if (!floatingPlatformActive) { 
                destroy();
            }

            else {
                startDestroy();
            }
        }

        if (other.gameObject.tag == "barrelPlatform" || other.gameObject.tag == "platformPiece" || other.gameObject.tag == "barrelFloatingPlatform"){
            floatingPlatform.SetActive(true);
            floatingPlatformActive = true;
        }

        if (!(other.gameObject.tag == "barrelPlatform" || other.gameObject.tag == "platformPiece" || other.gameObject.tag == "barrelFloatingPlatform")) {
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "barrelDeleter"){
            startDestroy();
        }
        
        if (other.gameObject.tag == "spikes"){
            startDestroy();
        }
    }

    void startDestroy(){
        if (!floatingPlatformActive) {
            Destroy(gameObject);
        }

        else {
            barrelplatformscript.seperateChildPlatform();
        }
    }

    public void changeParent() {
        currentPlatform = newplatformscript.getCurrentPlatform(transform.position.z);
        gameObject.transform.parent = currentPlatform.transform;
        // transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
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
}
