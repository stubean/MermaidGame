using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic_Trigger : MonoBehaviour {


    BackgroundMusic_Controller backgroundMusic_Controller;

    bool hasBeenUsed = false;

    private void Awake()
    {
        backgroundMusic_Controller = GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusic_Controller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenUsed)
        {
            if (collision.tag == "Mermaid" || collision.tag == "Kid")
            {
                hasBeenUsed = true;
                backgroundMusic_Controller.UpdateAudioClip("Chimes");


            }
           
        }
    }
}
