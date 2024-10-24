using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezeRayScript : MonoBehaviour
{
    public GameObject floatingPlatform;    
    private float range = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // floatingPlatform.SetActive(false);
    }

//new implementation of raycasting
    void Update() {
        Vector3 rayDirection = new Vector3(0, -1, 0);
        if (Physics.Raycast (transform.position, transform.TransformDirection (rayDirection), out RaycastHit hitinfo, range)){
            // Debug.Log(hitinfo.collider.tag);
            if (hitinfo.collider.tag == "barrelPlatform"){
                // Debug.DrawRay (transform.position, transform.TransformDirection (rayDirection) * hitinfo.distance, Color.green);
                floatingPlatform.SetActive(true);
            }

            else {
                // Debug.DrawRay (transform.position, transform.TransformDirection (rayDirection) * hitinfo.distance, Color.blue);
                // floatingPlatform.SetActive(false);
            }
        }

        else {
            // Debug.Log("hit nothing");
            Debug.DrawRay (transform.position, transform.TransformDirection (rayDirection) * hitinfo.distance, Color.red);
            // Debug.DrawRay (transform.position, transform.TransformDirection (rayDirection) * range, Color.green);
        }
    }


}
