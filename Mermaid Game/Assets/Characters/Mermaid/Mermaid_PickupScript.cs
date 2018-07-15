using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mermaid_PickupScript : MonoBehaviour {

    Characters_Parent characterParent;
    public bool canPickUp = false;

    private Text pickText;

    // Use this for initialization
    void Start () {
        characterParent = transform.parent.GetComponent<Characters_Parent>();

        pickText = GameObject.Find("PickText").GetComponent<Text>();
        pickText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SwitchTrigger>())
        {
            SwitchTrigger switchTrigger = collision.GetComponent<SwitchTrigger>();
            if (switchTrigger.isKidIn) {
                canPickUp = true;
                pickText.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<SwitchTrigger>())
        {
            canPickUp = false;
            pickText.enabled = false;
        }
    }
}


