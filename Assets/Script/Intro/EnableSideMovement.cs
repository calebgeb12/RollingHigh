using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSideMovement : MonoBehaviour
{
    private bool enableMovement = false;

    public bool getMovement(){
        return enableMovement;
    }

    public void setMovement(){
        enableMovement = true;
    }
}
