using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2ShipController : MonoBehaviour
{
    Rigidbody rb;

    [Header("Control Variables")]
       
    public int boost = 5;
    [SerializeField] private float forwardSpeed = 300, strafeSpeed = 200f, hoverSpeed = 200f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;
    public float deadZone = .25f;
    public float lookRateSpeed = 600f;
    private Vector2 lookInput, screenCenter, mouseDistance;
    float rollInput;
    float rollSpeed = 800, rollAcceleration = 90f;

    [Header("Dampener Variables")] 
    public int dampenerDrag = 1;
    public bool dampenersEngaged = true;
    public float dampMagnitude = .5f;
    float normalAngularDrag = .8f;
    public float angularDampener = 1.5f;


    [Header("UI Variables")] 
    public Slider zVectorSlider;
    public Slider xVectorSlider;
    public Slider yVectorSlider;
    public Slider combinedVectorSlider;
    public Text zVector;
    public Text xVector;
    public Text yVector;
    public Text combinedVector;
    public GameObject boostText;
    public GameObject dampenerText;
    public Vector3 Velocity;
    public float combinedVelocity;
    public Vector3 currentAngularVelocty;


// Start is called before the first frame update
private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        screenCenter.x = Screen.width *.5f;
        screenCenter.y = Screen.height * .5f;
        boostText.SetActive(false);
        dampenerText.SetActive(true);

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
        

           

        
        //movement handler 

        if(Input.GetKey("left shift"))
        {
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed * boost, forwardAcceleration*Time.deltaTime);
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed * boost, strafeAcceleration * Time.deltaTime);
            activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed * boost, hoverAcceleration * Time.deltaTime);  
            boostText.SetActive(true);
        }
        else
        {
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration*Time.deltaTime);
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
            activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);
            boostText.SetActive(false);
        }    

        //Inertial Dampeners  WIP!

        if (dampenersEngaged)
        {
               //rb.AddRelativeForce(Mathf.Lerp (-Velocity.x, 0f,dampMagnitude), Mathf.Lerp(-Velocity.y, 0f, dampMagnitude), Mathf.Lerp(-Velocity.z, 0f, dampMagnitude));
               //rb.AddRelativeForce(-Velocity.x, -Velocity.y, -Velocity.z);
               rb.drag = 1f;
               dampenerText.SetActive(true);
        }
        else
        {
            rb.drag = 0f;
            dampenerText.SetActive(false);
        }



        //transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        //transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime)+ (transform.up * activeHoverSpeed * Time.deltaTime);
     
        rb.AddRelativeForce(activeStrafeSpeed, activeHoverSpeed, activeForwardSpeed);
 
        //UI 
        
        int zVectorInt = Mathf.Abs((int)rb.velocity.z);
        zVectorSlider.value = zVectorInt/10000f;
        int xVectorInt = Mathf.Abs((int)rb.velocity.x);
        xVectorSlider.value = xVectorInt/10000f;
        int yVectorInt = Mathf.Abs((int)rb.velocity.y);
        yVectorSlider.value = yVectorInt/10000f;
        int combinedVectorInt = Mathf.Abs((int)rb.velocity.magnitude);
        combinedVectorSlider.value = combinedVectorInt/10000f;

        zVector.text = zVectorInt.ToString("0000");
        xVector.text = xVectorInt.ToString("0000");
        yVector.text = yVectorInt.ToString("0000");
        combinedVector.text = combinedVectorInt.ToString("0000");

        combinedVelocity = rb.velocity.magnitude;


        Velocity = rb.velocity;

        
    }

    

}
