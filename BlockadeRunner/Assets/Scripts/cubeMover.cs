using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMover : MonoBehaviour
{
    Transform tranform;

    public Rigidbody rb;

    int speed = 50;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        rb.AddForce(Vector3.forward * speed);
    }

}
