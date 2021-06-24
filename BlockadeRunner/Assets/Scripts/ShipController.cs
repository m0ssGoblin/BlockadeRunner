using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    Rigidbody rb;

    [Header("Control Variables")]
    public bool boostEngaged = false;       
    public int boost = 5;
    [SerializeField] private int forwardSpeed = 100, strafeSpeed = 50, hoverSpeed = 100;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;
    public int maxVelocity = 1000;
    public float deadZone = .25f;
    public float lookRateSpeed = 600f;
    private Vector2 lookInput, screenCenter, mouseDistance;
    float rollInput;
    float rollSpeed = 800, rollAcceleration = 90f;

    [Header("Dampener Variables")] 
    public bool dampenersEngaged = true;
    public float dampenerDrag = .25f;
    float normalAngularDrag = .5f;
    public float angularDampener = 1.5f;
    public Vector3 currentAngularVelocty;


// Start is called before the first frame update
private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        screenCenter.x = Screen.width *.5f;
        screenCenter.y = Screen.height * .5f;
    }

    void Update() 
    {
        if(Input.GetKeyDown("z"))
        {
            dampenersEngaged = !dampenersEngaged;
        }
        currentAngularVelocty = rb.angularVelocity;
    }

    void FixedUpdate()
    {
        rotationHandler();
        movementHandler();
        
        
    }

    void rotationHandler()
    {
        

        //rotation  handler
            
            //grab mouse position 
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

            //divide mouse input by smaller screen dimension to normalize inputs and save these numbers
        mouseDistance.x = (lookInput.x - screenCenter.x)/ screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
            //only allow values between 0 and 1 
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        //configure roll axis input
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
        
        
            //only add torque to the ship if the mouse is outside of a small circle in the center of the screen
                //within the circle the angular drag increases to give the playe rmore control.                
        if (mouseDistance.magnitude > deadZone)
        {
            rb.angularDrag = normalAngularDrag;
            rb.AddRelativeTorque(-mouseDistance.y * lookRateSpeed * Time.deltaTime,  mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime);            
        }
        else
        {
            rb.angularDrag = normalAngularDrag * angularDampener;
            rb.AddRelativeTorque(0,0, rollInput*rollSpeed*Time.deltaTime); 
        }

    }

    void movementHandler()
    {
        if(rb.velocity.magnitude < maxVelocity)
        {
                if(Input.GetKey("left shift"))
            {
                activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed * boost, forwardAcceleration*Time.deltaTime);
                activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed * boost, strafeAcceleration * Time.deltaTime);
                activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed * boost, hoverAcceleration * Time.deltaTime);  
                boostEngaged = true;
            }
            else
            {
                activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration*Time.deltaTime);
                activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
                activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);
                boostEngaged = false;
            }  
        }
        else
        {
            rb.velocity = rb.velocity.normalized * .9f;
        }
     

        //Inertial Dampeners

        if (dampenersEngaged)
        {
               rb.drag = dampenerDrag;
        }
        else
        {
            rb.drag = 0f;
        }

        rb.AddRelativeForce(activeStrafeSpeed, activeHoverSpeed, activeForwardSpeed);
    }

    

}
