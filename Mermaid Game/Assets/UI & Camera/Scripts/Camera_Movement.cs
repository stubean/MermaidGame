﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour {

    /// <summary>
    /// The camera used for both the kid and the mermaid
    /// </summary>


    private Characters_Parent characters_parent;

    // Use this for initialization
    void Start()
    {
        GameObject parent = GameObject.Find("Characters_Parent");
        characters_parent = parent.GetComponent<Characters_Parent>();
    }

    // Update is called once per frame
    void Update()
    {
        // fix camera to character
        if (characters_parent.characterType == Characters_Parent.CharactersType.Kid)
        {
            this.transform.position = characters_parent.kid.transform.position;
        }
        else if (characters_parent.characterType == Characters_Parent.CharactersType.Mermaid)
        {
            this.transform.position = characters_parent.mermaid.transform.position;
        }
    }

}