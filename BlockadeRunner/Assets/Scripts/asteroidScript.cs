using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidScript : MonoBehaviour
{
    Rigidbody rb;
    Transform trans;
    Vector3 dimension;
    Vector3 pos;
    Vector3 offset;

    int scale;
    int torque;

    int offsetMin = -1000;
    int offsetMax = 1000;
    

    // Start is called before the first frame update
    void Start()
    {

        // get components 
        rb = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();

        rb.angularDrag = 0;
        rb.useGravity = false;

        //get random number to use for dimensions
        scale = Random.Range(20,100);
        dimension.x= scale;
        dimension.y= scale;
        dimension.z= scale;

        transform.localScale = dimension;
       

        //offset the position of the asteroids within a random range by adding a randomly generated offset value to the asteroids posiiton when placed
        
        pos = trans.position;

             
        offset.x= Random.Range(offsetMin,offsetMax) + pos.x;
        offset.y= Random.Range(offsetMin,offsetMax) + pos.y;
        offset.z= Random.Range(offsetMin,offsetMax) + pos.z;

        trans.position = offset;

        //apply random torque ammount to asteroid

       int levelOfTorque;
       levelOfTorque = Random.Range(0,4) ;
       if ( levelOfTorque < 3)
       {
           rb.AddRelativeTorque(Random.Range(0,50),Random.Range(0,50),Random.Range(0,50));
       }
       
       else
       {
           rb.AddRelativeTorque(Random.Range(0,500),Random.Range(0,500),Random.Range(0,500));
       }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
