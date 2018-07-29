using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve_Controller : MonoBehaviour {

    //public WaterLevel_Controller waterLevel_Controller;//the body of water that get's affected
    Kid_Controller kid_Controller;
    SpriteRenderer activateSprite;
    public Transform turnPosition;
    Vector3 startPosition;
    Animator animator;
    AudioSource audioSource;

    public int AmountOfWaterToAdd = 10;
    float animationTime = 1.17f;
    public bool isKidIn = false;
    bool hasBeenTurned = false;

    public GameObject WaterDrop;//the water dropped from the pipe
    Transform WaterDropPosition;//where the water drops will fall from

    public float waterWaitTime;

    private void Awake()
    {
        activateSprite = transform.Find("ActivateSprite").GetComponent<SpriteRenderer>();
        WaterDropPosition = transform.Find("PipeWaterDropoff");
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!hasBeenTurned && isKidIn)
        {
            if (Input.GetAxis("Activate") > 0.1)//if the user has hit the activate button("E")
            {
                hasBeenTurned = true;
                TurnValve();
            }
        }
    }

    public void TurnValve()
    {
        kid_Controller.isBusy = true;
        activateSprite.color = new Color(1, 1, 1, 0);
        audioSource.Play();
        transform.position = turnPosition.position;
        animator.SetTrigger("TurnValve");
        kid_Controller.HideKid();
        Invoke("UnhideKid", animationTime);
    }

    public void UnhideKid()
    {
        transform.position = startPosition;
        kid_Controller.UnHideKid();
        kid_Controller.isBusy = false;
        StartCoroutine(CreateWater());
    }

    IEnumerator CreateWater()
    {
        for (int i = 0; i < AmountOfWaterToAdd; i++)
        {
            Instantiate(WaterDrop, WaterDropPosition);
            yield return new WaitForSeconds(waterWaitTime);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenTurned)
        {
            if (collision.GetComponent<Kid_Movement>())
            {
                isKidIn = true;
                kid_Controller = collision.GetComponent<Kid_Controller>();
            }

            if (isKidIn)
            {
                activateSprite.color = new Color(1, 1, 1, 1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!hasBeenTurned)
        {
            if (collision.GetComponent<Kid_Movement>())
            {
                isKidIn = false;
                kid_Controller = collision.GetComponent<Kid_Controller>();
            }


            if (!isKidIn)
            {
                activateSprite.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
