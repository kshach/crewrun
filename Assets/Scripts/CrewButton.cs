using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewButton : MonoBehaviour
{
    public GameManager gaman;
    float range;
    public float ClickRange;
    
    // Start is called before the first frame update
    void Start()
    {
        gaman = FindObjectOfType<GameManager>();
        range = gaman.clickRange;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)&&gaman.floors.start)
        {
            for (int i = 0; i < gaman.charpositions.Count; i++)
            {
                if (Input.mousePosition.x  <= gaman.charpositions[i].x + range && gaman.charpositions[i].x - range <= Input.mousePosition.x )
                {
                    int indexIs = (int)gaman.charpositions[i].y;
                    foreach (GameObject player in gaman.playersLeft)
                    {
                        if (player.GetComponent<CharacterControl>().id == (int)gaman.charpositions[i].y)
                        {
                            player.GetComponent<CharacterControl>().Jump();
                        }
                    }
                }
            }
            
        }
    }

    
}
