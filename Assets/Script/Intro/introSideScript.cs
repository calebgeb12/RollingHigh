using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introSideScript : MonoBehaviour
{
    ballScript ballscript;
    private EnableSideMovement enableScript; 
    float time = 0;
    bool right = false;
    bool freeze;
    int speed;
    float changeTime;
    bool enableMovement = false;

    void Start()
    {
        ballscript = FindObjectOfType<ballScript>();
        enableScript = FindObjectOfType<EnableSideMovement>();

        
        if (Random.Range(0, 2) == 0)
        {
            right = true;
        }

        speed = Random.Range(1, 3);
        Debug.Log(speed);


        if (speed >=5)
            changeTime = 2f;
        else if (speed == 4)
            changeTime = 3f;
        else if (speed == 3)
            changeTime = 4f;
        else if (speed == 2)
            changeTime = 6f;
    }

    void FixedUpdate()
    {   
        freeze = ballscript.getFreeze();
        enableMovement = enableScript.getMovement();

        if (!(freeze) && enableMovement)
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
