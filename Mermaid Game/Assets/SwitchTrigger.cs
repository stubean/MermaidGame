using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour {

    public bool isKidIn = false;
    public bool isMermaidIn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Kid_Movement>()) {
            isKidIn = true;
        }

        if (collision.GetComponent<Mermaid_Movement>()) {
            isMermaidIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Kid_Movement>())
        {
            isKidIn = false;
        }

        if (collision.GetComponent<Mermaid_Movement>())
        {
            isMermaidIn = false;
        }
    }
}
