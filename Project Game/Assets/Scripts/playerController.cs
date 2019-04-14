using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    //private Rigidbody2D rb2d;
    public float speed;
    Vector3 mousePos;
    public GameObject bulletSpawn;
    public GameObject ammoSpawn;
    private ammoSpawnController ammoScript;

    public Image damageImage;
    public float flashSpeed = 5.0f;
    public Color flashColour = new Color(1.0f, 0.0f, 0.0f, 0.1f);
    bool damaged = false;

    public GameObject bullet;
    public int ammoCount = 100;
    public int health = 100;
    public int score = 0;
    public bool isDead = false;
    public int enemyDamage = 10;
    public float shotrateTimer = 0;
    public int shotsFired = 0;
    public float shotsPerSecond;
    public int moveRateTimer = 0;
    private Vector3 prevPosition;
    public float healthDepleteTimer = 0;
    public float healthDepleteRate;
    public float healthDepleted;

    private AudioSource playerAudio;
    public AudioClip shot;
    public AudioClip reload;
    public AudioClip hurt;
    public AudioClip healthPickup;

    // Use this for initialization
    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        //reload = GetComponent<AudioSource>();

        ammoScript = ammoSpawn.GetComponent<ammoSpawnController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("w"))
        {
            transform.position += new Vector3(0, 1) * speed * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            transform.position += new Vector3(0, -1) * speed * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            transform.position += new Vector3(-1, 0) * speed * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(1, 0) * speed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (ammoCount > 0)
            {
                shoot();
                playerAudio.clip = shot;
                playerAudio.volume = 0.1f;
                playerAudio.Play();
                // shoot on mouse click
                ammoCount--;
                shotsFired++;
            }
        }

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // get mouse position and look at mouse position 

        if (health <= 0)
        {
            isDead = true;
            playerAudio.mute = true;
            StartCoroutine("endPause");
            // end game if health is 0
        }

        // calculating shooting rate
        shotrateTimer += Time.deltaTime;
        shotsPerSecond = shotsFired / shotrateTimer;
        //Debug.Log(shotsPerSecond);

        //calculating move rate
        if (prevPosition == transform.position)
        {
            moveRateTimer++;
        }
        else
        {
            moveRateTimer = 0;
            prevPosition = transform.position;
        }

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        //calculating health depletion rate
        healthDepleteTimer += Time.deltaTime;
        healthDepleteRate = healthDepleted / healthDepleteTimer;

    }

    void shoot()
    {
        Instantiate(bullet, bulletSpawn.transform.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (isDead == false)
            {
                playerAudio.clip = hurt;
                playerAudio.volume = 0.4f;
                playerAudio.Play();
                playerAudio.loop = true;

                health = health - enemyDamage;
                healthDepleted += enemyDamage;
                // decreasing player health
                damaged = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (isDead == false)
            {
                playerAudio.loop = false;

                //health = health - enemyDamage;
                // decreasing player health
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ammo")
        {
            ammoCount += ammoScript.ammoPickupcount;
            Destroy(collision.gameObject);
            playerAudio.clip = reload;
            playerAudio.volume = 0.9f;
            playerAudio.Play();
            // increase ammo on pickup
        }

        if (collision.gameObject.tag == "health")
        {
            health += 10;
            playerAudio.clip = healthPickup;
            playerAudio.volume = 0.7f;
            playerAudio.Play();
            Destroy(collision.gameObject);
            // increase health on pickup
        }
    }

    IEnumerator endPause()
    {
        // Destroy(gameObject);
        gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}

// https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health?playlist=17144
// used for flashing the screen red