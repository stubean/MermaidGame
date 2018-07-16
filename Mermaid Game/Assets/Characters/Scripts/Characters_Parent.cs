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
    Kid_Movement kidMovement;

    public bool hasDisembarked = false; // has kid and mermaid been disembarked
    public Vector3 dropMermaidOffset = new Vector3(4f, 0f, 0f);
    public Vector3 dropKidOffsetX = new Vector3(4f, 0, 0f);
    public Vector3 dropKidOffsetY = new Vector3(0f, 3f, 0f);

    bool switchActivated = false;

    // Use this for initialization
    void Start () {
        kid = GameObject.Find("Kid");
        kidMovement = kid.GetComponent<Kid_Movement>();

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

            if (characterType == CharactersType.Kid && kidMovement.canDropMermaid)
            {
                // if kid is dropping to the right
                if (kidMovement.dropDirection < 0f)
                {

                    mermaid.transform.position = kid.transform.position + dropMermaidOffset;
                    characterType = CharactersType.Mermaid;
                    // mermaid_Controller.ToggleMermaidFocus();//If the mermaid is not focused on, we just need to disable it's controlls but still see it
                    kidMovement.enabled = false;
                    mermaid.SetActive(true);
                    hasDisembarked = true;
                }
                // if kid is dropping to the left
                else if (kidMovement.dropDirection > 0f)
                {
                    mermaid.transform.position = kid.transform.position - dropMermaidOffset;

                    characterType = CharactersType.Mermaid;
                    // mermaid_Controller.ToggleMermaidFocus();//If the mermaid is not focused on, we just need to disable it's controlls but still see it
                    kidMovement.enabled = false;
                    mermaid.SetActive(true);
                    hasDisembarked = true;
                }
                else
                {
                    Debug.LogError("How the hell did you get here?");
                }
            }
            else if (characterType == CharactersType.Mermaid && mermaidMovement.canDropKid)
            {
                print(mermaidMovement.dropDirection);
                // if mermaid is dropping to the right
                if (kidMovement.dropDirection < 0f)
                {

                    kid.transform.position = mermaid.transform.position - dropKidOffsetX + dropKidOffsetY;
                    characterType = CharactersType.Kid;
                    // mermaid_Controller.ToggleMermaidFocus();//If the mermaid is not focused on, we just need to disable it's controlls but still see it
                    mermaidMovement.enabled = false;
                    kid.SetActive(true);
                    hasDisembarked = true;
                }
                // if mermaid is dropping to the left
                else if (kidMovement.dropDirection > 0f)
                {
                    kid.transform.position = mermaid.transform.position +dropKidOffsetX + dropKidOffsetY;

                    characterType = CharactersType.Kid;
                    // mermaid_Controller.ToggleMermaidFocus();//If the mermaid is not focused on, we just need to disable it's controlls but still see it
                    mermaidMovement.enabled = false;
                    kid.SetActive(true);
                    hasDisembarked = true;
                }
                else
                {
                    Debug.LogError("How the hell did you get here?");
                }
            }
        }
        else if (Input.GetAxis("Switch") > 0.1 && hasDisembarked && switchActivated == false)
        {
            // if character is mermaid, mermaid can pick up kid
            if (characterType == CharactersType.Mermaid)
            {
                characterType = CharactersType.Kid;
                mermaidMovement.enabled = false;
                kidMovement.enabled = true;

                mermaid.GetComponent<Rigidbody2D>().gravityScale = 0f;
            }
            // If character is the kid, kid can pick up mermaid
            else if (characterType == CharactersType.Kid)
            {
                characterType = CharactersType.Mermaid;
                mermaidMovement.enabled = true;
                kidMovement.enabled = false;
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
            if (characterType == CharactersType.Mermaid)
            {
                kidMovement.enabled = true;
                kid.SetActive(false);
                mermaidMovement.isCarryingKid = true;
                hasDisembarked = false;
            }
            else if (characterType == CharactersType.Kid) {
                mermaidMovement.enabled = true;
                mermaid.SetActive(false);
                hasDisembarked = false;
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
