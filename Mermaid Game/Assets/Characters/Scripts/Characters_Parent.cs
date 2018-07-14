using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters_Parent : MonoBehaviour {

    // different types of character;
    public enum CharactersType { Kid, Mermaid, Other };
    public CharactersType characterType;
    public GameObject mermaid;
    public GameObject kid;

    // Use this for initialization
    void Start () {
        kid = GameObject.Find("Kid");
        mermaid = GameObject.Find("Mermaid");

        // set character type to kid at start
        characterType = CharactersType.Kid;
        mermaid.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        // Press G  key to switch between characters
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (characterType == CharactersType.Kid)
            {
                characterType = CharactersType.Mermaid;
                mermaid.transform.position = kid.transform.position;
                kid.SetActive(false);
                mermaid.SetActive(true);
            }
            else if (characterType == CharactersType.Mermaid) {
                characterType = CharactersType.Kid;
                kid.transform.position = mermaid.transform.position;
                mermaid.SetActive(false);
                kid.SetActive(true);
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
