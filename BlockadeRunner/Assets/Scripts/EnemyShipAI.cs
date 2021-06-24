using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI : MonoBehaviour
{
     public Rigidbody Rb; 
    public Transform shipTransform;
    public Transform targetTransform;
          
    float rotationSpeed = .5f;
    float speed = 10000;

    int sensorRange = 4000;

    int weaponRange;

    public int stoppingDistance = 500;

    public float speedDistanceConstant = 100000;

    public GameObject explosion;

    public float health = 1000;

    public float damageConstant =1;


    // Start is called before the first frame update
    void Start()
    {
        //set weapon range equal to the value set in the turretScript
        LaserTurretScript LaserTurretScript=  GetComponent<LaserTurretScript>();       
        weaponRange = LaserTurretScript.range;              
    }

    // Update is called once per frame
    void Update()
    {
        if( health < 0)
        {
            death();
        }
        
    }

    private void FixedUpdate() 
    {
        float targetDistance = Vector3.Distance(transform.position, targetTransform.position);
        
        if(targetDistance < sensorRange)
        {
            pursue();
        }
        if(targetDistance < weaponRange)
        {
            orient();        
        }
    }

    void pursue()
    {     
        float targetDistance = Vector3.Distance(transform.position, targetTransform.position);
        speed = Mathf.Pow(speedDistanceConstant * (targetDistance),.5f);        

        Quaternion targetRotation;
        Vector3 targetDirection;
        targetDirection = targetTransform.position - shipTransform.position;
        targetRotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);

        Rb.MoveRotation(Quaternion.Lerp(shipTransform.rotation, targetRotation, rotationSpeed));     


        Rb.velocity = (Vector3.zero);
        Rb.AddRelativeForce(Vector3.forward * speed);
    }
    void orient()
    {
        Quaternion targetRotation;
        Vector3 targetDirection;
        targetDirection = targetTransform.position - shipTransform.position;
        targetRotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);

        Rb.MoveRotation(Quaternion.Lerp(shipTransform.rotation, targetRotation, rotationSpeed));
     
    }

    private void OnCollisionEnter(Collision collider) 
    {
        health -= collider.impulse.magnitude * damageConstant;        
    }

    public void death()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
