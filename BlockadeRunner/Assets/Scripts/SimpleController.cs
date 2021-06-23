using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    Rigidbody Rb;
    float speed = 300;
    int rotationSpeed = 6;

    int maxSpeed = 500;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    private void FixedUpdate() 
    {
        Rb.AddRelativeForce(0, Input.GetAxis("Hover"), Input.GetAxis("Vertical")*speed);
        Rb.AddRelativeTorque(0, Input.GetAxis("Horizontal")*rotationSpeed, 0);
    
        if(Rb.velocity.magnitude > maxSpeed)
        {
            Rb.velocity = Rb.velocity.normalized * maxSpeed;
        }        
    }
}
