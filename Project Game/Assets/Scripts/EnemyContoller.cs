using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    private playerController playerScript;

    //private Rigidbody2D rb2d;
    public GameObject player;
    public GameObject enemySpawn;
    private SpawnController enemySpawnScript;
    public Quaternion startRotation;

    private int health = 100;
    private float speed = 1.6f;

    AudioSource zombieSound;

    // Start is called before the first frame update
    void Start()
    {
        // get the player script
        player = GameObject.FindGameObjectWithTag("player");
        playerScript = player.GetComponent<playerController>();

        // get the enemy controller script 
        enemySpawn = GameObject.FindGameObjectWithTag("enemySpawn");
        enemySpawnScript = enemySpawn.GetComponent<SpawnController>();
        health = enemySpawnScript.health;
        speed = enemySpawnScript.speed;

        // get audio soruce
        zombieSound = GetComponent<AudioSource>();
        zombieSound.Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);

        transform.position += transform.forward * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);

        // get player position and follow
        Vector3 lookPos = player.transform.position;
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.eulerAngles = new Vector3(0.0f, 0.0f, transform.eulerAngles.z);

        if (health <= 0)
        {
            //decrease enemy count and increase kill count
            enemySpawnScript.numOfEnemies--;
            enemySpawnScript.enemiesKilled++;

            Destroy(gameObject);
            playerScript.score++;
            playerScript.ammoCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            // DDA Scaling for bullet damage
            bulletController bulletScript = collision.gameObject.GetComponent<bulletController>();
            health = health - bulletScript.bulletDamage;
            //Debug.Log(bulletScript.bulletDamage);
            Destroy(collision.gameObject);
        }
    }
}
