using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarabageRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<CharacterControl>().RemoveFromList();
            Destroy(other.gameObject);
        }
    }
}
