using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoSpawnController : MonoBehaviour
{
    public GameObject ammoBox;

    public float ammoSpawnMultiplyer = 1.0f;
    public int ammoPickupcount = 15;

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
        time += Time.deltaTime * ammoSpawnMultiplyer;

        if (time >= spawnTime)
        {
            int xSpawn = Random.Range(-5, 5);
            int ySpawn = Random.Range(-4, 4);

            Vector3 spawnPoint = new Vector3(xSpawn, ySpawn, 0);

            Instantiate(ammoBox, spawnPoint, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

            time = 0;
        }
    }
}
