using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public Rigidbody Rb; 
    public Transform missileTransform;
    public Rigidbody targetRb;
    public Transform targetTransform;
    private int launchSpeed =5000;
    public int timeToTargetLock = 3;
    public int timeToHome = 4;
    public float rotationSpeed = .5f;
    private int homeSpeed = 10000;
    GameObject target;
    GameObject missile;
    float spawnTime;
    int distanceToCoast = 50;
    public GameObject explosionEffect;
    public int explosionRadius = 5;
    int explosionForce = 3000;
    public int damageOnCollision = 100;
    

    
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        Rb = GetComponent<Rigidbody>();
        missileTransform = GetComponent<Transform>();
        target = GameObject.FindGameObjectWithTag("Player");

        targetRb = target.GetComponent<Rigidbody>();
        targetTransform = target.GetComponent<Transform>();

        Rb.useGravity = false;
        Rb.drag = 0;

        launch();
    }

    private void FixedUpdate() 
    {
        Quaternion targetRotation;
        Vector3 targetDirection;
        targetDirection = target.transform.position - transform.position;
        targetRotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);

        if(Time.time>timeToTargetLock)
        {
            Rb.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed));     
        } 

        if ((Time.time - spawnTime) > timeToHome)
        {
            Rb.velocity = (Vector3.zero);
            Rb.AddRelativeForce(Vector3.forward * homeSpeed);
        }
        if (Vector3.Distance(transform.position, targetTransform.position) < 1)
        {
            explode();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collide) 
    {
        explode();
    }
    
    void launch()
    {
        Rb.AddRelativeForce(Vector3.forward*launchSpeed);
    }
    void explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        Destroy(gameObject);
    }

}
