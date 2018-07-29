using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidFirstTrigger : MonoBehaviour {

    Characters_Parent characters_Parent;
    bool hasBeenUsed = false;

    private void Awake()
    {
        characters_Parent = GameObject.Find("Characters_Parent").GetComponent<Characters_Parent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenUsed && collision.tag == "Mermaid")
        {
            hasBeenUsed = true;
            characters_Parent.characterType = Characters_Parent.CharactersType.Mermaid;
            characters_Parent.someoneCarried = false;
        }
    }
}
