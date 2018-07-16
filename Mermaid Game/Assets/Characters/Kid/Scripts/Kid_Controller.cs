using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid_Controller : MonoBehaviour {

    /// <summary>
    /// The overal controls and statuses for the mermaid, except movement, which is done in the kid_Movement class
    /// Should only be on the "Mermaid" object
    /// </summary>


    Kid_Movement kid_Movement;

    //Statuses of the mermaid
    public bool canPickupKid = false;
    public bool isBeingCarried = false;



    bool activateButtonHit = false;

    // Use this for initialization
    void Start()
    {
        kid_Movement = GetComponent<Kid_Movement>();

    }

    //Turns on and off if we are controlling the mermaid
    public void SetFocus(bool IsFocused)
    {
        kid_Movement.isFocused = IsFocused;
    }

    // Update is called once per frame
    void Update()
    {


        if (!activateButtonHit && Input.GetAxis("Activate") > 0.1)//if the user has hit the activate button("E")
        {
            activateButtonHit = true;
            //if (canPickupKid)//only allow to pick up the kid if we can
            {
                //kid_Movement.ToggleCarryingMermaid();
            }



        }
        else if (activateButtonHit && Mathf.Abs(Input.GetAxis("Activate")) < 0.1)//if the player was pushing down the activate key, and released it, allow the user to activate again
        {
            activateButtonHit = false;
        }

    }

}
