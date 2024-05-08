using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
    public float speed;
    public KeyCode up;
    public KeyCode down;

    public bool isPlayer = true;

    public float offset = 0.2f;

    private Rigidbody rb;

    private Transform ball;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ball = GameObject.FindGameObjectsWithTag("Ball")[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isPlayer)
        {
            MoveByPlayer();
        } else
        {
            MoveByComputer();
        }
    }

    public void MoveByPlayer()
    {
        bool pressedUp = Input.GetKey(this.up);
        bool pressDown = Input.GetKey(this.down);

        if (pressedUp)
        {
            rb.velocity = Vector3.forward * speed;
        }

        if (pressDown)
        {
            rb.velocity = Vector3.back * speed;
        }

        if (!pressedUp && !pressDown)
        {
            rb.velocity = Vector3.zero;
        }
    }

    // This method will calculate where the Player 2, Computer, should move.
    public void MoveByComputer()
    {
        if (ball.position.z > transform.position.z + offset)
        {
            rb.velocity = Vector3.forward * speed;
        }
        else if (ball.position.z < transform.position.z - offset)
        {
            rb.velocity = Vector3.back * speed;
        }
        else { 
            rb.velocity = Vector3.zero;
        }
    }
}
