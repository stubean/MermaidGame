using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid_Movement : MonoBehaviour {

    public float walkspeed = 5f;
    public float jumpspeed = 1f;
    public bool isInAir = true;

    private Rigidbody2D rigidbody2d;


    // Use this for initialization
    void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
	}

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate () {
        Movement();
	}

    // Get Axis input, then apply force to the kid
    void Movement() {

        // If player press space, jump up
        if (Input.GetKeyDown(KeyCode.Space) && isInAir == false)
        {
            rigidbody2d.AddForce(new Vector2(0, jumpspeed), ForceMode2D.Impulse);
            isInAir = true;
        }

        // get player input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        if (isInAir == false)
        {
            rigidbody2d.velocity = new Vector2(moveHorizontal * walkspeed, rigidbody2d.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") {
            isInAir = false;
        }
    }
}

