using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockSpawnerScript : MonoBehaviour
{
    public ballScript ballscript;
    public SceneManagerScript sceneScript;
    private newPlatformScript newplatformscript;
    private GameObject currentPlatform;
    private worldTiltScript platformScript;
    public GameObject barrel;
    public GameObject health;
    public GameObject freezeObject;
    public GameObject coin;

    

//spawn rate variables
    private int diff = 0;
    private float spawnTimer = 0f;
    // private float spawnTime = 1.5f; //spawn every x seconds (initial value)
    private float spawnTime = .7f;
    private float spawnIncreaseTime = 5; //increase spawn rate every x seconds
    private float spawnIncreaseTimer = 0f;
    private float spawnIncreaseRate = .05f; //increase spawn rate by x
    private float maxSpawnSpeed = .7f;

    public bool intro;
    private int buffCountLimit = 7;

//spawn parameters per difficulty
    private float easySpawnTime = 2f;
    private float maxEasySpawn = 1f;

    private float medSpawnTime = 1.5f;
    private float maxMedSpawn = .8f;

    private float hardSpawnTime = 1.3f;
    private float maxHardSpawn = .7f;


    private int spawnChance = 100; //determines number of possible block outcomes
    public float ballPosition;

    public float xCord;
    public float zCord;

    private Rigidbody rb;
    private float time;
    private float increaseTime;
    private bool freeze;

//for testing purposes
    private bool customSpawn = false; //used to spawn specific object
    private int customSpawnType = 0; //1: freeze, 2: heart, 3: coin, 4: barrel only, 0: default 
    

    void Awake() {
        if (!intro) {
            diff = sceneScript.getDifficulty();
        }

        else {
            diff = 0; 
        }

        newplatformscript = FindObjectOfType<newPlatformScript>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();   //why do i have this??

    //set initial spawn rate and max spawn rate based on difficulty
        if (diff == 0) {
            spawnTime = easySpawnTime;
            maxSpawnSpeed = maxEasySpawn;
        }

        else if (diff == 1) {
            spawnTime = medSpawnTime;
            maxSpawnSpeed = maxMedSpawn;
        }

        else if (diff == 2) {
            spawnTime = hardSpawnTime;
            maxSpawnSpeed = maxHardSpawn;
        }
    }

    void Update()
    {
        if (gameObject.transform.position.z + 50 < gameObject.transform.position.z)
        {
            Destroy(gameObject);
        }

        //times spawning variables
        spawnTimer += Time.deltaTime;
        spawnIncreaseTimer += Time.deltaTime;
        
        //deals with spawn frequency
        if (spawnTimer > spawnTime){
            if (!ballscript.getFreeze()){
                spawn();
            }

            spawnTimer = 0f;
        }

    //deals with spawn increase frequency
        if (spawnIncreaseTimer > spawnIncreaseTime){
            increaseSpawn();
            spawnIncreaseTimer = 0f;
        }    

    //lets user spawn specific type of object if custom spawn is enabled
        if (Input.GetKeyDown("7")) { //spawn freeze
            customSpawnType = 1;
        }

        else if (Input.GetKeyDown("8")) { //spawn heart
            customSpawnType = 2;
        }

        else if (Input.GetKeyDown("9")) { //spawn coin
            customSpawnType = 3;
        }

        else if (Input.GetKeyDown("0")) { //only spawn barrels
            customSpawnType = 4;
            spawn();
        }

        else if (Input.GetKeyDown("l")) { //resets
            customSpawnType = 0;
        }
    }

    public void increaseSpawn()
    {
        if (spawnTime > maxSpawnSpeed)
        {
            spawnTime -= spawnIncreaseRate;
        }

    }

    void spawn()
    {
        float playerXPos = gameObject.transform.position.x;
        float playerZPos = gameObject.transform.position.z;

        float ySpawnPosition = 5f;
        int num = Random.Range(1, spawnChance);
        xCord = Random.Range(-6, 6);
        zCord = gameObject.transform.position.z + 10 + Random.Range(-1, 10);

        // num = Random.Range(0, 25); //for testing


    //prevents buff piling
        currentPlatform = newplatformscript.getCurrentPlatform(zCord); //gets platform for spawned object
        platformScript = currentPlatform.GetComponent<worldTiltScript>();
        int buffCount = platformScript.getBuffCount();

        if (buffCount >= buffCountLimit) { //spawn barrel if too many buffs
            num = 50; 
        }

        else if (num <= 25) {
            platformScript.increaseBuffCount();
        }
        
    //spawns specific type
        if (customSpawn) {
            if (customSpawnType == 1) { //spawn freeze
                num = 5;
            }

            else if (customSpawnType == 2) { //spawn heart
                num = 1;
            }

            else if (customSpawnType == 3) { //spawn coin
                num = 8;
            }

            else if (customSpawnType == 4) { //spawn coin
                num = 50;
            }
        }
        
        // num = 5;
        
    //instantiate health heart object
        if (num >= 1 && num <= 4) //4% chance of spawning
        {
            GameObject healthPrefab = Instantiate(health) as GameObject;
            healthPrefab.transform.position = new Vector3(xCord, ySpawnPosition, zCord);
            // healthPrefab.transform.position = new Vector3(gameObject.transform.position.x, ySpawnPosition, gameObject.transform.position.z + 4);
        }

    //instantiate freeze ball
        else if (num == 5) //1% chance of spawning
        {   
            GameObject freezePrefab = Instantiate(freezeObject) as GameObject;
            freezePrefab.transform.position = new Vector3(xCord, ySpawnPosition, zCord);
        }

    //instantiate coin
        else if (num >= 6 && num <= 25) //20 % chance of spawning
        {
            GameObject coinPrefab = Instantiate(coin) as GameObject;
            coinPrefab.transform.position = new Vector3(xCord, ySpawnPosition, zCord);
            // coinPrefab.transform.position = new Vector3(gameObject.transform.position.x, ySpawnPosition, gameObject.transform.position.z + 4);
        }

    //instantiate obstacle
        else //75% chance of spawning
        {
            xCord = gameObject.transform.position.x + Random.Range(0, 4);

            if (xCord < -9 || xCord > 7){
                xCord = gameObject.transform.position.x;
            }

            GameObject barrelPrefab = Instantiate(barrel) as GameObject;
            barrelPrefab.transform.position = new Vector3(xCord, ySpawnPosition, zCord);
            // barrelPrefab.transform.position = new Vector3(gameObject.transform.position.x, ySpawnPosition, gameObject.transform.position.z + 4);
            Rigidbody barrelrb = barrelPrefab.GetComponent<Rigidbody>();

            if (freeze)
            {
                barrelrb.useGravity = false;
            }
        }

        if (customSpawnType != 4) { //cannot be 4 because 4 represents barrel only, don't want to reset
            customSpawnType = 0; //resets spawn type
        }
    }
}
