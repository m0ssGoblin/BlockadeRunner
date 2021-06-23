using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI : MonoBehaviour
{

    Transform shipTransform;

    Vector3 initialPosition;
    Transform target;

    GameObject targetObject;

    Rigidbody rb;

    Quaternion currentRotation;

    int sensorRange = 4000;

    int weaponRange = 1000;

    int shipSpeed = 10000;

    float rotationSpeed = .5f;

    float targetDistance;
    float stoppingDistance = 250;

    float timeBetweenMoves = 10;
    float nextMoveTime;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
        
        shipTransform = GetComponent<Transform>();
        initialPosition = shipTransform.position;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
        
        shipTransform = GetComponent<Transform>();
        targetObject = GameObject.FindGameObjectWithTag("Player");
        target = targetObject.GetComponent<Transform>();
        targetDistance = Vector3.Distance(shipTransform.position, target.position);


        //if (targetDistance < sensorRange)
        //{
            pursue();
            Debug.Log("enemy Ship in Pursuit");
        //}

        //else
        //{
            patrol();
            Debug.Log("enemy Ship Patrolling");
        //}

        
    }

    void patrol()
    {
        if(Time.time > nextMoveTime)
        {
        
        int ran1 = Random.Range(200,500);
        int ran2 = Random.Range(200,500);
        int ran3 = Random.Range(200,500);
        Vector3 randomPoint;
        randomPoint = new Vector3 (ran1,ran2,ran3);

        Quaternion targetRotation;
        Vector3 targetDirection;
        targetDirection = randomPoint - transform.position;
        targetRotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);
        
        rb.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed));     

        rb.AddRelativeForce(Vector3.forward * shipSpeed*.5f);

        nextMoveTime += timeBetweenMoves;

        }

        

    }


    void pursue()
    {

        if(targetDistance<stoppingDistance)        
        {
        
        Quaternion targetRotation;
        Vector3 targetDirection;
        targetDirection = target.transform.position - transform.position;
        targetRotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);
        
        rb.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed));     

        rb.velocity = (Vector3.zero);
        rb.AddRelativeForce(Vector3.forward * shipSpeed);

        }
        
    }

    void attack()
    {
        
    }
}
