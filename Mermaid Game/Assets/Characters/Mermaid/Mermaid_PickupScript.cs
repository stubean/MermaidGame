using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermaid_PickupScript : MonoBehaviour {

    Mermaid_Controller mermaid_Controller;

	// Use this for initialization
	void Start () {

        mermaid_Controller = transform.parent.GetComponent<Mermaid_Controller>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Kid")
        {
            mermaid_Controller.canPickupKid = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Kid")
        {
            mermaid_Controller.canPickupKid = false;
        }
    }
}


