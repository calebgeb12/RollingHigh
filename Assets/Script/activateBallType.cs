using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateBallType : MonoBehaviour
{

    public ShopStatus shopStatus;
    public cameraScript camerascript;
    private int currentBallIndex;
    public GameObject[] balls;

    void Awake(){
        currentBallIndex = shopStatus.getCurrentBallType();
        // currentBallIndex = 0; //for manually setting ball type
        balls[currentBallIndex].SetActive(true);

        // set camera ball reference
        camerascript.lookAtBall(currentBallIndex, balls[currentBallIndex]);
    }

    public int getCurrentBallTypeIndex(){
        return currentBallIndex;
    }
}
