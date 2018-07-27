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
    Animator animator;
    SpriteRenderer spriteRenderer;

    public float walkspeed = 5f;
    public float jumpspeed = 1f;
    public bool isInAir = true;
    public bool isCarryingMermaid = false;

    public bool isWalking = false;

    private Rigidbody2D rigidbody2d;

    float moveHorizontal;
    float moveVertical;
    bool isFacingRight = true;

    public Vector3 safePosition;//the location where the kid was last safe
    float drowningTime = 0.25f;
    float savePositionTime = 9f;
    bool savingPosition = false;
    bool onBubble = false;

    private void Awake()
    {
        kid_Controller = GetComponent<Kid_Controller>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        SaveKidsPosition();
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

        if (moveHorizontal < -0.01)
        {
            spriteRenderer.flipX = true;
            isFacingRight = true;
        }
        else if (moveHorizontal > 0.01)
        {
            spriteRenderer.flipX = false;
            isFacingRight = false;
        }


        // if (isInAir == false)
        {
            rigidbody2d.velocity = new Vector2(moveHorizontal * walkspeed, rigidbody2d.velocity.y);
        }

        if (!isInAir && !savingPosition)
        {
            savingPosition = true;
            Invoke("SaveKidsPosition", savePositionTime);
        }

        if (!isInAir)
        {

            if (rigidbody2d.velocity.x != 0f)
            {
                isWalking = true;
                animator.SetBool("isWalking", true);
            }
            else
            {
                isWalking = false;
                animator.SetBool("isWalking", false);
            }
        }
        else {
            isWalking = false;
            animator.SetBool("isWalking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isInAir = false;
            if (collision.gameObject.name == "Bubble(Clone)")
                onBubble = true;
            else
                onBubble = false;
        }

      
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isInAir = false;
            if (collision.gameObject.name == "Bubble(Clone)")
                onBubble = true;
            else
                onBubble = false;
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isInAir = true;
            onBubble = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")//the kid is drowning
        {
            //Invoke("ResetKidFromDrowning", 0f);
            transform.position = safePosition;

        }
    }

    public void ResetKidFromDrowning()
    {
       
    }

    public void SaveKidsPosition()
    {
        if (!onBubble)
        {
            safePosition = transform.position;
            savingPosition = false;
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

