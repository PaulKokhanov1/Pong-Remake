using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicManagerScript : MonoBehaviour
{

    public float aiDeadzone = 1f;
    public float moveSpeed = 10f;
    public float delay = 1;
    private int direction = 0;
    private float moveSpeedMultiplier = 1f;


    private BallMoveScript ball;
    private PlayerMoveScript player;
    public GameObject Opponent;
    public Rigidbody2D OpponentRigidBody;
    public GameUI gameUI;
    private Vector2 startPosAi;
    private Vector2 velocityAi;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMoveScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveScript>();
        gameUI = GameObject.FindGameObjectWithTag("Screen").GetComponent<GameUI>();

    }

    private void FixedUpdate()
    {
        //end game condition, once player or opponent reach 5 points -> game ends
        if (gameUI.playerScore >= 5 || gameUI.opponentScore >= 5)
        {
            //used to pause game, not most efficient method, but works for my purposes of the game
            Time.timeScale = 0;
            endGame();
        } else
        {
            moveAi();
        }

        //debugging condition, useful to reset game if game goes into never ending situation
        if (Input.GetKey(KeyCode.Escape))
        {
            restartGame();
        }
    }

    private void moveAi()
    {
        Vector2 ballPos = ball.transform.position;       
        //making Opponent follow position of ball only when ball is on their half of the field
        if ((Opponent.transform.position.x - ballPos.x ) <= Opponent.transform.position.x)
        {       
            //creating deadzone between ball y position and Opponent y position to create difficulty of AI
            //Adjusting Difficulty means decreasing deadzone and increaing moveSpeed
            if (Mathf.Abs(ballPos.y - Opponent.transform.position.y) > aiDeadzone)
            {
                direction = ballPos.y > Opponent.transform.position.y ? 1 : -1;
            }
        }
        else
        {
            //not moving opponent when ball is in player half of the field
            direction = 0;
        }

        //only called once in a hundred times, so roughly at 60 fps -> 100/60 = 1.67 times a second
        if (Random.value < 0.01f)
        {
            //slithglty adjusting Opponent speed throughout the game
            moveSpeedMultiplier = Random.Range(0.75f, 1.25f);
        }


        Move();

        
    }

    //movement for Opponent, uses players's speed as reference and then adjusts as necessary, for example depending on difficulty
    private void Move()
    {
        velocityAi = player.playerRigidbody.velocity;
        velocityAi.y = moveSpeed * moveSpeedMultiplier * direction;
        OpponentRigidBody.velocity = velocityAi;

    }


    //restart game entails, resetting player, opponent & ball position, aswell as re-shooting ball in random direction
    public void restartGame()
    {
        ball.transform.position = Vector3.zero;
        ball.randomDirection();
        player.transform.position = new Vector3(-7.839f, 0.0f, 0.0f);
        Opponent.transform.position = new Vector3(7.839f, 0.0f, 0.0f);

    }

    public void endGame()
    {
        gameUI.endScreen.SetActive(true);
    }
}
