using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject target;
    public float xOffset, yOffset, zOffset;
    public GameObject[] balls;
      
    void Update()
    {
        transform.position = target.transform.position + new Vector3(xOffset, yOffset, zOffset);
        transform.LookAt(target.transform.position);   
    }

    public void lookAtBall(int currentBallIndex, GameObject ball){
        target = ball;
    }   
}
