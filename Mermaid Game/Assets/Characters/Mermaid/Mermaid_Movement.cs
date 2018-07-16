using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermaid_Movement : MonoBehaviour {

    /// <summary>
    /// The code for controlling the mermaids movement
    /// </summary>
    public bool isInWater = false;
    public bool isCarryingKid = false;
    public bool isFocused = false;
    public float movespeed = 5f;

    private Rigidbody2D rigidbody2d;

    float moveHorizontal, moveVertical;

    

    // Use this for initialization
    void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
    }

    void Movement()
    {
        // get player input
        if (isFocused)//if the player is focused on the mermaid
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        else
        {
            moveHorizontal = 0f;
            moveVertical = 0f;
        }


        if (isInWater)
        {
            rigidbody2d.velocity = new Vector2(moveHorizontal * movespeed, moveVertical * movespeed);
        }
    }

    /// <summary>
    /// This turns on and off the mermaid's behavior if they are carrying the kid or not
    /// </summary>
    public void ToggleCarryingKid()
    {

        isCarryingKid = !isCarryingKid;

        if (isCarryingKid)
        {
            rigidbody2d.constraints ^= RigidbodyConstraints2D.FreezePositionY;//these are bitwise function. But we basically add or remove the freeze position Y so the mermaid can't go up and down.
        }
        else
        {
            rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
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
