using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conditionalBarrelPlatformScript : MonoBehaviour
{

    // for making platform trigger
    private Collider collider;

    //for changing material
    public Material originalMaterial;
    public Material triggerMaterial;
    
    void Start(){
        collider = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "Player"){
            collider.isTrigger = true;
            GetComponent<Renderer>().material = triggerMaterial;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player"){
            collider.isTrigger = false;
            GetComponent<Renderer>().material = originalMaterial;
        }
    }

    //problem: player is always colliding with this platform
}
