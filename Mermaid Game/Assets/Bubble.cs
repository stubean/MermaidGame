using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.up * Time.deltaTime);
	}
}
