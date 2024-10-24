using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezeOverlaySpinner : MonoBehaviour
{
    void FixedUpdate()
    {
        this.transform.Rotate(0f, 0f, 4f);
    }
}
