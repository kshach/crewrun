using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLocator : MonoBehaviour
{
    [Range(0, 5)]
    public int Height;

    private void OnValidate()
    {
        int index = transform.GetSiblingIndex();
        transform.position = new Vector3(0, Height * 6, index * 10);
    }
}
