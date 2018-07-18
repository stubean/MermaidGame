using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters_Parent : MonoBehaviour
{

    // different types of character;
    public enum CharactersType { Kid, Mermaid, Other };
    public CharactersType characterType;
    public GameObject mermaid;
    Mermaid_Controller mermaid_Controller;
    public GameObject kid;
    Kid_Controller kid_Controller;

    bool switchActivated = false;
    public bool someoneCarried = false;

    // Use this for initialization
    void Start()
    {
        kid = GameObject.Find("Kid");
        kid_Controller = kid.GetComponent<Kid_Controller>();
        mermaid = GameObject.Find("Mermaid");
        mermaid_Controller = mermaid.GetComponent<Mermaid_Controller>();

        // set character type to kid at start
        characterType = CharactersType.Kid;
    }

    // Update is called once per frame
    void Update()
    {

        // Press G  key to switch between characters and the player isn't holding down the switch key
        if (!switchActivated && Input.GetAxis("Switch") > 0.1)
        {
            switchActivated = true;
            if (characterType == CharactersType.Kid)//if we are currently the kid, changing into mermaid
            {
                characterType = CharactersType.Mermaid;
                mermaid_Controller.SetFocus(true);//If the mermaid is not focused on, we just need to disable it's controlls but still see it
                kid_Controller.SetFocus(false);
            }
            else if (characterType == CharactersType.Mermaid)//if we are currently the mermaid, changing into kid
            {
                characterType = CharactersType.Kid;
                mermaid_Controller.SetFocus(false);
                kid_Controller.SetFocus(true);
            }
        }
        else if (switchActivated && Mathf.Abs(Input.GetAxis("Switch")) < 0.1)//if the player was pushing down the switch key, and released it, allow the user to switch again
        {
            switchActivated = false;
        }
    }



    // return character type
    public CharactersType GetCharacterType()
    {
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