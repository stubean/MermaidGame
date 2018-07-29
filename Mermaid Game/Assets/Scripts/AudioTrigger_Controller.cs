using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger_Controller : MonoBehaviour {

    public Characters_Parent.CharactersType characterType;
    public GameHelp_Controller.HelpType helpType;
    GameHelp_Controller gameHelp_Controller;
    AudioSource audioSource;

    bool hasBeenUsed = false;
    public bool showTutorial = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameHelp_Controller = GameObject.Find("GameHelp").GetComponent<GameHelp_Controller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenUsed)
        {
            if (characterType == Characters_Parent.CharactersType.Mermaid && collision.tag == "Mermaid")
            {
                hasBeenUsed = true;
                audioSource.Play();
                if(showTutorial)
                    gameHelp_Controller.ShowHelp(helpType);
            }
            else if (characterType == Characters_Parent.CharactersType.Kid && collision.tag == "Kid")
            {
                hasBeenUsed = true;
                audioSource.Play();
                if (showTutorial)
                    gameHelp_Controller.ShowHelp(helpType);
            }
        }
    }


}
