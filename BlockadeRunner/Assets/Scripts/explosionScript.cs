using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{
    float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        float spawnTime = Time.time;    
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnTime > 4f)
        {
            Destroy(gameObject);
        }
    }
}
