using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters_Parent : MonoBehaviour {

    // different types of character;
    public enum CharactersType { Kid, Mermaid, Other };
    public CharactersType characterType;

    public GameObject mermaid;
    Mermaid_Movement mermaidMovement;
    Mermaid_PickupScript mermaidPickupScript;
    public GameObject kid;
    Kid_Movement kid_Movement;

    public bool hasDisembarked = false; // has kid and mermaid been disembarked
    public Vector3 dropOffsetX = new Vector3(4f, 0f, 0f);

    bool switchActivated = false;

    // Use this for initialization
    void Start () {
        kid = GameObject.Find("Kid");
        kid_Movement = kid.GetComponent<Kid_Movement>();

        mermaid = GameObject.Find("Mermaid");
        mermaidMovement = mermaid.GetComponent<Mermaid_Movement>();
        mermaidPickupScript = mermaid.GetComponent<Mermaid_PickupScript>();

        // set character type to kid at start
        characterType = CharactersType.Kid;
        mermaid.SetActive(false);
    }
	


	// Update is called once per frame
	void Update () {

        // Press G  key to switch between characters and the player isn't holding down the switch key
        if (Input.GetAxis("Switch") > 0.1 && !hasDisembarked && switchActivated == false)
        {
            switchActivated = true;
            // if character is kid can he can drops the mermaid (kid is inside trigger zone)
            
            if (characterType == CharactersType.Kid && kid_Movement.canDropMermaid) {

                print(kid_Movement.dropDirection);
                // if kid is dropping to the right
                if (kid_Movement.dropDirection < 0f)
                {
                    
                    mermaid.transform.position = kid.transform.position + dropOffsetX;
                    characterType = CharactersType.Mermaid;
                    // mermaid_Controller.ToggleMermaidFocus();//If the mermaid is not focused on, we just need to disable it's controlls but still see it
                    kid_Movement.enabled = false;
                    mermaid.SetActive(true);
                    hasDisembarked = true;
                }
                // if kid is dropping to the left
                else if (kid_Movement.dropDirection > 0f)
                {
                    mermaid.transform.position = kid.transform.position - dropOffsetX;

                    characterType = CharactersType.Mermaid;
                    // mermaid_Controller.ToggleMermaidFocus();//If the mermaid is not focused on, we just need to disable it's controlls but still see it
                    kid_Movement.enabled = false;
                    mermaid.SetActive(true);
                    hasDisembarked = true;
                }
                else {
                    Debug.LogError("How the hell did you get here?");
                }
            }
        }
        else if (Input.GetAxis("Switch") > 0.1 && hasDisembarked && switchActivated == false)
        {
            
            if (characterType == CharactersType.Mermaid)
            {
                characterType = CharactersType.Kid;
                mermaidMovement.enabled = false;
                kid_Movement.enabled = true;

                mermaid.GetComponent<Rigidbody2D>().gravityScale = 0f;
            }
            else if (characterType == CharactersType.Kid)
            {
                characterType = CharactersType.Mermaid;
                mermaidMovement.enabled = true;
                kid_Movement.enabled = false;
            }
            else {
                Debug.LogError("Who the hell are you?");
            }
        }
        else if(switchActivated && Mathf.Abs(Input.GetAxis("Switch")) < 0.1)//if the player was pushing down the switch key, and released it, allow the user to switch again
        {
            switchActivated = false;
        }

        //Press F to pick up other character
        if (Input.GetKeyDown(KeyCode.F) && hasDisembarked && mermaidPickupScript.canPickUp) {
            print("reached here");
            if (characterType == CharactersType.Mermaid) {
                kid.SetActive(false);
                mermaidMovement.isCarryingKid = true;
            }
        }

    }

    // return character type
    public CharactersType GetCharacterType() {
        switch (characterType)
        {
            case CharactersType.Kid:
                return CharactersType.Kid;
            case CharactersType.Mermaid:
                return CharactersType.Mermaid;
            default:
                return CharactersType.Other;
        }
    }
}
