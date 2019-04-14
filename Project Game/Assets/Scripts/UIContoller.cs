using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContoller : MonoBehaviour
{
    public playerController playerScript;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text ammoText;


    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + playerScript.score;

        if (playerScript.health >= 0)
        {
            healthText.text = "Health: " + playerScript.health;
        }
        else if (playerScript.health < 0)
        {
            healthText.text = "Health: 0";
        }

        ammoText.text = "Ammo: " + playerScript.ammoCount;
    }
}
