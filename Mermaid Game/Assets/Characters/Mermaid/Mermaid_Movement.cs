using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermaid_Movement : MonoBehaviour {

    public bool isInWater = false;
    public float movespeed = 5f;

    private Rigidbody2D rigidbody2d;

    // Use this for initialization
    void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        print(isInWater);
        Movement();
    }

    void Movement()
    {
        // get player input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        if (isInWater)
        {
            rigidbody2d.velocity = new Vector2(moveHorizontal * movespeed, moveVertical * movespeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water") {
            isInWater = true;
            rigidbody2d.gravityScale = 0.5f;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            isInWater = false;
            rigidbody2d.gravityScale = 1f;
        }
    }
}
