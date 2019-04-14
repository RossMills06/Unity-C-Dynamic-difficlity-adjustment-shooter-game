using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    //private Rigidbody2D rb2d;
    public int bulletTimer = 0;
    public int bulletDamage = 20;

    // Start is called before the first frame update
    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * 10 * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        bulletTimer++;

        if (bulletTimer < 12)
        {
            bulletDamage = 30;
        }
        else if (bulletTimer >= 12 && bulletTimer < 20)
        {
            bulletDamage = 20;
        }
        else if (bulletTimer <= 20 && bulletTimer < 30)
        {
            bulletDamage = 10;
        }
        // scaling bullet damage

        //Debug.Log(bulletTimeMultiplyer);
        //Debug.Log(bulletTimer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "enemy")
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}
}
