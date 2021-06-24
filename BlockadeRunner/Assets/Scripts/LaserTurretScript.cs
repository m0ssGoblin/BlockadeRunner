using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretScript : MonoBehaviour
{
    float timeBetweenShots = 1f;
    float nextActionTime;
    Transform turretTransform;
    Transform targetTransform;
    public int range =1000;

    Vector3 laserSpawnPoint;

    float spawnPositionOffsetMultiplier = 75;

    // Start is called before the first frame update
    public Rigidbody missile;

    private void Start() 
    {

    }
    private void Update() 
    {
        //get the transform of the thing firing the lasers and the thing its shooting at
        turretTransform = GetComponent<Transform>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if(targetTransform != null)
        {
            laserSpawnPoint = (transform.forward * spawnPositionOffsetMultiplier) + turretTransform.position;

            if (Vector3.Distance(targetTransform.position, turretTransform.position) < range)
            {
                if (Time.time > nextActionTime)
                {
                    nextActionTime += timeBetweenShots;
                    Instantiate(missile, laserSpawnPoint, turretTransform.rotation); 
                    
                }
            }
        }             
    }
}
