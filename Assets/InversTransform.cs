using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InversTransform : MonoBehaviour
{

   
    private Vector3 relative;

    void Start()
    {
        relative = transform.position;
        Debug.Log(relative);
        relative = transform.InverseTransformDirection(0, 0, 1);
        Debug.Log(relative);
    }
}

