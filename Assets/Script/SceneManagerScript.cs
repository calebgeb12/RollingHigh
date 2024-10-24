using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    private pauseScript pausescript;
    private static int diff = -1;

    void Start(){        
        pausescript = GetComponent<pauseScript>();
        Application.targetFrameRate = 60;        
    }
    
    public void next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void reload()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        // StartCoroutine("wait");
    }

    public void loadGame(int num) {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        diff = num;
    }

    public void loadIntro(){
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(3.0f);
        reload();
    }

    public void load(int index)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
    }

    public void goHome(){
        SceneManager.LoadScene(0);
        pausescript.continueGame();
    }

    public void sceneSelect()
    {
        SceneManager.LoadScene("SelectScreen");
    }

    public int getDifficulty() {
        return diff;
    }
}
