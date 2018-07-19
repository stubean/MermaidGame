using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    private float upWardSpeed = 1f;

	// Use this for initialization
	void Start () {
        this.transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.up * upWardSpeed * Time.deltaTime);
	}

    private void DestroyItSelf() {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kid") {
            upWardSpeed = 0f;
            Invoke("DestroyItSelf", 4);
        }
    }
}
