using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Averager : MonoBehaviour
{
    public GameManager gaman;
    // Start is called before the first frame update
    void Start()
    {
        gaman = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gaman.playersLeft.Count>0)
        {
            float Avg = 0;
            foreach (GameObject CharObject in gaman.playersLeft)
            {
                Avg += CharObject.transform.position.y;
            }
            transform.position = new Vector3(0, Avg / gaman.playersLeft.Count, 0);
        }
    }
}
