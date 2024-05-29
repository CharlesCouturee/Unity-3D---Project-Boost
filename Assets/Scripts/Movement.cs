using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineBooster;
    [SerializeField] ParticleSystem LeftThrusterBooster;
    [SerializeField] ParticleSystem RightThrusterBooster;

    Rigidbody rigidBody;
    AudioSource audioSource;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineBooster.Stop();
    }

    void StartThrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineBooster.isPlaying)
        {
            mainEngineBooster.Play();
        }
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void StopRotating()
    {
        RightThrusterBooster.Stop();
        LeftThrusterBooster.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!LeftThrusterBooster.isPlaying)
        {
            LeftThrusterBooster.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!RightThrusterBooster.isPlaying)
        {
            RightThrusterBooster.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidBody.freezeRotation = false; // Unfreezing rotation so the physic system takes over
    }
}
