using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] private float enableDelay = 0.5f;
    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float rotationSpeed = 100;
    [SerializeField] AudioClip thrustSFX;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;


    Rigidbody rb;
    AudioSource auSrc;

    private void OnEnable()
    {
        Invoke(nameof(EnableControls), enableDelay);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        auSrc = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(thrustForce * Time.fixedDeltaTime * Vector3.up);
            if (!auSrc.isPlaying) { auSrc.PlayOneShot(thrustSFX, 1.0F); }
            if (!mainBoosterParticles.isPlaying) { mainBoosterParticles.Play(); }
        }
        else
        {
            mainBoosterParticles.Stop();
            auSrc.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0) // Rotate clockwise
        {
            if (!rightBoosterParticles.isPlaying)
            {
                leftBoosterParticles.Stop();
                rightBoosterParticles.Play(); 
            }
            ApplyRotation(rotationSpeed);
        }
        else if (rotationInput > 0) // Rotate counter-clockwise
        {
            if (!leftBoosterParticles.isPlaying)
            {
                rightBoosterParticles.Stop();
                leftBoosterParticles.Play(); 
            }
            ApplyRotation(-rotationSpeed);
        } 
        else
        {
            rightBoosterParticles.Stop();
            leftBoosterParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Prevent rotation from physics during player rotation
        transform.Rotate(rotationThisFrame * Time.fixedDeltaTime * Vector3.forward);
        rb.freezeRotation = false;
    }

    // Allows us to use Invoke to delay the player controls activating
    private void EnableControls()
    {
        thrust.Enable();
        rotation.Enable();
    }
}
