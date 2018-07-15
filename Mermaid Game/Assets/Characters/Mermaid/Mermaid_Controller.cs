﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermaid_Controller : MonoBehaviour {
    /// <summary>
    /// The overal controls and statuses for the mermaid, except movement, which is done in the Mermaid_Movement class
    /// </summary>


    Mermaid_Movement mermaid_Movement;

    //Statuses of the mermaid
    public bool canPickupKid = false;



    bool activateButtonHit = false;

	// Use this for initialization
	void Start () {
        mermaid_Movement = GetComponent<Mermaid_Movement>();

    }

    //Turns on and off if we are controlling the mermaid
    public void ToggleMermaidFocus()
    {
        mermaid_Movement.isControlled = !mermaid_Movement.isControlled;
    }
	
	// Update is called once per frame
	void Update () {
		

        if(!activateButtonHit && Input.GetAxis("Activate") > 0.1)//if the user has hit the activate button("E")
        {
            activateButtonHit = true;
            //if (canPickupKid)//only allow to pick up the kid if we can
            {
                mermaid_Movement.ToggleCarryingKid();
            }



        }
        else if (activateButtonHit && Mathf.Abs(Input.GetAxis("Activate")) < 0.1)//if the player was pushing down the activate key, and released it, allow the user to activate again
        {
            activateButtonHit = false;
        }

    }
}
