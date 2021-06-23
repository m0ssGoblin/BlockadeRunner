using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    Transform transform;
    Rigidbody Rb;
    GameObject target;
    
    public Transform targetTransform;

    float rotationSpeed = .1f;
    int range = 1000;

    Quaternion currentRotation;

    Quaternion targetRotation;

    public bool onTarget = false;

    int tolerance = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        transform = GetComponent<Transform>();
        Rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetTransform = target.GetComponent<Transform>();

        Quaternion targetRotation;
        Vector3 targetDirection;
        targetDirection = target.transform.position - transform.position;
        targetRotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);

        currentRotation = transform.rotation;

        Vector3 targetEuler = targetRotation.eulerAngles;
        Vector3 currentEuler = currentRotation.eulerAngles;

        if (currentEuler.y > targetEuler.y-tolerance && currentEuler.y < targetEuler.y+tolerance)
        {
            onTarget = true;
        }
        else
        {
            onTarget = false;
        }
    }


    private void FixedUpdate()
    {
        transform = GetComponent<Transform>();
        Rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetTransform = target.GetComponent<Transform>();

        if (Vector3.Distance(targetTransform.position, transform.position) < range)
        {
            track();
        }
        else
        {
            //chill
        }

    }

    void track()
    {
        transform = GetComponent<Transform>();
        Rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetTransform = target.GetComponent<Transform>();
        
        Quaternion targetRotation;
        Vector3 targetDirection;
        targetDirection = target.transform.position - transform.position;
        targetRotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);
        targetRotation.x = 0;
        targetRotation.z = 0;
        Rb.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed));
    }
}
