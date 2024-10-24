using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldTiltScript : MonoBehaviour
{    

    private SceneManagerScript sceneScript;

    public float rotateSpeed;
    public float minRotateAngle;
    public float maxRotateAngle;
    public int platformType;
    public int color;
    private int diff;

    //titling speed parameters per difficulty
    private float[] medSpeeds = new float[]{10, 10, 20};
    private float[] hardSpeeds = new float[]{20, 20, 25};
    private ballScript ballscript;
    private Rigidbody rb;
    private bool freeze;
    private float adjustedRotateSpeed;
    private bool left = false;

    // private float playerPosition; 
    private float destroyTime = 70f;
    private float time;

    //for preventing powerup piling
    private int buffCount;

    public bool intro = false;

    void Awake() {
        sceneScript = FindObjectOfType<SceneManagerScript>();
        diff = sceneScript.getDifficulty();
        
        if (diff == 0) {
            rotateSpeed = 0f;
        }
        
        else if (diff == 1) {
            rotateSpeed = medSpeeds[platformType];
        }

        else if (diff == 2) {
            rotateSpeed = hardSpeeds[platformType];
        }

        if (intro) {
            rotateSpeed = 0;
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        

        ballscript = FindObjectOfType<ballScript>();
        
    }

    void FixedUpdate()
    {
        // playerPosition = ballscript.getPosition();
        freeze = ballscript.getFreeze();
        time += Time.deltaTime;
        
        // if (time > destroyTime)
        // {
        //     Destroy(gameObject);
        // }
        
        if (!(freeze))
        {
            if (left)
            {
                adjustedRotateSpeed =  rotateSpeed * Time.deltaTime;
                transform.Rotate(0, 0, adjustedRotateSpeed);
                //transform.Rotate(rotate/5, 0, 0);
            }

            else 
            {
                adjustedRotateSpeed = -1 * rotateSpeed * Time.deltaTime;
                transform.Rotate(0, 0, adjustedRotateSpeed);
            }

            if (transform.eulerAngles.z > minRotateAngle && transform.eulerAngles.z < 200)
            {
                left = false;
            }
            
            if (transform.eulerAngles.z < maxRotateAngle && transform.eulerAngles.z > 100)
            {
                left = true;
            }
        }
    }


    public float getZPosition()
    {
        return transform.position.z;
    }
    
    public float getZAngle()
    {
        return transform.rotation.eulerAngles.z;
    }

    public float getRotateSpeed()
    {
        return rotateSpeed;
    }
    

    public float getMinRotateAngle()
    {
        return minRotateAngle;
    }

    
    public float getMaxRotateAngle()
    {
        return maxRotateAngle;
    
    }

    public float getYPosition(){
        return transform.position.y;
    }

    public void setRotateSpeed(float speed){
        rotateSpeed = speed;
    }

//for preventing buff piling

    public void increaseBuffCount() {
        this.buffCount++;
    }

    public int getBuffCount() {
        return this.buffCount;
    }

    public void decreaseBuffCount() {
        this.buffCount--;
    }





}
