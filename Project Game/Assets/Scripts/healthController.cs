using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 7);
        // destory pickup after 7 seconds
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
