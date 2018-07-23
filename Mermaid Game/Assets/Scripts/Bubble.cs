﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    private float upWardSpeed = 0.25f;
    private float upWardSpeedWithChild = 0.25f;
    float currentSpeed = 1f;
    float bubbleLifeTime = 4f;


    private void Awake()
    {
        currentSpeed = upWardSpeed;
    }

    void Start () {
        this.transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
	}

    private void DestroyItSelf() {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kid") {
            currentSpeed = upWardSpeedWithChild;
            Invoke("DestroyItSelf", bubbleLifeTime);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kid")
        {
            currentSpeed = upWardSpeed;
        }
    }
}
