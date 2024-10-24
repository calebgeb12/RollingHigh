using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeScript : MonoBehaviour
{

    // public AudioSource spikeSound;
    public AudioClip spikeSound;
    private float spikeSoundVolume = 1f;


    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "block"){

            // spikeSound.Play();
            // spikeSound2.Play();

            AudioSource.PlayClipAtPoint(spikeSound, transform.position, spikeSoundVolume);
            
            Destroy(gameObject);
        }
    }
}
