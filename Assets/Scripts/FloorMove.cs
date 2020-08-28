using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    public float floorspeed;
    public bool start;

    void FixedUpdate()
    {
      if(start)
        {
            transform.Translate(0, 0, -floorspeed * Time.deltaTime);
        }
    }
}
