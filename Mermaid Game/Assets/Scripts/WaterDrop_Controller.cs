using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop_Controller : MonoBehaviour {

    WaterLevel_Controller waterLevel_Controller;//the body of water that get's affected
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            waterLevel_Controller = collision.GetComponent<WaterLevel_Controller>();
            waterLevel_Controller.AddWater(1);
            Destroy(this.gameObject);
        }
    }

   

}
