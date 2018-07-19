using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour {

    Mermaid_Controller mermaid_Controller;
    Kid_Controller kid_Controller;

    public Vector3 kidDropOffPosition, mermaidDropOffPosition;

    public bool isKidIn = false;
    bool kidCarryingMermaid = false;
    public bool isMermaidIn = false;
    bool mermaidCarryingKid = false;
    SpriteRenderer activateSprite;

    private void Awake()
    {
        activateSprite = transform.Find("ActivateSprite").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        mermaidDropOffPosition = transform.Find("MermaidDropOff").transform.position;
        kidDropOffPosition = transform.Find("KidDropOff").transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Kid_Movement>()) {
            isKidIn = true;
            kid_Controller = collision.GetComponent<Kid_Controller>();
            kidCarryingMermaid = kid_Controller.carryingMermaid;
        }

        if (collision.GetComponent<Mermaid_Movement>()) {
            isMermaidIn = true;
            mermaid_Controller = collision.GetComponent<Mermaid_Controller>();
            mermaidCarryingKid = mermaid_Controller.carryingKid;
        }

        if(isKidIn && isMermaidIn || isKidIn && kidCarryingMermaid || isMermaidIn && mermaidCarryingKid)
        {
            activateSprite.color = new Color(1, 1, 1, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Kid_Movement>())
        {
            isKidIn = false;
            kid_Controller = collision.GetComponent<Kid_Controller>();
            kidCarryingMermaid = kid_Controller.carryingMermaid;
        }

        if (collision.GetComponent<Mermaid_Movement>())
        {
            isMermaidIn = false;
            mermaid_Controller = collision.GetComponent<Mermaid_Controller>();
            mermaidCarryingKid = mermaid_Controller.carryingKid;
        }
        if (!isKidIn || !isMermaidIn)
        {
            activateSprite.color = new Color(1, 1, 1, 0);
        }
    }
}
