using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbProjectileAI : MonoBehaviour
{
    Rigidbody bulletRB;
    Rigidbody targetRb;
    Transform targetTransform;
    Transform bulletTransform;
    public GameObject bullet;
    GameObject target;
    public GameObject explosion;
    Vector3 targetPosition;
    Vector3 bulletPositon;
    int speed = 50000;
    Rigidbody rb;
    int explosionRadius = 20;
    int explosionForce = 1000;

    public int damageOnCollision = 75;

    // Start is called before the first frame update
    void Start()
    {
        
        bulletRB = GetComponent<Rigidbody>();

        //grab transform of the enemey to whom this script is attached.
        bulletTransform = GetComponent<Transform>();
        
        //see if we can find the target 
        target = GameObject.FindWithTag("Player");

        //set some variables to the transform of the target so we can know where it is. 
        targetTransform = target.GetComponent<Transform>();
        targetPosition = targetTransform.position;

        //also set a variable equal to our transform so we can calculate the distance to things. 
        bulletPositon = transform.position;
        
        bulletPositon = transform.position; 
        
        rb = GetComponent<Rigidbody>();
        
        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);   


        if (target != null)
        {
            shoot();
        }

    }

    void shoot()
    {
        bulletRB.AddForce((targetPosition - bulletPositon).normalized * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, bulletPositon, bulletTransform.rotation);
        Destroy(gameObject);
    }


}
