using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class shipStatus : MonoBehaviour
{

    public GameObject explosion;
    Rigidbody rb;
    public float damageConstant = .5f;
    public Slider healthSlider;
    public int healthValue;
    public Text healthText;
    
    [Header("UI Variables")] 
        public Slider velocitySlider;
    public Text velocityText;
    public GameObject boostText;
    public GameObject dampenerText;
    public Vector3 Velocity;

[Header("Ship Controller variables")]
    ShipController shipController;

    // Start is called before the first frame update
    void Start()
    {
        healthValue = 1000;
        rb = GetComponent<Rigidbody>(); 

        boostText.SetActive(false);
        dampenerText.SetActive(true);

        shipController = GetComponent<ShipController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Velocity UI 
        int maxVelocity = shipController.maxVelocity;
        int absoluteVelocityInt = Mathf.Abs((int)rb.velocity.magnitude);
        velocitySlider.value = absoluteVelocityInt/maxVelocity;
        velocityText.text = absoluteVelocityInt.ToString("0");    
        Velocity = rb.velocity;

        //Health UI
        healthSlider.value = healthValue;
        healthText.text = healthValue.ToString("0");


        //Dampener UI 
        if(shipController.dampenersEngaged)
        {
            dampenerText.SetActive(true);
        }
        else
        {
            dampenerText.SetActive(false);
        }

        //Boost UI 
        if(shipController.boostEngaged)
        {
            boostText.SetActive(true);
        }
        else
        {
            boostText.SetActive(false);
        }

        //blow up if health < 0
        if(healthValue<0)
        {
            StartCoroutine("death");
        }
    }

    private void OnCollisionEnter(Collision collider) 
    {

        //Deal damage

        if(healthValue > 0)
        {
        float damage = collider.impulse.magnitude * damageConstant;
        healthValue -= (int)damage;
        }

    }
    IEnumerator death()
    {        
    Instantiate(explosion, transform.position, Quaternion.identity);
    Destroy(this.gameObject);
    yield return new WaitForSeconds(5);
    RestartScene();        
    }

    public void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

}
