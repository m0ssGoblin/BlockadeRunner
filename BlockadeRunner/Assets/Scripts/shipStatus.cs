using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shipStatus : MonoBehaviour
{
    Rigidbody rb;
    public float damageConstant = .5f;
    public Slider healthSlider;
    public float healthValue;

        //UI variables

    public Slider combinedVectorSlider;


    public Text combinedVector;
    public GameObject boostText;
    public GameObject dampenerText;
    public Vector3 Velocity;
    public float combinedVelocity;
    public Vector3 currentAngularVelocty;

    // Start is called before the first frame update
    void Start()
    {
        healthValue = 1000;
        rb = GetComponent<Rigidbody>(); 

        boostText.SetActive(false);
        dampenerText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = healthValue;
        currentAngularVelocty = rb.angularVelocity;
    }

    private void OnCollisionEnter(Collision collider) 
    {
        if(healthValue > 0)
        {
        float damage = collider.impulse.magnitude * damageConstant;
        healthValue -= damage;
        Debug.Log(healthValue);
        }
        
    }

    private void FixedUpdate() {
        
        
        //UI 
        
        int combinedVectorInt = Mathf.Abs((int)rb.velocity.magnitude);
        combinedVectorSlider.value = combinedVectorInt/10000f;

        combinedVector.text = combinedVectorInt.ToString("0000");

        combinedVelocity = rb.velocity.magnitude;


        Velocity = rb.velocity;

    }

}
