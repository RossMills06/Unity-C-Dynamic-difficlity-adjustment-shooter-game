using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthSpawnController : MonoBehaviour
{
    public GameObject healthBox;

    public float healthSpawnMultiplyer = 1.0f;

    float maxTime = 25;
    float minTime = 10;

    private float time;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Random.Range(minTime, maxTime);
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * healthSpawnMultiplyer;

        if (time >= spawnTime)
        {
            int xSpawn = Random.Range(-5, 5);
            int ySpawn = Random.Range(-4, 4);

            Vector3 spawnPoint = new Vector3(xSpawn, ySpawn, 0);

            Instantiate(healthBox, spawnPoint, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

            time = 0;
        }
    }
}
