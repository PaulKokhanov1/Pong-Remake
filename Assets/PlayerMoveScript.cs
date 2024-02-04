using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{

    public Rigidbody2D playerRigidbody;
    public float speed = 14; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.y < 4.45)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (-4.45 < transform.position.y)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
        }
    }
}
