using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    Transform goal;     
    Transform player;
    public bool goalReached = false;
    public bool pointsAdded = false;
    public int pointValue = 100;

    // Start is called before the first frame update
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        //get transforms
        goal = GetComponent<Transform>();        
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); 
        player = playerObject.GetComponent<Transform>();

        if( Vector3.Distance(player.position , goal.position) < (goal.localScale.magnitude / 1.5) )
        {
            Debug.Log(Vector3.Distance(player.position , goal.position) + (goal.localScale.magnitude / 2));
            Destroy(this.gameObject);
            goalReached = true;
            pointAdder();
        }
    }

    void pointAdder()
    {
        //get access ot gamemanager script and points variable so we can add points 
        GameObject gamemanagerObject = GameObject.FindGameObjectWithTag("Manager");
        GameManager manager = gamemanagerObject.GetComponent<GameManager>();
        manager.points += pointValue;
        pointsAdded = true;
    }

    
}
