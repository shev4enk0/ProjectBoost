using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    enum State
    {
        Live,
        Dead,
        Transfer
    }

    State state = State.Live;

    [SerializeField]float rotateStrength = 100f;
    [SerializeField]float thrustStrength = 100f;

    [SerializeField]ParticleSystem turboParticle;
    [SerializeField]ParticleSystem bangParticle;
    [SerializeField]ParticleSystem winJoyParticle;

    [SerializeField]AudioClip bangSound;
    [SerializeField]AudioClip turboSound;
    [SerializeField]AudioClip winJoySound;

    public bool isDebugStateActive;


    Rigidbody rb;
    AudioSource audio;
    Vector3 basePos;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        basePos = transform.position;
    }
	
    // Update is called once per frame
    void Update()
    {
        LiveStateRocket();

        DebugMode();
       
    }

    void LiveStateRocket()
    {
        if (state == State.Live)
        {
            Thrust();
            Rotate();
        }
    }

    void DebugMode()
    {
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                isDebugStateActive = !isDebugStateActive;
            }
            if (Input.GetKeyDown(KeyCode.L))
                LoadNextLevel();
        }
    }

    void Reset()
    {
        rb.velocity = Vector3.zero;
        transform.position = basePos;
        transform.rotation = Quaternion.identity;
        state = State.Live;
    }

    void OnCollisionEnter(Collision other)
    {
        if (state != State.Live)
            return;
        switch (other.gameObject.tag)
        {
            case "Finish":    
                {
                    WinStateRocket();
                }
                break;
            case "Friendly":
                break;
            default:
                {
                    if (isDebugStateActive)
                        return;
                    BangStateRocket();
                }
                break;
        }
    }

    void BangStateRocket()
    {
        if (state == State.Live)
        {
            state = State.Dead;
            audio.PlayOneShot(bangSound);
            bangParticle.Play();
            Invoke("Reset", 2f);
        }
       
    }

    void WinStateRocket()
    {
        state = State.Transfer;
        audio.Stop();
        audio.PlayOneShot(winJoySound);
        winJoyParticle.Play();
        Invoke("LoadNextLevel", winJoySound.length + 2f);
    }

    void LoadNextLevel()
    {
        int totalNumber = SceneManager.sceneCountInBuildSettings;
        print(totalNumber); 
        if (SceneManager.GetActiveScene().buildIndex < totalNumber - 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            turboParticle.Play();
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.deltaTime);

            if (!audio.isPlaying)
                audio.PlayOneShot(turboSound);
        }
        else
            audio.Stop();
    }

    void Rotate()
    {
        float speed = rotateStrength * Time.deltaTime;
        rb.freezeRotation = true;
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.forward * speed);
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.back * speed);
        rb.freezeRotation = false;
    }
}
