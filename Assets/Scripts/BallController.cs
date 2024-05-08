using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;

    public float minDirection = 0.5f;

    public GameObject sparksVFX;

    private Vector3 direction;
    private Rigidbody rb;

    private bool stopped = true;


    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void FixedUpdate()
    {
        if (this.stopped) return;

        this.rb.MovePosition(this.rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool hit = false;

        if (other.CompareTag("Wall"))
        {
            direction.z = -direction.z;
            hit = true;
        }

        if (other.CompareTag("Racket"))
        {
            Vector3 newDirecction = (transform.position - other.transform.position).normalized;

            newDirecction.x = Mathf.Sign(newDirecction.x) * Mathf.Max(Mathf.Abs(newDirecction.x), this.minDirection);
            newDirecction.z = Mathf.Sign(newDirecction.z) * Mathf.Max(Mathf.Abs(newDirecction.z), this.minDirection);
            direction = newDirecction;

            hit = true;
        }


        if (hit)
        {
            GameObject sparks = Instantiate(this.sparksVFX, transform.position, transform.rotation);
            Destroy(sparks, 4f);
        }
    }


    public void Stop() {
        stopped = true;
    }

    public void Go() {
        ChooseDirection();
        stopped = false;
    }

    // This functions will help us by random generating a number, in order for the ball to move in a different direction 
    // when the game starts.
    private void ChooseDirection()
    {
        float signX = Mathf.Sign(Random.Range(-1f, 1f));
        float signZ = Mathf.Sign(Random.Range(-1f, 1f));

        this.direction = new Vector3(0.5f * signX, 0, 0.5f * signZ);
    }
}
