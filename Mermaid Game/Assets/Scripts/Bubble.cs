using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

   public WaterLevel_Controller waterLevel_Controller;//the water source the bubble came from

    float maxHeightAmount = 10;//how high the bubble can float
    private float upWardSpeed = 0.25f;
    private float upWardSpeedWithChild = 0.25f;
    float currentSpeed = 1f;
    float bubbleLifeTime = 4f;
    float maxHeight;


    private void Awake()
    {
        currentSpeed = upWardSpeed;
        maxHeight = transform.position.y + maxHeightAmount;
    }

    void Start () {
        this.transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y <= maxHeight)//the bubble can only go up so high before stopping
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
