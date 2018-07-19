using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermaid_Controller : MonoBehaviour {
    /// <summary>
    /// The overal controls and statuses for the mermaid, except movement, which is done in the Mermaid_Movement class
    /// Should only be on the "Mermaid" object
    /// </summary>

    Characters_Parent characters_Parent;
    Kid_Controller kid_Controller;
    Mermaid_Movement mermaid_Movement;
    SwitchTrigger currentSwitchTrigger;
    SpriteRenderer mySpriteRenderer;
    BoxCollider2D myBoxCollider2D;
    Rigidbody2D myRigidbody2D;

    //Statuses of the mermaid
    public bool canPickupKid = false;
    public bool isBeingCarried = false;
    public bool canDropoffKid = false;
    public bool carryingKid = false;
    public bool isFocused = false;



    bool activateButtonHit = false;

    private void Awake()
    {
        mermaid_Movement = GetComponent<Mermaid_Movement>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        kid_Controller = GameObject.FindGameObjectWithTag("Kid").GetComponent<Kid_Controller>();
        characters_Parent = GameObject.Find("Characters_Parent").GetComponent<Characters_Parent>();


    }

    //Turns on and off if we are controlling the mermaid
    public void SetFocus(bool IsFocused)
    {
        isFocused = IsFocused;

        /*
        if(isFocused)//if we aren't focused, we don't want the meriad to keep traveling down. ASK DESIGNERS TO MAKE SURE
            myRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        else
            myRigidbody2D.bodyType = RigidbodyType2D.Kinematic;*/
    }
	
	// Update is called once per frame
	void Update () {
		

        if(isFocused && !activateButtonHit && Input.GetAxis("Activate") > 0.1)//if the user has hit the activate button("E" or "F"). They are trying to pickup something or activate something
        {
            activateButtonHit = true;
            if (!carryingKid && canPickupKid)//If we can pickup the kid and they aren't already being carried
            {
                carryingKid = true;
                mermaid_Movement.SetCarryingKid(true);
                kid_Controller.Pickup();
                characters_Parent.someoneCarried = true;
            }
            else if(carryingKid && canDropoffKid)//if the user is tring to drop off the kid
            {
                carryingKid = false;
                mermaid_Movement.SetCarryingKid(false);
                kid_Controller.DropOff(currentSwitchTrigger);
                characters_Parent.someoneCarried = false;
            }



        }
        else if (activateButtonHit && Mathf.Abs(Input.GetAxis("Activate")) < 0.1)//if the player was pushing down the activate key, and released it, allow the user to activate again
        {
            activateButtonHit = false;
        }

    }


    /// <summary>
    /// Change the mermaids's state to be being picked up. Disables box collider and makes the kid invisible
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
    /// Change the mermaids's state to be being dropped off. Enables box collider and makes the kid visible
    /// </summary>
    public void DropOff(SwitchTrigger switchTrigger)
    {
        isBeingCarried = false;
        transform.position = switchTrigger.mermaidDropOffPosition;
        mySpriteRenderer.color = new Color(1, 1, 1, 1);
        myBoxCollider2D.enabled = true;
        myRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)//if the mermaid is in the switchTrigger
    {
        if (collision.GetComponent<SwitchTrigger>())
        {
            currentSwitchTrigger = collision.GetComponent<SwitchTrigger>();
            if(carryingKid)
            {
                canDropoffKid = true;
            }
            else if (currentSwitchTrigger.isKidIn)
            {
                canPickupKid = true;
            }
            else
            {
                canPickupKid = false;
            }
        }

       
    }

    private void OnTriggerStay2D(Collider2D collision)//if the mermaid is in the switchTrigger
    {
        if (collision.GetComponent<SwitchTrigger>())
        {
            if (carryingKid)
            {
                canDropoffKid = true;
            }
            else if (currentSwitchTrigger.isKidIn)
            {
                canPickupKid = true;
            }
            else
            {
                canPickupKid = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//leaving the dropoff zone
    {
        if (collision.GetComponent<SwitchTrigger>())
        {
            canDropoffKid = false;
            canPickupKid = false;
        }
        currentSwitchTrigger = null;//remove the reference to the switch trigger the mermaid was using
    }
}
