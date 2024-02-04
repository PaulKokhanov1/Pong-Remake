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
    //float timer = 0;
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
        if (gameUI.playerScore >= 5 || gameUI.opponentScore >= 5)
        {
            Time.timeScale = 0;
            endGame();
        } else
        {
            moveAi();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            restartGame();
        }
    }

    private void moveAi()
    {
        Vector2 ballPos = ball.transform.position;       
        if ((Opponent.transform.position.x - ballPos.x ) <= Opponent.transform.position.x)
        {       
            if (Mathf.Abs(ballPos.y - Opponent.transform.position.y) > aiDeadzone)
            {
                direction = ballPos.y > Opponent.transform.position.y ? 1 : -1;
            }
        }
        else
        {
            direction = 0;
        }

        //only called once in a hundred times, so roughly at 60 fps -> 100/60 = 1.67 times a second
        if (Random.value < 0.01f)
        {
            moveSpeedMultiplier = Random.Range(0.75f, 1.25f);
        }


        Move();

        
    }

    private void Move()
    {
        velocityAi = player.playerRigidbody.velocity;
        velocityAi.y = moveSpeed * moveSpeedMultiplier * direction;
        OpponentRigidBody.velocity = velocityAi;

    }



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
