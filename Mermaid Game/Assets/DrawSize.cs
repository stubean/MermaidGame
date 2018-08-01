using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSize : MonoBehaviour {

    WaterLine waterLine;

    void OnDrawGizmos()
    {
        waterLine = gameObject.GetComponent<WaterLine>();
        float waterHeight = waterLine.Height;
        float waterWidth = waterLine.Width;

        Vector2 drawSize = new Vector2(waterWidth, waterHeight);
        Vector2 drawPosition = new Vector2(transform.position.x, transform.position.y - waterHeight/2);

        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(drawPosition, drawSize);
    }
}
