using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    GameObject target;

    Transform targetTransform;
    
    

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetTransform = target.GetComponent<Transform>();

        StartCoroutine("bounce");
    }

    IEnumerator bounce()
    {
        while(true)
        {
            if(Vector3.Distance(targetTransform.position, transform.position) < 10)
            {
                rb = GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * 200);
                yield return new WaitForSeconds(1);
            }            
        }        
    }
}
