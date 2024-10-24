using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    private ballScript ballscript;
    private SceneManagerScript sceneScript;
    public worldTiltScript tiltScript;
    public EnableSideMovement enableSideMovementScript;
    public AudioSource tipSound;

    private float changeTime;
    private float timer = 0f;

    public GameObject[] lessons;
    public GameObject cubes;
    public GameObject redWall;
    public GameObject barrel1;
    public GameObject heart;
    public GameObject coin;

    public GameObject scoreText;
    public GameObject lifeText;
    public GameObject coinText;
    public GameObject scoreArrow;
    public GameObject lifeArrow;
    public GameObject coinArrow;
    private bool[] lessonStatus;

    private int cubeCount = 0;
    public Text cubeCountText;
    private bool redWallTouched = false;
    private Rigidbody rb;
    private bool barrelHit = false;

    //for mock playing
    public GameObject barrel;
    public GameObject freezeBall;
    public GameObject[] testPlayObjects;
    private Vector3[] testPlayObjectPositions;
    public GameObject finishLine;
    private bool testPlay = false;
    private float spawnTime = 1.5f;
    private float spawnTimer = 0f;
    private int objectIndex = 1;
    private bool freezeSpawned = false;
    private bool freezeCollected = false;
    private bool testPlayFinished = false;
    public GameObject freezeOverlay;

    


    void Start(){
        testPlayObjectPositions = new Vector3[testPlayObjects.Length];

        for (int i = 0; i < testPlayObjects.Length; i++){
            testPlayObjectPositions[i] = testPlayObjects[i].transform.position;
        }
        
        lessonStatus = new bool[lessons.Length];
        changeTime = 3f;
        lessons[0].SetActive(true);
        tipSound.Play();
        ballscript = FindObjectOfType<ballScript>();
        sceneScript = FindObjectOfType<SceneManagerScript>();
        rb = GetComponent<Rigidbody>();
        
    // for jumping straight to specific lesson
        // lessons[0].SetActive(false);
        // (lessonStatus[0], lessonStatus[1], lessonStatus[2], lessonStatus[3], lessonStatus[4], 
        // lessonStatus[5], lessonStatus[6], lessonStatus[7], lessonStatus[8], lessonStatus[9], 
        // lessonStatus[10], lessonStatus[11], lessonStatus[12], lessonStatus[13]) = 
        // (true, true, true, true, true,  
        //     true, true, true, false, false, 
        //         false, false, false, false);
        // timer = 10f;
    }

    private void Update(){
        timer += Time.deltaTime;
        Debug.Log(timer);

        if (testPlay){ //when player takes damage, need to figure out way to reset everything (reset player position, reset barrel and freeze object positions)
            spawnTimer += Time.deltaTime;

            if (spawnTime < spawnTimer){
                spawnTimer = 0f;

                float xCord = gameObject.transform.position.x + Random.Range(0, 4);
                float yCord = 5f;
                float zCord = gameObject.transform.position.z + 10 + Random.Range(-1, 10);

                Vector3 objectPosition = new Vector3(xCord, yCord, zCord);
                    
                if (objectIndex % 4 == 0){ //object that is spawned is a freeze object
                    GameObject freezeBallPrefab = Instantiate(freezeBall) as GameObject;
                    freezeBallPrefab.transform.position = objectPosition;
                    Rigidbody freezeBallrb = freezeBallPrefab.GetComponent<Rigidbody>();                
                    freezeSpawned = true;
                } 

                else {
                    GameObject barrelPrefab = Instantiate(barrel) as GameObject;
                    barrelPrefab.transform.position = objectPosition;
                    Rigidbody barrelrb = barrelPrefab.GetComponent<Rigidbody>();     
                    barrelPrefab.SetActive(true);              
                }

                objectIndex++;
            }
        }

        //lesson 1: collect cubes
        if (!lessonStatus[0] && timer > changeTime){
            transform.position = new Vector3(0, 1.3f, 0);
            tipSound.Play();
            //activate cubes
            cubes.SetActive(true);
            lessonStatus[0] = true;
            lessons[0].SetActive(false);
            lessons[1].SetActive(true);
            timer = 0f;
        }

        //lesson 2: go to the end of the platform
        else if (lessonStatus[0] && !lessonStatus[1] && cubeCount == 6){ //change back to 6 later
            tipSound.Play();
            cubes.SetActive(false);
            redWall.SetActive(true);

            scoreText.SetActive(true);
            lessonStatus[1] = true;
            lessons[1].SetActive(false);
            lessons[2].SetActive(true);
        }

        //lesson 3: look at score, goal of game is to travel as far as possible
        else if (lessonStatus[1] && !lessonStatus[2] && redWallTouched){
            tipSound.Play();
            ballscript.lockMovement();
            redWall.SetActive(false);
            scoreArrow.SetActive(true);
            lessonStatus[2] = true;
            lessons[2].SetActive(false);
            lessons[3].SetActive(true);
            changeTime = 5f;
            timer = 0f;
            transform.position = new Vector3(0, 1.3f, 0);
        }

        //lesson 4: about lives
        else if (lessonStatus[2] && !lessonStatus[3] && changeTime < timer){
            tipSound.Play();
            transform.position = new Vector3(0, 1.3f, 0);
            rb.constraints = RigidbodyConstraints.FreezePosition;        
            lifeText.SetActive(true);
            scoreArrow.SetActive(false);
            lifeArrow.SetActive(true);
            timer = 0f;
            lessonStatus[3] = true;
            lessons[3].SetActive(false);
            lessons[4].SetActive(true);
            changeTime = 5f;
        }    

        //lesson 5: about barrels
        else if (lessonStatus[3] && !lessonStatus[4] && changeTime < timer){
            tipSound.Play();
            // spawn barrel hitting player
            barrel1.SetActive(true);
            timer = 0f;
            lifeArrow.SetActive(false);
            lessonStatus[4] = true;
            lessons[4].SetActive(false);
            lessons[5].SetActive(true);
            changeTime = 5f;
        }    


        //lesson 6: demonstrate lives decrementing
        else if (!lessonStatus[5] && barrelHit && changeTime < timer){
            tipSound.Play();
            lessons[6].SetActive(true);
            lessons[5].SetActive(false);
            lifeArrow.SetActive(true);
            lessonStatus[5] = true;
            timer = 0f;
            changeTime = 3f;
        }

        //lesson 7: about hearts
        else if (!lessonStatus[6] && lessonStatus[5] && changeTime < timer){
            tipSound.Play();
            timer = 0;
            changeTime = 3f;
            lifeArrow.SetActive(false);
            lessons[6].SetActive(false);
            lessonStatus[6] = true;
            lessons[7].SetActive(true);

            //spawn heart
            heart.SetActive(true);

        }

        //lesson 8: about coins
        else if (!lessonStatus[7] && lessonStatus[6] && changeTime < timer){
            tipSound.Play();
            timer = 0f;
            changeTime = 3f;
            coinText.SetActive(true);
            coinArrow.SetActive(true);
            lessons[7].SetActive(false);
            lessonStatus[7] = true;
            lessons[8].SetActive(true);
            heart.SetActive(false);

            //spawn coin
            coin.SetActive(true);
        }

        //lesson 9: about moving and tilting platforms
        else if (!lessonStatus[8] && lessonStatus[7] && changeTime < timer){
            tipSound.Play();
            timer = 0f;
            changeTime = 3f;
            coinArrow.SetActive(false);
            lessons[8].SetActive(false);
            lessonStatus[8] = true;
            lessons[9].SetActive(true);
            coin.SetActive(false);

            //allow tilting and platform piece movement
            tiltScript.setRotateSpeed(5);
            enableSideMovementScript.setMovement();
        }

        
        //lesson 10: ready to play for a bit
        else if (!lessonStatus[10] && !lessonStatus[9] && lessonStatus[8] && changeTime < timer){
            ballscript.setHealth(99);
            tipSound.Play();
            transform.position = new Vector3(0, 3f, 0);
            ballscript.unlockMovement();
            lessons[9].SetActive(false);
            lessonStatus[9] = true;
            lessons[10].SetActive(true);

            finishLine.SetActive(true);

            lifeText.GetComponent<Text>().text = "Lives: 1";

            //let player play until they get to the end (player has to make it through without taking damage, or everything restarts)
            transform.position = new Vector3(0, 1.3f, 0);
            testPlay = true;

        }


        //lesson 11: collect freeze command
        else if (!lessonStatus[11] && !lessonStatus[10] && lessonStatus[9] && freezeSpawned) {
            tipSound.Play();
            lessons[10].SetActive(false);
            lessonStatus[10] = true;
            lessons[11].SetActive(true);
        }

        //lesson 12: about freeze 
        else if (!testPlayFinished && !lessonStatus[11] && lessonStatus[10] && freezeCollected){
            tipSound.Play();
            lessons[11].SetActive(false);
            lessonStatus[11] = true;
            lessons[12].SetActive(true);
        }

        //lesson 13: you are ready to play, enjoy the game
        else if (testPlayFinished && !lessonStatus[12]){
            tipSound.Play();
            transform.position = new Vector3(0f, 20f, 500f);
            ballscript.lockMovement();
            timer = 0;
            changeTime = 3f;
            testPlay = false;
            lessons[10].SetActive(false);
            lessons[11].SetActive(false);
            lessons[12].SetActive(false);
            lessonStatus[12] = true;
            lessons[13].SetActive(true);
            finishLine.SetActive(false);
            freezeOverlay.SetActive(false);
            saveIntro();
            //implement logic making player do intro during only first time
        }

        //take player to main game
        else if (lessonStatus[12] && changeTime < timer){
            tipSound.Play();
            sceneScript.loadGame(1);
        }
        
        //player falls into void
        if (transform.position.y < -20f) {
            bool freeze = ballscript.getFreeze();

            if (!freeze) {
                transform.position = new Vector3(0, 1.3f, 0);

                if (testPlay){ //player falls into void during test play
                    resetTestPlay();
                }
            }

            else {
                gameObject.transform.position = new Vector3(0f, 10f, transform.position.z);

            }
        }

    }

    void OnCollisionEnter (Collision other){
        if (other.gameObject.tag == "introCube"){
            cubeCount++;
            cubeCountText.text = "> Cubes Collected: " + cubeCount +  " / 6";
        }
        
        if (other.gameObject.tag == "block"){
            bool freeze = ballscript.getFreeze();

            barrelHit = true;

            if (testPlay && !freeze) {
                resetTestPlay();
            }
        }

        if (other.gameObject.tag == "freeze"){
            freezeCollected = true;
        }
    }

    void OnTriggerEnter (Collider other){
        if (other.gameObject.tag == "redWall"){
            redWallTouched = true;
        }

        if (other.gameObject.tag == "finishLine"){
            testPlayFinished = true;
        }
    }

    void resetTestPlay(){
        transform.position = new Vector3(0, 1.3f, 0);
        //reset lessons and lesson status
        lessonStatus[9] = false;
        // lessonStatus[10] = false;
        // lessonStatus[11] = false;
        lessons[10].SetActive(false);
        // lessons[11].SetActive(false);
        lessons[12].SetActive(false);
        spawnTime = 1.5f;
        freezeSpawned = false;
        freezeCollected = false;
        
    }

    public void saveIntro(){
        SaveSystem.saveIntroData();
    }
}
