using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    Rigidbody rb;

    [Header("Control Variables")]
    public bool boostEngaged = false;       
    int boost = 3;

    int forwardSpeed = 500, strafeSpeed = 50, hoverSpeed = 100;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 3f, strafeAcceleration = 2f, hoverAcceleration = 3f;
    public int maxVelocity = 1000;
    //public float deadZone = .25f;
    //public float lookRateSpeed = 600f;
    private Vector2 lookInput, screenCenter, mouseDistance;
    float pitchInput;
    float yawInput;    
    float rollInput;
    float pitchSpeed = 500, pitchAcceleration = 200f;
    float yawSpeed = 500, yawAcceleration = 75f;
    float rollSpeed = 600, rollAcceleration = 200f;

    [Header("Dampener Variables")] 
    public bool dampenersEngaged = true;
    public float dampenerDrag = 2f;
    float normalAngularDrag = .5f;
    public float angularDampener = 1.5f;


// Start is called before the first frame update
private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }


    void FixedUpdate()
    {

        movementHandler();        
        rotationHandler();


        rb.AddRelativeForce(activeStrafeSpeed, activeHoverSpeed, activeForwardSpeed);        
    }


    void rotationHandler()
    {
        //configure roll axis input
        pitchInput = Mathf.Lerp(pitchInput, Input.GetAxisRaw("Pitch"), pitchAcceleration * Time.deltaTime);

        //yawInput = Mathf.Lerp(yawInput, Input.GetAxisRaw("Yaw"), yawAcceleration * Time.deltaTime);
        
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);


        rb.AddRelativeTorque(pitchInput * pitchSpeed * Time.deltaTime , yawInput * yawSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime);

    }

    void movementHandler()
    {
        if(Input.GetKey("space"))
        {
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Throttle") * forwardSpeed * boost, forwardAcceleration*Time.deltaTime);
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Yaw") * strafeSpeed * boost, strafeAcceleration * Time.deltaTime);
            activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed * boost, hoverAcceleration * Time.deltaTime);  
            boostEngaged = true;
        }
        else
        {
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Throttle") * forwardSpeed, forwardAcceleration*Time.deltaTime);
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Yaw") * strafeSpeed, strafeAcceleration * Time.deltaTime);
            activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);
            boostEngaged = false;
        }  
        
        //Inertial Dampeners

        if(Input.GetAxisRaw("Brake") != 0)
        {
            dampenersEngaged = true;
            rb.drag = dampenerDrag;
        }
        else
        {
            dampenersEngaged = false;
            rb.drag = .5f;
        }
    }
}
