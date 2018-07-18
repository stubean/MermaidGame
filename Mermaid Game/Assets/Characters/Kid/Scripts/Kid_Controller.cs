using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid_Controller : MonoBehaviour {

    /// <summary>
    /// The overal controls and statuses for the mermaid, except movement, which is done in the kid_Movement class
    /// Should only be on the "Mermaid" object
    /// </summary>

    Characters_Parent characters_Parent;
    Mermaid_Controller mermaid_Controller;//FUNCTION SHOULD BE CONVERTED TO A FUNCTION IN mermaid_controller
    Kid_Movement kid_Movement;
    SwitchTrigger currentSwitchTrigger;
    SpriteRenderer mySpriteRenderer;
    BoxCollider2D myBoxCollider2D;
    Rigidbody2D myRigidbody2D;

    //Statuses of the kid
    public bool canPickupMermaid = false;
    public bool isBeingCarried = false;
    public bool canDropoffMermaid = false;
    public bool carryingMermaid = false;
    public bool isFocused = true;//start at kid on default


    bool activateButtonHit = false;

    private void Awake()
    {
        kid_Movement = GetComponent<Kid_Movement>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        mermaid_Controller = GameObject.FindGameObjectWithTag("Mermaid").GetComponent<Mermaid_Controller>();
        characters_Parent = GameObject.Find("Characters_Parent").GetComponent<Characters_Parent>();

    }

    //Turns on and off if we are controlling the kid
    public void SetFocus(bool IsFocused)
    {
        isFocused = IsFocused;
    }

    // Update is called once per frame
    void Update()
    {


        if (isFocused && !activateButtonHit && Input.GetAxis("Activate") > 0.1)//if the user has hit the activate button("E")
        {
            activateButtonHit = true;
            if (!carryingMermaid && canPickupMermaid)//only allow to pick up the Mermaid if we can
            {
                carryingMermaid = true;
                kid_Movement.SetCarryingMermaid(true);
                mermaid_Controller.Pickup();
                characters_Parent.someoneCarried = true;
            }
            else if (carryingMermaid && canDropoffMermaid)
            {
                carryingMermaid = false;
                kid_Movement.SetCarryingMermaid(false);
                mermaid_Controller.DropOff(currentSwitchTrigger);
                characters_Parent.someoneCarried = false;
            }



        }
        else if (activateButtonHit && Mathf.Abs(Input.GetAxis("Activate")) < 0.1)//if the player was pushing down the activate key, and released it, allow the user to activate again
        {
            activateButtonHit = false;
        }

    }

    /// <summary>
    /// Change the kid's state to be being picked up. Disables box collider and makes the kid invisible
    /// </summary>
    public void Pickup()
    {
        isFocused = false;//make sure we are not focused
        isBeingCarried = true;
        mySpriteRenderer.color = new Color(1, 1, 1, 0);
        myBoxCollider2D.enabled = false;
        myRigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    /// <summary>
    /// Change the kid's state to be being dropped off. Enables box collider and makes the kid visible
    /// </summary>
    public void DropOff(SwitchTrigger switchTrigger)
    {
        isBeingCarried = false;
        transform.position = switchTrigger.kidDropOffPosition;
        mySpriteRenderer.color = new Color(1, 1, 1, 1);
        myBoxCollider2D.enabled = true;
        myRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)//if the kid is in the switchTrigger
    {
        if (collision.GetComponent<SwitchTrigger>())
        {
            currentSwitchTrigger = collision.GetComponent<SwitchTrigger>();
            if (carryingMermaid)
            {
                canDropoffMermaid = true;
            }
            else if (currentSwitchTrigger.isMermaidIn)
            {
                canPickupMermaid = true;
            }
            else
            {
                canPickupMermaid = false;
            }
        }


    }

    private void OnTriggerStay2D(Collider2D collision)//if the mermaid is in the switchTrigger
    {
        if (collision.GetComponent<SwitchTrigger>())
        {
            if (carryingMermaid)
            {
                canDropoffMermaid = true;
            }
            else if (currentSwitchTrigger.isMermaidIn)
            {
                canPickupMermaid = true;
            }
            else
            {
                canPickupMermaid = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//leaving the dropoff zone
    {
        if (collision.GetComponent<SwitchTrigger>())
        {
            canDropoffMermaid = false;
            canPickupMermaid = false;
        }
        currentSwitchTrigger = null;//remove the reference to the switch trigger the kid was using
    }

}
