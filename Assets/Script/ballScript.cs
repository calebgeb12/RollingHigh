using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.SceneManagement;
using TMPro;


public class ballScript : MonoBehaviour
{
    public SceneManagerScript scenescript;
    public newPlatformScript newplatformscript; 
    public gameOverScript gameoverscript;
    public blockSpawnerScript blockspawnerscript;
    public ShopStatus shopStatus;

    public int healthPoint;
    public int spawnRateIncreaseFrequency;
    private bool immunity = false;
    private int targetDistance;  
    private int distance;
    private Rigidbody rb;
    private float platformZVal;
    private int diff;
    private float scoreMult = 1f;

    //input force
    private float xInput;
    private float zInput;

    //tracking variables
    private int totalDistance;
    private int coins = 0;

    //speed variables
    public float speed;
    public float maxSpeed;
    public float normSpeed;
    public float normMaxSpeed;
    public float boostSpeed;
    public float boostMaxSpeed;
    public float speedBoostDuration;

    //freeze powerup variables
    private float freezeTime = 2.5f;
    private float freezeTimer = 0;
    public bool freeze;

    //UI
    public Text scoreText;
    public Text lifeText;
    public Text coinText;
    public bool restart = true; //so that game ending condition doesn't loop 20
    

    //audio
    public AudioSource worldFreezeSound;

        //addressing heart beat issue
    public AudioSource heartBeat;
    private float heartBeatDuration = 5f;
    private bool playHeartBeat = true;

    public AudioClip healthPickup;
    public AudioClip hitBlock;
    public AudioClip speedPickup;
    public AudioClip worldFreezePickup;
    public AudioClip coinSound;

    //audio volume
    private float healthPickUpVolume = .2f;
    private float coinPickupVolume = .7f;


    private bool heartPlayed = false;

    private int totalScore = 0;
    private int otherScore = 0; //not being used anymore, doesn't contribute to total score

    private int prefabLength = 230;
    private float zOutOfBounds = -15;


    private bool dropBall = false;

    //Overlays
    public GameObject initialInstructions;
    private float initialInstructionsTime = 0;
    private float initialInstructionsDisplayTime = 7f;

    public GameObject dropTextObject;
    public Text dropCounterText;
    public GameObject speedOverlay;
    public GameObject damagedOverlay;
    private float overlayDisplayTime = .5f;
    public GameObject freezeOverlay;
     
     //joystick testing
     [SerializeField] private FixedJoystick joystick;
     [SerializeField] private Animator animator;

    //for automatically dropping player
    private float dropTime = 3f;
    private float dropTimer = 0f;
    private bool countDropTime = false;

    //for selecting ball skin
    public Material[] skins;
    public Material[] defaultSkins;
    private Material currentSkin;

    //for determining ball type
    private int ballTypeIndex;

    //for biggie smalls and the world's cooldown
    // private float freezeCoolDown = 3f;
    private float freezeCoolDown = 5f;
    private float freezeCoolDownTimer;
    private float abilityFreezeTime = 2.2f;


    private bool allowFreeze = true;
    public Button freezeButton;

    private float sizeChangeCoolDown = 5f;
    private float sizeChangeCoolDownTimer;
    private bool allowSizeChange = true;
    public Button sizeChangeButton;


    //for spikey mikey
    public GameObject[] spikes;
    private int numOfSpikes;


    private bool movementLocked = false;
    public bool intro;


    //for adding a life
    public GameObject inGameObjects;
    public GameObject gameOverObjects;
    private bool revived = false;
    private float oldSpeed;
        //for solving weird issue where joystick position doesn't reset
        [SerializeField] private FixedJoystick newJoystick;
        [SerializeField] private FixedJoystick tempJoystick;
        private Vector3 joystickScale;

    //for doubling coins
    private int oldCoins;


    //special ting
    private bool easterEgg1Found = false;
    private bool easterEgg2Found = false;
     

