using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerScript : MonoBehaviour
{
    private SceneManagerScript scene;

    void Start()
    {
        scene = FindObjectOfType<SceneManagerScript>();
    }
    void Update()
    {
        if (Input.GetKey("joystick button 1") || Input.GetKeyDown("r"))
        {
            scene.reload();
        }

        // if (Input.GetKey("joystick button 2") || Input.GetKeyDown("m"))
        // {
        //     scene.sceneSelect();
        // }
    }
}
