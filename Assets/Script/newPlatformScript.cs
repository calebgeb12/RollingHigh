    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPlatformScript : MonoBehaviour
{
    //scripts
    public ballScript ballscript;
    public obstacleScript obstaclescript;


    public List<GameObject> platformTypes = new List<GameObject>();
    private GameObject platform;
    
    private GameObject currentPlatform = null;
    private GameObject previousPlatform = null;
    private int currentPlatformColor;
    public List<GameObject> allPlatforms = new List<GameObject>();

    private List<float> rotationParameters = new List<float>();
    private worldTiltScript worldtiltscript;

    public void createPlatform(float zVal)
    {
        int num = Random.Range(0, platformTypes.Count);

        // num = 2; //for instantiating specific platform

        platform = Instantiate(platformTypes[num]);
        platform.transform.position = new Vector3(0, 0, zVal);
        allPlatforms.Add(platform);

        // Debug.Log("spawning");
    }



///attempt at matching barell and platform rotation

    //returns current platform parameters
    public GameObject getCurrentPlatform(float zPosition)
    {
        int currentPlatformIndex = allPlatforms.Count - 1;

        ////determines which platform the barrel should rotate with 
        while (currentPlatformIndex != -1)
        {
            worldtiltscript = allPlatforms[currentPlatformIndex].GetComponent<worldTiltScript>();
            float currentPlatformZPosition =  worldtiltscript.getZPosition();
            currentPlatformColor = worldtiltscript.color;
            // Debug.Log(zPosition - 115 + "---" + currentPlatformZPosition + "---" + zPosition + 115);
            if (currentPlatformZPosition > zPosition - 115 && zPosition + 115 > currentPlatformZPosition)
            {
                // Debug.Log("platform position: " + currentPlatformZPosition + "    barrel position: " + zPosition);

                // currentPlatform = allPlatforms[currentPlatformIndex];
                return allPlatforms[currentPlatformIndex];
            }

            currentPlatformIndex--;
        }

        // Debug.Log("returning null");

        return null;
    }

//for testing
    public GameObject getCurrentPlatform(float zPosition, int num)
    {
        int currentPlatformIndex = allPlatforms.Count - 1;

        ////determines which platform the barrel should rotate with 
        while (currentPlatformIndex != -1)
        {
            worldtiltscript = allPlatforms[currentPlatformIndex].GetComponent<worldTiltScript>();
            float currentPlatformZPosition =  worldtiltscript.getZPosition();
            currentPlatformColor = worldtiltscript.color;
            // Debug.Log(num + ":    " + (zPosition - 115) + " to " + currentPlatformZPosition + " to " + zPosition + 115);
            if (currentPlatformZPosition > zPosition - 115 && zPosition + 115 > currentPlatformZPosition)
            {
                // Debug.Log("current platform found");

                // currentPlatform = allPlatforms[currentPlatformIndex];
                return allPlatforms[currentPlatformIndex];
            }

            currentPlatformIndex--;
        }

        // Debug.Log(num + "   is returning null");

        return null;
    }


    //returns current platform
    // public GameObject getCurrentPlatform(){
    //     return currentPlatform;
    // }

    // public int getCurrentPlatformColor(){
    //     return currentPlatformColor;
    // }





    
}
