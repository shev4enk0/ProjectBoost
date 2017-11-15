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
        Transform
    }

    State state = State.Live;

    [SerializeField] AudioClip[] clip;

    [SerializeField]float rotateStrength = 100f;
    [SerializeField]float thrustStrength = 100f;

    [SerializeField] ParticleSystem turboParticle;
    [SerializeField] ParticleSystem boomParticle;
    [SerializeField] ParticleSystem winBoomParticle;


    Rigidbody rb;
    AudioSource sound;
    Vector3 basePos;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        basePos = transform.position;
    }
	
    // Update is called once per frame
    void Update()
    {
        LiveModeRocket();
        if (state == State.Dead)
            Invoke("SetActiveFalse", 0.2f);
        if (Input.GetKeyDown(KeyCode.Escape))
            Reset();
    }

    void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

    void LiveModeRocket()
    {
        if (state == State.Live)
        {
            gameObject.SetActive(true);
            Thrust();
            Rotate();
        }
    }

    void Reset()
    {
        gameObject.SetActive(true);
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
                HappyLanding();
                break;
            case "Friendly":
                state = State.Live;
                break;
            default:
                BumpObstacle();
                break;
        }
    }

    void HappyLanding()
    {
        state = State.Transform;
        sound.Stop();
        winBoomParticle.Play();
        sound.PlayOneShot(clip[2]);
        Invoke("LoadNextLevel", clip[2].length + 1f);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void BumpObstacle()
    {
        sound.Stop();
        boomParticle.Play();
        sound.PlayOneShot(clip[1]);
        Invoke("Reset", 2f);
        state = State.Dead;
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.deltaTime);
            turboParticle.Play();

            if (!sound.isPlaying)
                sound.PlayOneShot(clip[0]);
        }
        else
        {
            sound.Stop();
            turboParticle.Stop();
        }
    }

    void Rotate()
    {
        float speed = rotateStrength;
        rb.freezeRotation = true;
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.forward * speed);
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.back * speed);
        rb.freezeRotation = false;
    }
}