    void Awake()
    {
        diff = scenescript.getDifficulty();
        if (diff == 0) {
            scoreMult = .5f;
        }

        else if (diff == 1) {
            scoreMult = 1f;
        }

        else if (diff == 2) {
            scoreMult = 2f;
        }
        
        if (!intro){

            shopStatus = FindObjectOfType<ShopStatus>();
            int skinIndex = shopStatus.getCurrentSkin();
            activateBallType activateBallTypeScript = FindObjectOfType<activateBallType>();
            skinSetter skinSetterScript = FindObjectOfType<skinSetter>();

            ballTypeIndex = activateBallTypeScript.getCurrentBallTypeIndex();

            //for setting the current skin
            if (skinIndex == 0){
                currentSkin = skinSetterScript.getDefaultSkin(ballTypeIndex);
                // gameObject.GetComponent<Renderer>().material = defaultSkins[ballTypeIndex];
                gameObject.GetComponent<Renderer>().material = currentSkin;
            }

            else{
                currentSkin = skinSetterScript.getSkin(skinIndex);
                // gameObject.GetComponent<Renderer>().material = skins[skinIndex];
                gameObject.GetComponent<Renderer>().material = currentSkin;
            }

            if (ballTypeIndex == 2) { //for setting number of spikes for spikey mikey
                numOfSpikes = SaveSystem.getAbilitiesData().getNumOfSpikes();
                for (int i = 0; i < numOfSpikes; i++){
                    spikes[i].SetActive(true);
                }
            }

            if (ballTypeIndex == 4){ //for accessing freeze ability variables
                abilityFreezeTime = SaveSystem.getAbilitiesData().getFreezeTime();
                freezeCoolDown = SaveSystem.getAbilitiesData().getFreezeCooldown();
            }
        }

            speed = normSpeed;
            maxSpeed = normMaxSpeed;
            rb = GetComponent<Rigidbody>();
            targetDistance = distance + spawnRateIncreaseFrequency;

            coinText.text = "> Coins: " + coins;
            // destroyTime = initialDestroyTime;

        joystickScale = joystick.transform.localScale;
    }


    public void Update()
    {
        initialInstructionsTime += Time.deltaTime;

        if (initialInstructionsTime > initialInstructionsDisplayTime)
        {
            initialInstructions.SetActive(false);
        }

        totalDistance = (int) (gameObject.transform.position.z * scoreMult);
        int one = totalDistance % 10;
        int two = (totalDistance / 10) % 10;
        int three = (totalDistance / 100) % 10;
        int four = (totalDistance / 1000) % 10;
        int five = (totalDistance / 10000) % 10;
        int six = (totalDistance / 100000) % 10;

        //displays score and hidden message :)
        
        if (totalDistance < 0 && totalDistance > -50 && !easterEgg1Found)
        {
            scoreText.text = "> Go forward!";
        }

        else if (totalDistance <= - 50 && totalDistance > -100 && !easterEgg1Found) {
            scoreText.text = "> You kinda suck bro...";
        }

        else if (totalDistance <= - 100 && totalDistance > -200 && !easterEgg1Found) {
            scoreText.text = "> bro...";
        }

        else if (totalDistance <= - 200 && totalDistance >= -500 && !easterEgg1Found) {
            scoreText.text = "> you're doing this on purpose...aren't you...";
        }

        else if (totalDistance <= -500 && totalDistance >= -750 && !easterEgg1Found) {
            scoreText.text = "> bro...this actually takes skill...I guess?";
        }

        else if (totalDistance <= -750 && totalDistance >= -1500 && !easterEgg1Found) {
            scoreText.text = "> I would reward you, but I'm too lazy...";
        }

        else if (totalDistance <= -1500) {

            if (!easterEgg1Found) {
                easterEgg1Found = true;
                coins += 250;
                coinText.text = "> Coins: " + coins;
            }

            scoreText.text = "> alright here's 250 coins...I lied";
        }   

        else if (easterEgg1Found && totalDistance < 0) {
            scoreText.text = "> bing bong";
        }

        else if (easterEgg1Found && totalDistance > 0) {
            if (!easterEgg2Found) {
                easterEgg2Found = true;
                coins += 250;
                scoreText.text = "> wah...";
                coinText.text = "> Coins: " + coins;
            }
        }

        else
        {
            scoreText.text = "> Score: " + six + five + four + three + two + one;
        }

        if (totalDistance > targetDistance)
        {
            targetDistance += spawnRateIncreaseFrequency;
            blockspawnerscript.increaseSpawn();
        }



        //activates only if ball needs to be dropped and if player moves or if timer for dropping is up
        if (xInput > .1 || xInput < -.1 || zInput > .1 || zInput < -.1 || dropTime < dropTimer) 
        {
            dropTextObject.SetActive(false);
            GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePosition;
            dropTimer = 0f;
            countDropTime = false;
        }

        if (freeze){
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0){
                freeze = false;
                immunity = false;
                freezeOverlay.SetActive(false);
            }
        }

