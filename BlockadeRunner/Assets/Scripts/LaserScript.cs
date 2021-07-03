using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    Rigidbody projectileRB;
    Rigidbody targetRb;
    Transform targetTransform;
    Transform projectileTransform;
    GameObject projectile;
    GameObject target;
    public GameObject explosion;
    Vector3 targetPosition;
    Vector3 projectilePositon;
    int speed = 500000;
    Rigidbody rb;
    int blastRadius = 50;
    int explosionForce = 1000;

    int maximumDistance = 50000;

    // Start is called before the first frame update
    void Start()
    {

        projectileRB = GetComponent<Rigidbody>();

        //grab transform of the enemey to whom this script is attached.
        projectileTransform = GetComponent<Transform>();

        //see if we can find the target 
        target = GameObject.FindWithTag("Player");

        //set some variables to the transform of the target so we can know where it is. 
        targetTransform = target.GetComponent<Transform>();
        targetPosition = targetTransform.position;

        //also set a variable equal to our transform so we can calculate the distance to things. 
        projectilePositon = transform.position;


        if (target != null)
        {
            shoot();
        }

    }

    void shoot()
    {
        projectileRB.AddRelativeForce(Vector3.forward * speed * Time.deltaTime);

        //projectileRB.AddForce((targetPosition - projectilePositon).normalized * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        projectilePositon = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
        Instantiate(explosion, projectilePositon, projectileTransform.rotation);
        Destroy(gameObject);

    }

    private void Update() {
                //see if we can find the target 
        target = GameObject.FindWithTag("Player");

        //set some variables to the transform of the target so we can know where it is. 
        targetTransform = target.GetComponent<Transform>();
        targetPosition = targetTransform.position;

        float targetDistance = Vector3.Distance(projectilePositon , targetPosition);

        if(targetDistance > maximumDistance){
            Destroy(gameObject);
        }
    }


}
