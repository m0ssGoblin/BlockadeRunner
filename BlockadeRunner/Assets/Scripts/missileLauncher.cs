using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class missileLauncher : MonoBehaviour
{
    public float timeBetweenShots = 3;
    public float nextActionTime;
    Transform towerTransform;
    Transform targetTransform;
    int sensorRange =2500;

    // Start is called before the first frame update
    public Rigidbody missile;

    private void Start() 
    {
        towerTransform = GetComponent<Transform>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update() 
    {
        if (Vector3.Distance(targetTransform.position, towerTransform.position) < sensorRange)
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += timeBetweenShots;
                Instantiate(missile, towerTransform.position + new Vector3(0, 5, 0), towerTransform.rotation);
            }
        }
                      
    }
}