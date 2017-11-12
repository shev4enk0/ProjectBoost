using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
	
    // Update is called once per frame
    void Update()
    {
        ProsesInput();
    }

    void ProsesInput()
    {   

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up);

        }
        if (Input.GetKey(KeyCode.A))
            print("A Pressed");
        else if (Input.GetKey(KeyCode.D))
            print("D Pressed");
        
    }
}
