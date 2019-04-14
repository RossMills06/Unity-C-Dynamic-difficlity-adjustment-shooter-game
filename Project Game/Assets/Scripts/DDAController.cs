// current highscore: 850, 16th Feb 19, Meelkk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDAController : MonoBehaviour
{
    private playerController playerScript;
    private ammoSpawnController ammoScript;
    private SpawnController enemySpawnScript;
    private healthSpawnController healthSpawnScript;
    private bulletController bulletScript;
    // gameobjects for the scripts

    public GameObject player;
    public GameObject ammoSpawn;
    public GameObject enemySpawn;
    public GameObject healthSpawn;
    public GameObject bullet;
    // game objects for the game objects

    private float enemyHealthDDAkillrate;
    private float enemyHealthDDAplayerhealthrate;
    private float avgEnemyHealthDDA;

    private float enemySpeedDDAkillrate;
    private float enemySpeedDDAplayerhealthrate;
    private float avgEnemySpeedDDA;

    private int gameTimer;
    private int gameTimerDDAindex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        // getting the scripts from their relative game objects
        playerScript = player.GetComponent<playerController>();
        ammoScript = ammoSpawn.GetComponent<ammoSpawnController>();
        enemySpawnScript = enemySpawn.GetComponent<SpawnController>();
        healthSpawnScript = healthSpawn.GetComponent<healthSpawnController>();
        //bulletScript = bullet.GetComponent<bulletController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameTimer += (int)Time.deltaTime;
        gameTimer = (int)Time.time;
        //updating the game timer

        // ********** SCALING AMMO PICKUP SPAWN RATE **********
        if (playerScript.ammoCount > 75)
            ammoScript.ammoSpawnMultiplyer = 0.0f;
        else if (playerScript.ammoCount <= 75 && playerScript.ammoCount > 50)
            ammoScript.ammoSpawnMultiplyer = 0.5f;
        else if (playerScript.ammoCount <= 50 && playerScript.ammoCount > 25)
            ammoScript.ammoSpawnMultiplyer = 1.0f;
        else if (playerScript.ammoCount <= 25)
            ammoScript.ammoSpawnMultiplyer = 2.0f;
        // ammo pickups spawn with a higher frequency when the players ammo is low


        // ********** SCALING HEALTH PICKUP SPAWN RATE **********
        if (playerScript.health > 75)
            healthSpawnScript.healthSpawnMultiplyer = 0.0f;
        else if (playerScript.health <= 75 && playerScript.health > 50)
            healthSpawnScript.healthSpawnMultiplyer = 0.5f;
        else if (playerScript.health <= 50 && playerScript.health > 25)
            healthSpawnScript.healthSpawnMultiplyer = 1.0f;
        else if (playerScript.health <= 25)
            healthSpawnScript.healthSpawnMultiplyer = 2.0f;
        // health pickups spawn with a higher frequency when the players health is low


        // ********** SCALING ENEMY SPAWN RATE **********
        if (enemySpawnScript.numOfEnemies > 6)
            enemySpawnScript.enemySpawnMultiplyer = 1.0f;
        else if (enemySpawnScript.numOfEnemies <= 5 && enemySpawnScript.numOfEnemies > 4)
            enemySpawnScript.enemySpawnMultiplyer = 2.0f;
        else if (enemySpawnScript.numOfEnemies <= 4)
            enemySpawnScript.enemySpawnMultiplyer = 3.0f;
        // enemies spawn faster when there are less enemies on the screen


        // ********** SCALING MAX NUMBER OF ENEMIES **********
        if (playerScript.shotsPerSecond > 2.0f)
            enemySpawnScript.maxNumOfEnemies = 15;
        else if (playerScript.shotsPerSecond <= 2.0f && playerScript.shotsPerSecond > 1.0f)
            enemySpawnScript.maxNumOfEnemies = 12;
        else if (playerScript.shotsPerSecond <= 1.0f)
            enemySpawnScript.maxNumOfEnemies = 10;
        // if the player is shooting alot, give them more enemies to shoot at

        // checking if player is staying the same spot (camping, analysing stratergies)
        if (playerScript.moveRateTimer > 300)
            enemySpawnScript.health = 100;
        // setting enemy health to full if the palyer has been in the same spot for a while


        // ********** SCALING ENEMY DAMAGE **********
        if (playerScript.health > 75)
            playerScript.enemyDamage = 20;
        else if (playerScript.health <= 75 && playerScript.health > 60)
            playerScript.enemyDamage = 15;
        else if (playerScript.health <= 50 && playerScript.health > 30)
            playerScript.enemyDamage = 10;
        else if (playerScript.health <= 10)
            playerScript.enemyDamage = 5;
        // enemy damage becomes less when the players health is lower


        // ********** SCALING ENEMY HEALTH **********
        if (enemySpawnScript.enemyKillRate > 1.0f)
            enemyHealthDDAkillrate = 1.0f;
        else if (enemySpawnScript.enemyKillRate <= 0.75 && enemySpawnScript.enemyKillRate > 0.5)
            enemyHealthDDAkillrate = 0.8f;
        else if (enemySpawnScript.enemyKillRate <= 0.5 && enemySpawnScript.enemyKillRate > 0.25)
            enemyHealthDDAkillrate = 0.6f;
        else if (enemySpawnScript.enemyKillRate <= 0.25)
            enemyHealthDDAkillrate = 0.4f;
        // enemy health is less when the enemy kill rate is low

        if (playerScript.healthDepleteRate > 8.0f)
            enemyHealthDDAplayerhealthrate = 0.4f;
        else if (playerScript.healthDepleteRate <= 8.0f && playerScript.healthDepleteRate > 4.0f)
            enemyHealthDDAplayerhealthrate = 0.6f;
        else if (playerScript.healthDepleteRate <= 4.0f && playerScript.healthDepleteRate > 1.0f)
            enemyHealthDDAplayerhealthrate = 0.8f;
        else if (playerScript.healthDepleteRate <= 1.0f)
            enemyHealthDDAplayerhealthrate = 1.0f;
        // enemy health is less when the player is losing health at a fast rate


        // ********** SCALING ENEMY SPEED **********
        if (enemySpeedDDAkillrate == 0.0f)
            enemySpeedDDAkillrate = 2.0f;
        else if (enemySpawnScript.enemyKillRate > 1.0f)
            enemySpeedDDAkillrate = 1.0f;
        else if (enemySpawnScript.enemyKillRate <= 0.75 && enemySpawnScript.enemyKillRate > 0.5)
            enemySpeedDDAkillrate = 1.5f;
        else if (enemySpawnScript.enemyKillRate <= 0.5 && enemySpawnScript.enemyKillRate > 0.2)
            enemySpeedDDAkillrate = 2.0f;
        else if (enemySpawnScript.enemyKillRate <= 0.2)
            enemySpeedDDAkillrate = 2.5f;
        // enemies become faster if the kill rate (kills/second) is low

        if (playerScript.healthDepleteRate > 8.0f)
            enemySpeedDDAplayerhealthrate = 1.0f;
        else if (playerScript.healthDepleteRate <= 8.0f && playerScript.healthDepleteRate > 4.0f)
            enemySpeedDDAplayerhealthrate = 1.5f;
        else if (playerScript.healthDepleteRate <= 4.0f && playerScript.healthDepleteRate > 1.0f)
            enemySpeedDDAplayerhealthrate = 1.5f;
        else if (playerScript.healthDepleteRate <= 1.0f)
            enemySpeedDDAplayerhealthrate = 2.0f;
        // enemy speed is less when the player is losing health at a fast rate


        // ********** calculating game values **********
        if (gameTimer > 300)
            gameTimerDDAindex = 5;
        else if (gameTimer <= 300 && gameTimer > 240)
            gameTimerDDAindex = 4;
        else if (gameTimer <= 240 && gameTimer > 180)
            gameTimerDDAindex = 3;
        else if (gameTimer <= 180 && gameTimer > 120)
            gameTimerDDAindex = 2;
        else if (gameTimer <= 120 && gameTimer > 60)
            gameTimerDDAindex = 1;
        else if (gameTimer <= 60)
            gameTimerDDAindex = 0;
        // increase difficulty every minute
    

        // calcuating health
        avgEnemyHealthDDA = (enemyHealthDDAkillrate + enemyHealthDDAplayerhealthrate + (0.2f * gameTimerDDAindex)) / 2;
        enemySpawnScript.health =  (int)(100 * avgEnemyHealthDDA);
        //Debug.Log(enemySpawnScript.health);

        // calcualting speed
        avgEnemySpeedDDA = (enemySpeedDDAkillrate + enemySpeedDDAplayerhealthrate + (0.2f * gameTimerDDAindex)) / 2;
        enemySpawnScript.speed = 1 * avgEnemySpeedDDA;
        //Debug.Log(enemySpawnScript.speed);

        //Debug.Log(gameTimer);
        //Debug.Log(gameTimerDDAindex);


        // debugging
        Debug.Log("Enemy Health: " + avgEnemyHealthDDA + ", Enemy Speed: " + avgEnemySpeedDDA + ", Enemy Damage: " + playerScript.enemyDamage);
    }
}

// if you like a lot of chocolate on your biscuit, join our club 
