using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]float rotateStrength = 100f;
    [SerializeField]float rthrustStrength = 100f;
    Rigidbody rb;
    AudioSource rocketSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
    }
	
    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void Thrust()
    {
//        float speed = rotateStrength * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * rthrustStrength);
            if (!rocketSound.isPlaying)
                rocketSound.Play();
        }
        else
            rocketSound.Stop();
    }

    void Rotate()
    {
        float speed = rotateStrength * Time.deltaTime;
        rb.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.forward * speed);
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.back * speed);
        rb.freezeRotation = false;
    }
}
