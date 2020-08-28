using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<CharacterControl>()!=null)
        {
            other.GetComponent<CharacterControl>().RemoveFromList();
        }
    }
}
