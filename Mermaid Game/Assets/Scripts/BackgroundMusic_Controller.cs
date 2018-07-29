using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic_Controller : MonoBehaviour {

    AudioSource audioSource;
    public AudioClip ambient, chimes;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }



    public void UpdateAudioClip(string AudioClipName)
    {
        switch (AudioClipName)
        {
            case "Ambient":
                audioSource.clip = ambient;
                audioSource.Play();
                return;
            case "Chimes":
                audioSource.clip = chimes;
                audioSource.Play();
                Invoke("UpdateAudioClip", 82);
                return;
        }
    }

    void ResetBackgroundMusic()
    {
        UpdateAudioClip("Ambient");
    }
}
