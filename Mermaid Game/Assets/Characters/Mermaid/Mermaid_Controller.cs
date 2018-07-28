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
    public WaterLevel_Controller waterLevel_Controller;//gets set from the mermaid_movement
    SwitchTrigger currentSwitchTrigger;
    SpriteRenderer mySpriteRenderer;
    BoxCollider2D myBoxCollider2D;
    Rigidbody2D myRigidbody2D;
    public GameObject bubble;
    public int maxBubbleCount = 5;
    public int currentBubbleCount = 0;
   public List<GameObject> bubbleList;
    GameObject tempBubble;
    Bubble tempBubbleScript;

    public GameObject bubbleSpawner;

    //Statuses of the mermaid
    public bool canPickupKid = false;
    public bool isBeingCarried = false;
    public bool canDropoffKid = false;
    public bool carryingKid = false;
    public bool isFocused = false;
    public bool isInWater = false;



    bool activateButtonHit = false;
    bool isSpawningBubble = false;
    bool isResetingBubbles = false;

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
        bubbleSpawner = GameObject.Find("BubbleSpawner");

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


        if (isFocused && !activateButtonHit && Input.GetAxis("Activate") > 0.1)//if the user has hit the activate button("E"). They are trying to pickup something or activate something
        {
            activateButtonHit = true;
            if (!carryingKid && canPickupKid)//If we can pickup the kid and they aren't already being carried
            {
                carryingKid = true;
                mermaid_Movement.SetCarryingKid(true);
                kid_Controller.Pickup();
                characters_Parent.someoneCarried = true;
            }
            else if (carryingKid && canDropoffKid)//if the user is tring to drop off the kid
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



        if (isFocused && !carryingKid && isInWater && !isSpawningBubble && waterLevel_Controller != null && Mathf.Abs(Input.GetAxis("Power1")) > 0.1)
        {
            if (currentBubbleCount < maxBubbleCount)
            {
                currentBubbleCount++;
                isSpawningBubble = true;
                tempBubble = Instantiate(bubble, bubbleSpawner.transform);
                tempBubbleScript = tempBubble.GetComponent<Bubble>();
                tempBubbleScript.waterLevel_Controller = waterLevel_Controller;
                bubbleList.Add(tempBubble);
                waterLevel_Controller.RemoveWater(1);
            }
        }
        else if(isSpawningBubble && Mathf.Abs(Input.GetAxis("Power1")) > 0.1)
        {
            isSpawningBubble = false;
        }

        if (isFocused && !isResetingBubbles && currentBubbleCount>0 && Input.GetAxis("Power2") > 0.1)//if the user has hit the activate button("C"). They are trying to remove all the bubbles
        {
            isResetingBubbles = true;
            ResetBubbles();
        }
        else if (isResetingBubbles && Mathf.Abs(Input.GetAxis("Power2")) < 0.1)//if the player was pushing down the reset bubbles key, and released it, allow the user to reset bubbles again
        {
            isResetingBubbles = false;
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

    /// <summary>
    /// Delete all the bubbles and put the water back in it's source.(Might change to be current water source if we want the mermaid to move water too?)
    /// </summary>
    public void ResetBubbles()
    {
        Bubble currentBubble;
        WaterLevel_Controller currentWaterLevel_Controller;
        for(int i = 0; i< bubbleList.Count;i++)
        {
            currentBubble = bubbleList[i].GetComponent<Bubble>();
            currentWaterLevel_Controller = currentBubble.waterLevel_Controller;
            currentBubble.DestroyBubble();//will add water back into water body
        }
        bubbleList.Clear();
        currentBubbleCount = 0;
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
            currentSwitchTrigger = collision.GetComponent<SwitchTrigger>();
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
