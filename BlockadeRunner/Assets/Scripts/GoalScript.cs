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
    public bool timeLogged = false;

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

        if( Vector3.Distance(player.position , goal.position) < (goal.localScale.magnitude / 1.5) && !goalReached)
        {
            Destroy(this.gameObject);
            goalReached = true;
            pointAdder();

            //log the time once
            if(!timeLogged)
            {
                timeLogger();
            }
        }        
    }

    void pointAdder()
    {
        //get access to gamemanager script and points variable so we can add points 
        GameObject gamemanagerObject = GameObject.FindGameObjectWithTag("Manager");
        GameManager manager = gamemanagerObject.GetComponent<GameManager>();
        manager.pointsToAdd += pointValue;
        manager.numberOfGoalsReached += 1;
        pointsAdded = true;
        
    }
    void timeLogger()
    {
        GameObject gamemanagerObject = GameObject.FindGameObjectWithTag("Manager");
        GameManager manager = gamemanagerObject.GetComponent<GameManager>();
        manager.timeOfCurrentGoal = Time.time;
        timeLogged = true;
    }

    
}
