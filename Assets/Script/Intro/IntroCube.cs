using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCube : MonoBehaviour
{
    public AudioSource hitSound;
    void Start(){

    }

    void OnCollisionEnter (Collision other){
        if (other.gameObject.tag == "Player"){
            hitSound.Play();
            Destroy(gameObject);
        }
    }
}
