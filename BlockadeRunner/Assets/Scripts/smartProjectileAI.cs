using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smartProjectileAI : MonoBehaviour
{
    Rigidbody enemyRB;
    Rigidbody playerRB;
    Transform playerTransform;
    Transform enemyTransform;
    GameObject player;
    Vector3 playerPosition;
    Vector3 enemyPositon;
    public float interval = 5;



    public int speed = 100;

    public int distanceToStop = 100;

    // Start is called before the first frame update
    void Start()
    {
        //not used but maybe used later
        enemyRB = GetComponent<Rigidbody>();
        //grab transform of the enemey to whom this script is attached.
        
        GameObject player = GameObject.FindWithTag("Player");
        enemyTransform = GetComponent<Transform>();       
        if(player != null)
            {
                Debug.Log("an enemy cannot find the player gameobject");
            }
        playerRB = player.GetComponent<Rigidbody>();
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() 
    {
        homing();
    }

    void homing()
    {
        //see if we can find the player 

        //set some variables to the rigidbody and transform of the player so we can know where it is. 
        playerTransform = player.GetComponent<Transform>();
        playerPosition = playerTransform.position;

        //also set a variabel equal to our transform so we can calculate the distance to things. 
        enemyPositon = transform.position;
         transform.LookAt(playerPosition);

         if(Vector3.Distance(transform.position, playerPosition) > distanceToStop)
         {
            enemyRB.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
         }
         else
         {
            Vector3 playerVelocity = playerRB.velocity;
            Vector3 enemyVelocity = enemyRB.velocity;
            enemyRB.AddForce(playerVelocity - enemyVelocity);
            enemyRB.AddRelativeForce(Vector3.forward * speed*.5f, ForceMode.Force); 
         }


     }
     void moveTowardsPlayer()
     {
        playerTransform = player.GetComponent<Transform>();
        playerPosition = playerTransform.position;

        //also set a variabel equal to our transform so we can calculate the distance to things. 
        enemyPositon = transform.position;
        enemyRB.AddForce(playerPosition-enemyPositon);    
     }
    
}
