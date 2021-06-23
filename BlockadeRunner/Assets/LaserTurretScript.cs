using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretScript : MonoBehaviour
{
    float timeBetweenShots = .5f;
    float nextActionTime;
    Transform turretTransform;
    Transform targetTransform;
    public int range =1000;

    Vector3 downwardDirection;

    Vector3 forwardDirection;
    Vector3 laserSpawnPoint;

    float spawnPositionOffsetMultiplier = 50;

    // Start is called before the first frame update
    public Rigidbody missile;

    private void Start() 
    {

    }
    private void Update() 
    {
        turretTransform = GetComponent<Transform>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        downwardDirection = -turretTransform.up;
        forwardDirection = transform.forward;


        laserSpawnPoint = (downwardDirection * spawnPositionOffsetMultiplier) + (forwardDirection * spawnPositionOffsetMultiplier) + turretTransform.position;

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
