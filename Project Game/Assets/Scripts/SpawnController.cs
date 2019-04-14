using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    // spawn points
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;

    public GameObject enemy;

    public float maxTime = 5;
    public float minTime = 2;
    public float enemySpawnMultiplyer = 1.0f;

    private float time;
    private float spawnTime;

    // values for enemy
    public float speed = 1.6f;
    public int health = 80;
    public int maxNumOfEnemies;
    public int numOfEnemies = 0;

    // values for calculating kill rate
    public int enemiesKilled = 0;
    public float enemyKillRate;
    private float killTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Random.Range(minTime, maxTime);
        time = minTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime * enemySpawnMultiplyer;

        if (time >= spawnTime && numOfEnemies < maxNumOfEnemies)
        {
            int spawnPointIndex = Random.Range(0, 5);
            
            if (spawnPointIndex == 1)
            {
                Instantiate(enemy, spawn1.transform.position, spawn1.transform.rotation);
                time = minTime;
                numOfEnemies++;
            }
            else if (spawnPointIndex == 2)
            {
                Instantiate(enemy, spawn2.transform.position, spawn2.transform.rotation);
                time = minTime;
                numOfEnemies++;
            }
            else if (spawnPointIndex == 3)
            {
                Instantiate(enemy, spawn3.transform.position, spawn3.transform.rotation);
                time = minTime;
                numOfEnemies++;
            }
            else if (spawnPointIndex == 4)
            {
                Instantiate(enemy, spawn4.transform.position, spawn4.transform.rotation);
                time = minTime;
                numOfEnemies++;
            }

            spawnTime = Random.Range(minTime, maxTime);
        }

        // calculate enemy kill rate
        killTimer += Time.deltaTime;
        enemyKillRate = enemiesKilled / killTimer;
        //Debug.Log(enemyKillRate);
        
    }
}

// https://docs.unity3d.com/ScriptReference/Random.Range.html
// used for random spawn time
