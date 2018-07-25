using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel_Controller : MonoBehaviour {

   public int maxWaterUnitCount;//the maximum amount the water has
    public int minWaterUnitCount;//the minimum amount the water will allow
   
    public float waterUnitMovementValue;//how many units of water is considered
    public float waterChangeRate;//how fast we move the water
    public int currentWaterCount;


    public void Awake()
    {
        currentWaterCount = maxWaterUnitCount;
    }

    public void RemoveWater(int amount)
    {
        if (currentWaterCount - amount < minWaterUnitCount)//if the remove amount will go past the min. set the amount to be what's left to remove
        {
            amount = currentWaterCount - minWaterUnitCount;// set the amount to be what's left to remove
            currentWaterCount = minWaterUnitCount;
        }
        else
            currentWaterCount -= amount;

        //In the future, have a interpolation of the water so it looks like it flows down
        transform.position = new Vector3(transform.position.x, transform.position.y - waterUnitMovementValue * amount);
        
    }

    public void AddWater(int amount)
    {
        if (currentWaterCount - amount < minWaterUnitCount)//if the adding amount will go past the max. set the amount to be what's left to remove
        {
            amount = maxWaterUnitCount - currentWaterCount;// set the amount to be what's left to remove
            currentWaterCount = maxWaterUnitCount;
        }
        else
            currentWaterCount += amount;

        //In the future, have a interpolation of the water so it looks like it flows down
        transform.position = new Vector3(transform.position.x, transform.position.y + waterUnitMovementValue * amount);

    }

    public void ResetWater()
    {
        AddWater(int.MaxValue);//May want to change to have an original value
    }
}
