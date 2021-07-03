using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinner : MonoBehaviour
{
    int spinSpeed = 1000;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spinVector;
        spinVector.x = Random.Range(.5f,1);
        spinVector.y = Random.Range(.5f,1);
        spinVector.z = Random.Range(.5f,1);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddRelativeTorque(spinVector * spinSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
