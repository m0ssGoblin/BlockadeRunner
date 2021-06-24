using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAITest : MonoBehaviour
{
     public Rigidbody Rb; 
    public Transform shipTransform;
    public Transform targetTransform;
          
    float rotationSpeed = .5f;
    float speed = 10000;

    int sensorRange = 4000;

    int weaponRange;

    public int stoppingDistance = 0;

    public float speedDistanceConstant = 100000;




    // Start is called before the first frame update
    void Start()
    {
        LaserTurretScript LaserTurretScript=  GetComponent<LaserTurretScript>();
        weaponRange = LaserTurretScript.range;              
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
