using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Schepens.UtilScripts;

public class legacyGameOverScript : MonoBehaviour
{
    
    //game over screen and sound
    public ballScript ballscript;
    public SceneManagerScript scene;
    public Text scoreText;
    public AudioSource source;
    public AudioClip gameOverAudio;
    
    private HighScoreHandler handler = new HighScoreHandler();
    private int score;
    private bool newHighScore = false;

    //name input system
    public GameObject inputField;
    public Text nameText;
    private string letters = "";
    private bool addedScore = false;

    //high score table displaying score system
    public GameObject highScoreTable;
    public List<Text> highScoreTexts = new List<Text>();



//for game over screen
    public void gameOver(int s)
    {
        source = GetComponent<AudioSource>();
        gameObject.SetActive(true);
        score = s;
        scoreText.text = "> Final Score: " + score;
        source.PlayOneShot(gameOverAudio);
        handler.ReadFromFile();
        // handler.WriteToFile();
        int lastItemIndex = handler.scores.Count;
        if (handler.scores.Count != 10 || score > handler.scores[lastItemIndex- 1].score)
        {
            newHighScore = true;
        }
    }

//for input screen
    public void inputName(string letter)
    {
        Debug.Log("inside inputName");
        letters += letter;
        nameText.text = letters + "_";
    }

    public void deleteLetter()
    {   
        if (letters.Length > 0)
        {
            letters = letters.Substring(0, letters.Length - 1);
            nameText.text = letters + "_";
        }
    }

    public void confirmName()
    {
        //if player did not already confirm their name, then this will confirm their name and add it to the high score table
        if (!(addedScore))
        {
            addedScore = true;
            handler.AddScore(letters, score);
            handler.WriteToFile();
            highScoreTable.SetActive(true);
            displayScores();
        }
    }

//for highscore screen
    public void displayScores()
    {
        for (int scoreIndex = 0; scoreIndex < handler.scores.Count; scoreIndex++)
        {
            highScoreTexts[scoreIndex].text = "> " + handler.scores[scoreIndex].initials + "---"  + handler.scores[scoreIndex].score;
        }

        scene.restart();


    }

//for advancing to next scene (mainly)
    void Update()
    {
        if (Input.GetKey("joystick button 1") || Input.GetKeyDown("r"))
        {
            if (newHighScore)
            {
                inputField.SetActive(true);
            }

            else
            {
                StartCoroutine("wait");
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3.0f);
        displayScores();
    }}
