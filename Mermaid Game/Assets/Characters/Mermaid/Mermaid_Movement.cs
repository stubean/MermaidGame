using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermaid_Movement : MonoBehaviour {

    /// <summary>
    /// The code for controlling the mermaids movement
    /// </summary>
    /// 

    public Mermaid_Controller mermaid_Controller;
    SpriteRenderer spriteRenderer;

    public bool isInWater = false;
    public bool isCarryingKid = false;
    public float movespeed = 5f;

    public bool isSwimming = false;
    Animator animator;

    private Rigidbody2D rigidbody2d;

    float moveHorizontal, moveVertical;
    bool isFacingRight = true;


    private void Awake()
    {
        mermaid_Controller = GetComponent<Mermaid_Controller>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

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
        if (mermaid_Controller.isFocused)//if the player is focused on the mermaid
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        else
        {
            moveHorizontal = 0f;
            moveVertical = 0f;
        }

        if(moveHorizontal < -0.01)
        {
            spriteRenderer.flipX = true;
            isFacingRight = true;
        }
        else if (moveHorizontal > 0.01)
        {
            spriteRenderer.flipX = false;
            isFacingRight = false;
        }

        if (isInWater)
        {
            rigidbody2d.velocity = new Vector2(moveHorizontal * movespeed, moveVertical * movespeed);

            if (rigidbody2d.velocity.x != 0f || rigidbody2d.velocity.y != 0f)
            {
                isSwimming = true;
                animator.SetBool("isSwimming", true);
            }
            else
            {
                isSwimming = false;
                animator.SetBool("isSwimming", false);
            }
        } else {
            isSwimming = false;
        }
    }

    /// <summary>
    /// This turns on and off the mermaid's behavior if they are carrying the kid or not
    /// </summary>
    public void SetCarryingKid(bool IsCarrying)
    {

        isCarryingKid = IsCarrying;

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
            mermaid_Controller.isInWater = true;
            mermaid_Controller.waterLevel_Controller = collision.GetComponent<WaterLevel_Controller>();
            rigidbody2d.gravityScale = 0.5f;
        } 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            isInWater = true;
            mermaid_Controller.isInWater = true;
            mermaid_Controller.waterLevel_Controller = collision.GetComponent<WaterLevel_Controller>();
            rigidbody2d.gravityScale = 0.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            isInWater = false;
            mermaid_Controller.isInWater = false;
            mermaid_Controller.waterLevel_Controller = null;
            rigidbody2d.gravityScale = 1f;
        }
    }
}
