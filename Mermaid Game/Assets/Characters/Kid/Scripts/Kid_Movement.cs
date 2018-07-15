using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kid_Movement : MonoBehaviour {

    public float walkspeed = 5f;
    public float jumpspeed = 1f;
    public bool isInAir = true;

    private Rigidbody2D rigidbody2d;
    private Text switchText;

    public bool canDropMermaid = false;
    public float dropDirection = 0f; // if negative then drop mermaid to right, if positive then drop mermaid to left

    // Use this for initialization
    void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();

        switchText = GameObject.Find("SwitchText").GetComponent<Text>();
        switchText.enabled = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SwitchTrigger") {
            switchText.enabled = true;
            Vector2 directionEntered = this.transform.position - collision.transform.position;
            canDropMermaid = true;
            // if negative then entered from left, if position then entered from right
            dropDirection = directionEntered.x;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SwitchTrigger")
        {
            switchText.enabled = false;
            canDropMermaid = false;
            dropDirection = 0f;
        }
    }
}

