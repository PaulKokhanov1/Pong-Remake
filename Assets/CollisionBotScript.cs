using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Handles Edge Case where ball glitches through Bottom Wall and flys endlessly
public class CollisionBotScript : MonoBehaviour
{
    private LogicManagerScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        logic.restartGame();
    }
}
