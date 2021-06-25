using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{
    public Text playerScoreText;
    int playerScore = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreCounter();
    }

    void scoreCounter()
    {
        GameObject gamemanagerObject = GameObject.FindGameObjectWithTag("Manager");
        GameManager manager = gamemanagerObject.GetComponent<GameManager>();   

        if(playerScore != manager.playerScore)
        {
            Mathf.Lerp(playerScore, manager.playerScore,1);
        }           
        playerScoreText.text = playerScore.ToString("0");
    }
}
