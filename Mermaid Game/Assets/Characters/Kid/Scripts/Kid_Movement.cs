using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kid_Movement : MonoBehaviour {

    /// <summary>
    /// The code for controlling the kids movement
    /// </summary>
    /// 

    Kid_Controller kid_Controller;

    public float walkspeed = 5f;
    public float jumpspeed = 1f;
    public bool isInAir = true;
    public bool isCarryingMermaid = false;

    private Rigidbody2D rigidbody2d;

    float moveHorizontal;
    float moveVertical;


    private void Awake()
    {
        kid_Controller = GetComponent<Kid_Controller>();
    }

    // Use this for initialization
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        Movement();
    }

    // Get Axis input, then apply force to the kid
    void Movement()
    {

        // If player press space, jump up
        if (kid_Controller.isFocused && Input.GetKeyDown(KeyCode.Space) && isInAir == false)
        {
            rigidbody2d.AddForce(new Vector2(0, jumpspeed), ForceMode2D.Impulse);
            isInAir = true;
        }

        // get player input
        if (kid_Controller.isFocused)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        else
        {
            moveHorizontal = 0f;
            moveVertical = 0f;
        }


        if (isInAir == false)
        {
            rigidbody2d.velocity = new Vector2(moveHorizontal * walkspeed, rigidbody2d.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isInAir = false;
        }
    }


    /// <summary>
    /// This turns on and off the mermaid's behavior if they are carrying the kid or not
    /// </summary>
    public void SetCarryingMermaid(bool IsCarrying)
    {

        isCarryingMermaid = IsCarrying;

    }
}

