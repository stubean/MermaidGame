using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mermaid_PickupScript : MonoBehaviour {
    /// <summary>
    /// This script tells the mermaid controller if the kid can be pickedup or not. Should only be used on the mermaid's child object "MermaidPickupRadius"
    /// </summary>


    Mermaid_Controller mermaid_Controller;

    // Use this for initialization
    void Start()
    {
        mermaid_Controller = transform.parent.GetComponent<Mermaid_Controller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Kid")
        {
            mermaid_Controller.canPickupKid = true;
        }
       /* else if (collision.tag == "Interactable")
        {
            mermaid_Controller.Interactable = true;
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Kid")
        {
            mermaid_Controller.canPickupKid = false;
        }
    }
}