        if (countDropTime){
            dropTimer += Time.deltaTime;
            dropCounterText.text = "Dropping in " + (dropTime - dropTimer).ToString("0.00") + "s";
        }

        if (!allowFreeze){
            freezeCoolDownTimer += Time.deltaTime;
            if (freezeCoolDownTimer > freezeCoolDown){
                allowFreeze = true;
                freezeCoolDownTimer = 0f;
                Image buttonImage = freezeButton.GetComponent<Image>();
                Color newColor = buttonImage.color;
                newColor.a = 1f;
                buttonImage.color = newColor;
            }
        }

    //for 'the world'
         if (Input.GetKeyDown("l") && ballTypeIndex == 4 && allowFreeze){
            stopTime();
            // setFreezeTimer(abilityFreezeTime);
            // immunity = true;
            // freeze = true;
            // freezeOverlay.SetActive(true);
            // worldFreezeSound.Play();
            // otherScore += 200;
            // allowFreeze = false;
         }
    }

    public void FixedUpdate()
    {
     //for controller and PC   
        // if (!movementLocked){
        //     xInput = Input.GetAxis("Horizontal");
        //     zInput = Input.GetAxis("Vertical");
        // }

    //for mobile
        if (!movementLocked){
            xInput = joystick.Horizontal; 
            zInput = joystick.Vertical;        
        }

        float playerZPosition = gameObject.transform.position.z;


        // Address being too fast or too slow
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        if (rb.linearVelocity.magnitude < 2)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * 2;
        }


        // If ball is off track, negative health, or something else we cannot remember which is why
        // we add comments to know what we did last month (Mr. Schepens raging)
    //possible place for optimization? Only need to check this condition when hitting obstacle
        if (healthPoint <= 0 && restart)
        {
            if (!intro){
                if (revived) {
                    SaveSystem.savePointData(this, coins - oldCoins);
                }

                else {
                    SaveSystem.savePointData(this);
                    oldCoins = coins;
                }
            }

            gameoverscript.gameOver(totalDistance, coins); 
            coinText.text = "> Coins: " + coins;
            oldSpeed = speed;

            speed = 0;
            restart = false;
        }
        
        if (gameObject.transform.position.y < zOutOfBounds)
        {
            if (!immunity){
                healthPoint--;
            }

            lifeText.text = "> Lives: " + healthPoint.ToString(); 

            if (healthPoint != 0)
            {
                dropTextObject.SetActive(true);
                gameObject.transform.position = new Vector3(0f, 10f, playerZPosition);
                rb.constraints = RigidbodyConstraints.FreezePosition;
                dropBall = true;
                countDropTime = true;
            }   
            
        }
        
        rb.AddForce(new Vector3(xInput, 0.0f, zInput) * speed);
    }


    void OnCollisionEnter(Collision other)
    {        
        if (other.gameObject.tag == "block" && !immunity)
        {
            AudioSource.PlayClipAtPoint(hitBlock, transform.position, 1.0f);
            healthPoint--;     
            if (healthPoint != 0) //need to check this for aesthetic reasons (doesn't look nice when overlay activates when game ends)
                damagedOverlay.SetActive(true);
            StartCoroutine("overlayDuration");             
        }

        // If on last heart, play heartbeat
        if (healthPoint == 1 && playHeartBeat && ballTypeIndex != 1) //don't want heartbeat sound playing for humpty dumpty
        {
            playHeartBeat = false;
            heartBeat.Play();
            StartCoroutine("heartBeatCoroutine");
        }

        if (other.gameObject.tag == "health")
        {
            //decreases buff count on current platform
            GameObject currentPlatform = newplatformscript.getCurrentPlatform(transform.position.z);
            currentPlatform.GetComponent<worldTiltScript>().decreaseBuffCount(); 

            healthPoint++;
            AudioSource.PlayClipAtPoint(healthPickup, transform.position, healthPickUpVolume);
            otherScore += 50;

            playHeartBeat = true;
            heartBeat.Stop();
        }

        if (other.gameObject.tag == "speed")
        {
            AudioSource.PlayClipAtPoint(speedPickup, transform.position, healthPickUpVolume);
            speed = boostSpeed;
            maxSpeed = boostMaxSpeed;
            speedOverlay.SetActive(true);
            StartCoroutine("speedDuration");  
            otherScore += 100;  
        }

        if (other.gameObject.tag == "freeze")
        {

            //decreases buff count on current platform
            GameObject currentPlatform = newplatformscript.getCurrentPlatform(transform.position.z);
            currentPlatform.GetComponent<worldTiltScript>().decreaseBuffCount(); 

            setFreezeTimer(freezeTime);
            immunity = true;
            freeze = true;
            freezeOverlay.SetActive(true);
            worldFreezeSound.Play();
            otherScore += 200;
        }

        if (other.gameObject.tag == "coin")
        {
            //decreases buff count on current platform
            GameObject currentPlatform = newplatformscript.getCurrentPlatform(transform.position.z);
            currentPlatform.GetComponent<worldTiltScript>().decreaseBuffCount(); 

            coins++;
            coinText.text = "> Coins: " + coins;
            AudioSource.PlayClipAtPoint(coinSound, transform.position, coinPickupVolume);
        }

        lifeText.text = "> Lives: " + healthPoint.ToString(); 
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "platformTrigger")
        {
            platformZVal += prefabLength;
            newplatformscript.createPlatform(platformZVal);
        }
    }

    public void addOneLife() {
        //attempt at resolving joystick issue (joystick position not resetting after gaining life)
        tempJoystick = Instantiate(newJoystick) as FixedJoystick;
        tempJoystick.transform.SetParent(inGameObjects.transform);
        tempJoystick.transform.position = new Vector3(joystick.transform.position.x, joystick.transform.position.y, joystick.transform.position.z);
        joystick.gameObject.SetActive(false);
        joystick = tempJoystick;
        joystick.gameObject.SetActive(true);
        joystick.transform.localScale = joystickScale;

        //update variables
        revived = true;
        speed = oldSpeed;
        restart = true;
        healthPoint = 1;
        lifeText.text = "> Lives: " + healthPoint.ToString(); 
        
        //disables game over screen
        inGameObjects.SetActive(true);
        gameOverObjects.SetActive(false);

        //allow player to drop after gaining a life
        xInput = 0f;
        zInput = 0f;
        dropTimer = 0f;
        dropTextObject.SetActive(true);
        gameObject.transform.position = new Vector3(0f, 10f, transform.position.z);
        rb.constraints = RigidbodyConstraints.FreezePosition;
        dropBall = true;
        countDropTime = true;     
    }

    public void doubleCoins() {
        if (revived) {
            SaveSystem.savePointData(this, coins);
        }

        else {
            SaveSystem.savePointData(this, coins);
        }

        gameoverscript.doubleCoinsText(coins);
    }

    public void stopTime(){
        if (allowFreeze){
            setFreezeTimer(abilityFreezeTime);
            immunity = true;
            freeze = true;
            freezeOverlay.SetActive(true);
            worldFreezeSound.Play();
            otherScore += 200;
            allowFreeze = false;
            Image buttonImage = freezeButton.GetComponent<Image>();
            Color newColor = buttonImage.color;
            newColor.a = .2f;
            buttonImage.color = newColor;
         }
    }


    IEnumerator overlayDuration(){
        yield return new WaitForSeconds(overlayDisplayTime);
        damagedOverlay.SetActive(false);
    }

    IEnumerator heartBeatCoroutine(){
        yield return new WaitForSeconds(heartBeatDuration);
        heartBeat.Stop();
    }

    public bool getFreeze()
    {
        return freeze;
    }

    public float getPosition()
    {
        return gameObject.transform.position.z;
    }

    public void setFreezeTimer(float freezeTime){
        freezeTimer += freezeTime;

    }

    public int getCoins(){
        return this.coins;
    }

    public int getCurrentBallTypeIndex(){
        return ballTypeIndex;
    }

    public void lockMovement() {
        movementLocked = true;
        xInput = 0;
        zInput = 0;
        rb.constraints = RigidbodyConstraints.FreezePosition;        
    }

    public void unlockMovement() {
        movementLocked = false;
        rb.constraints &= ~RigidbodyConstraints.FreezePosition;   
    }

    public void setHealth(int amount){
        healthPoint = amount;
    }

    public int getScore() {
        return this.totalDistance;
    }

}


/**speeds for ball types
    - mobile
        - default: 20
        - egg: 28
        - spike: 16
        - steel: 13
        - freeze: 20
    - windows
        - default: 13
        - egg: 20
        - spike: 11
        - steel: 11
        - freeze: 13

        **/