using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machineGun : MonoBehaviour
{
    Transform bulletTransform;
    public Transform targetTransform;
    public GameObject bullet;
    public GameObject target;


    public GameObject turretHead;

    Vector3 targetPosition;
    int range = 1000;
    float fireRate = 1;
    float lastShotTime = 0;

    public bool onTarget = false;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletTransform = GetComponent<Transform>();
        target = GameObject.FindWithTag("Player");

        //set some variables to the transform of the target so we can know where it is. 
        targetTransform = target.GetComponent<Transform>();
        targetPosition = targetTransform.position;

    }

    private void FixedUpdate()
    {
        onTarget = turretHead.GetComponent<TurretScript>().onTarget;

        float targetDistance = Vector3.Distance(transform.position, targetTransform.position);

        if (targetDistance < range && Time.time > lastShotTime + (3.0f / fireRate) && onTarget)
        {
            shoot();
            lastShotTime = Time.time;
        }
    }

    void shoot()
    {

        Instantiate(bullet, transform.position + transform.forward*2, transform.rotation);

    }
}
