using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidescript : MonoBehaviour
{
    private ballScript ballscript;
    private SceneManagerScript sceneScript;

    private float time = 0;
    private bool right = false;
    private bool freeze;
    private int speed;
    private float changeTime;
    private int diff = 0;

    private int minSpeed;
    private int maxSpeed;

    //side movement speeds per difficulty
    private int[] minSpeeds = new int[]{1, 2, 4};
    private int[] maxSpeeds = new int[]{2, 5, 10};


    void Awake() {
        sceneScript = FindObjectOfType<SceneManagerScript>();
        diff = sceneScript.getDifficulty();

        //set min and maxspeed based of difficulty
        if (diff != -1) {
            minSpeed = minSpeeds[diff];
            maxSpeed = maxSpeeds[diff];
        }


    }




    void Start()
    {
        ballscript = FindObjectOfType<ballScript>();
        if (Random.Range(0, 2) == 0)
        {
            right = true;
        }
        
        // speed = Random.Range(2, 6);
        speed = Random.Range(minSpeed, maxSpeed);

        if (speed >=5)
            changeTime = 2f;
        else if (speed == 4)
            changeTime = 3f;
        else if (speed == 3)
            changeTime = 4f;
        else if (speed == 2)
            changeTime = 6f;
        else if (speed == 1) {
            changeTime = 7f;
        }
    }

    void FixedUpdate()
    {   
        freeze = ballscript.getFreeze();
        if (!(freeze))
        {
            time += Time.deltaTime;

            if (time > changeTime)
            {
                time = 0;
                right = !(right);
            }

            if (right)
            {
                transform.Translate(Time.deltaTime * speed, 0, 0);
            }

            if (!(right))
            {
                transform.Translate(-Time.deltaTime * speed, 0, 0);
            }
        }
    }
}
