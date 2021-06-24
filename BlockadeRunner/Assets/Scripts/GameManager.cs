using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerScore;
    public int points = 0;

    public Text playerScoreText;

    public int timeValue;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timeValue = (int)Time.time;
        timeText.text = timeValue.ToString("0");


        if( points > 0)
        {
        playerScore = points;
        playerScoreText.text = playerScore.ToString("0000");
        }
        else
        {
        playerScoreText.text = 0.ToString("0");            
        }           
    }
}
