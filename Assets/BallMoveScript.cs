using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoveScript : MonoBehaviour

{
    public Rigidbody2D myRigidbody;
    private GameUI gameUI;

    public Vector3 randomStartDirection;
    private Vector3 lastVelocity;
    public float moveSpeed = 8;
    // Start is called before the first frame update
    void Start()
    {
        gameUI = GameObject.FindGameObjectWithTag("Screen").GetComponent<GameUI>();
        //Append method randomDirection to listeners of onStartGame Action
        gameUI.onStartGame += randomDirection;
    }

    private void FixedUpdate()
    {
        //consistently updates lastVelocity to be used in OnCollisionEnter2D
        lastVelocity = myRigidbody.velocity;
        //Handles edge case where ball gets stuck not moving in x-direction
        if (lastVelocity.x == 0)
        {
            lastVelocity.x = 1;
        }
    }

    //used to maintain same speed reflecting off surfaces
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocity.magnitude * 1.1f;
        var direction = Vector3.Reflect(lastVelocity.normalized,
                                        collision.contacts[0].normal);

        myRigidbody.velocity = direction * Mathf.Max(speed, 2f);
    }

    //used to initialize ball at start of the game and each time player or opponent scores point
    public void randomDirection()
    {
        randomStartDirection = Random.insideUnitCircle;
        
        float getRad = Mathf.Atan(randomStartDirection.y / randomStartDirection.x);
        
        
        //handles case where ball is shot in upwards direction rather than towards player
        if (randomStartDirection.x >= 0 && randomStartDirection.y >= 0)
        {
            //1st quadrant
            if (getRad > Mathf.PI / 3)
            {
                randomDirection();
            }

        } else if (randomStartDirection.x < 0 && randomStartDirection.y >= 0)
        {
            //2nd quadrant
            if (getRad < Mathf.PI / 3)
            {
                randomDirection();
            }

        }else if (randomStartDirection.x < 0 && randomStartDirection.y < 0)
        {
            //3rd quadrant
            if (getRad > Mathf.PI / 3)
            {
                randomDirection();
            }
        }
        else
        {
            //4th quadrant
            if (getRad < Mathf.PI / 3)
            {
                randomDirection();
            }
        }

        randomStartDirection.z = 0;
        //needed to normalized randomStartDirection to ensure same speed is given to ball at the start of game and everytime character scores
        myRigidbody.velocity = randomStartDirection.normalized * moveSpeed;
        Debug.Log(myRigidbody.velocity);
    }
}
