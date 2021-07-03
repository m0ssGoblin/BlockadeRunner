using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Score calculation variables")]
    public int playerScore;
    public int previousPlayerScore;
    public int pointsToAdd = 0;
    public Text playerScoreText;

    public int timeValue;
    public Text timeText;
    public int previousNumberOfGoalsReached = 0;
    public int numberOfGoalsReached = 0;
    public float timeOfCurrentGoal = 0;    
    public float timeOfLastGoal = 0;
    public int timeBetweenGoals = 0;
    public int totalGoalsInScene = 10;     
   
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Update the time
        timeValue = (int)Time.time;
        timeText.text = (Time.time - timeOfLastGoal).ToString("0");

        timeManager();
        pointCalculator();
        
        //update score UI
        playerScoreText.text = playerScore.ToString("0");

        //end the scene and load the level ciomplete scene if all thegaosl are collected
        if(numberOfGoalsReached == totalGoalsInScene)
        {  
        endScene();
        }

    }

    void timeManager()
    {
        if(timeOfCurrentGoal > timeOfLastGoal) // if a goal has been scored 
        {
            if(timeBetweenGoals == 0)
            {
                timeBetweenGoals = (int)(timeOfCurrentGoal - Time.time);
            }
            timeBetweenGoals = (int)(timeOfCurrentGoal - timeOfLastGoal); //then calculate how much time has elapsed and save it for later
            timeOfLastGoal = timeOfCurrentGoal; //cahneg the time of last goal to right now             
        }        
    }
    void pointCalculator()
    {
        if(numberOfGoalsReached > previousNumberOfGoalsReached)
        {
        playerScore += (pointsToAdd / timeBetweenGoals);
        pointsToAdd = 0;
        previousNumberOfGoalsReached = numberOfGoalsReached;
        }
    }

    void endScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Load Next Scene!");    
    }


}
