using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayIntro : MonoBehaviour
{
    void Start()
    {
        IntroPlayedVerifier introData = SaveSystem.getIntroData();

        if (!introData.getIntroPlayed()) {
            SceneManager.LoadScene(2); 
        }
    }
}
