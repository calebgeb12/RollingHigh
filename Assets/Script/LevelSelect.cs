using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public GameObject levelScreen;
    public GameObject title;
    public GameObject homeScreenButtons;

    public void enableLevelScreen() {
        levelScreen.SetActive(true);
        title.SetActive(false);
        homeScreenButtons.SetActive(false);
    }

    public void disableLevelScreen() {
        levelScreen.SetActive(false);
        title.SetActive(true);
        homeScreenButtons.SetActive(true);
    }
}
