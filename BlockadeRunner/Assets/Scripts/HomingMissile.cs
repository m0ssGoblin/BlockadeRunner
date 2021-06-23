using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    Rigidbody missileRB;
    Rigidbody playerRB;
    Transform playerTransform;
    Transform missileTransform;
    GameObject player;
    Vector3 playerPosition;
    Vector3 missilePositon;

    public int launchSpeed = 750;
    public float rotationSpeed = .01f;
    public int missileAcceleration = 100;
    public int timeToHome = 3;

    public float maxSpeed=1000;
    public float maxDeltaV = 10;
    public float distanceToHome = 100;
    float deltaV;
    float deltaP;

    // Start is called before the first frame update
    void Start()
    {
        //grab transform of the missile to whom this script is attached.
        missileRB = GetComponent<Rigidbody>();
        missileTransform = GetComponent<Transform>();
        
        
        //see if we can find the player 
        player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            playerPosition = player.GetComponent<Transform>().position;
            Debug.Log("player found: " + playerPosition);
        }   

        launch();

        StartCoroutine(home()); 
    }
    

    // Update is called once per frame
    void Update()
    {        
        missilePositon = missileTransform.position;
        
        playerTransform = player.GetComponent<Transform>();
        playerPosition = playerTransform.position;
       
        playerRB = GetComponent<Rigidbody>();

        //grab useful data
        deltaV = playerRB.velocity.magnitude - missileRB.velocity.magnitude;
        deltaP = Vector3.Distance(playerPosition, missilePositon);
                
    }
    private void FixedUpdate() 
    {
                 
    }





    void launch()
    {
        missileRB.AddRelativeForce(Vector3.forward*launchSpeed);

    }

    IEnumerator home()
    {

        if(Time.time > timeToHome)
        {
            /// this seciton needs ot be in an update method or it will not work properly. 
            Vector3 direction = playerPosition - transform.position;
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.time);

//            Vector3 absoluteVelocity = missileRB.velocity;
            
            yield return new WaitForSeconds(1f);
            
            missileRB.AddRelativeForce(Vector3.forward*missileAcceleration);
        }


        
    }
}
