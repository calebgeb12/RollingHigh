using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezeButton : MonoBehaviour
{
    public ballScript ballscript;

    void Start(){
        ballscript = FindObjectOfType<ballScript>();
        if (ballscript.getCurrentBallTypeIndex() != 4)
            gameObject.SetActive(false);
    }
}
