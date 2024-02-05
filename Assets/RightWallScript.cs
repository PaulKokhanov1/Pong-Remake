using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class used to detect whether Opponent has been scored on and increase score to player by 1, and restart the game
public class RightWallScript : MonoBehaviour
{
    private LogicManagerScript logic;
    private GameUI gameUI;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
        gameUI = GameObject.FindGameObjectWithTag("Screen").GetComponent<GameUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameUI.addScorePlayer(1);
        logic.restartGame();
    }
}
