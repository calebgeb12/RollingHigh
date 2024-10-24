using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    private float range = 20f;
    void Update(){
        if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out RaycastHit hitinfo, range)){
            Debug.Log(hitinfo.collider.tag);
            Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * hitinfo.distance, Color.red);

        }

        else {
            Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * range, Color.green);
        }
    }
}
