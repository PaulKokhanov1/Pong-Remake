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
        gameUI.onStartGame += randomDirection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        lastVelocity = myRigidbody.velocity;
        if (lastVelocity.x == 0)
        {
            lastVelocity.y = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocity.magnitude * 1.1f;
        var direction = Vector3.Reflect(lastVelocity.normalized,
                                        collision.contacts[0].normal);

        myRigidbody.velocity = direction * Mathf.Max(speed, 2f);
    }
    public void randomDirection()
    {
        randomStartDirection = Random.insideUnitCircle;
        
        float getRad = Mathf.Atan(randomStartDirection.y / randomStartDirection.x);
        
        
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
        myRigidbody.velocity = randomStartDirection.normalized * moveSpeed;
        Debug.Log(myRigidbody.velocity);
    }
}
