using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // enum levels { menu, game };
    AudioSource menuMusic;

    // Start is called before the first frame update
    void Start()
    {
        menuMusic = GetComponent<AudioSource>();
        menuMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void startOnClick()
    {
        SceneManager.LoadScene(1);
    }
}
