using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startController : MonoBehaviour
{
    private SceneManagerScript scene;

    void Start()
    {
        scene = FindObjectOfType<SceneManagerScript>();
    }

    void Update()
    {
        //starts game for keyboard players
        if (Input.GetKey("joystick button 1") || Input.GetKeyDown("r"))
        {
            scene.next();
        }

        //starts game for mobile players
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            scene.next();
        }
    }
}
