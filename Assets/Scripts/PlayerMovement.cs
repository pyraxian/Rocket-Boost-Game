using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;

    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float rotationSpeed = 100;

    Rigidbody rb;
    AudioSource auSrc;

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        auSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Thrust();
        ProcessRotation();
    }

    private void Thrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(thrustForce * Time.fixedDeltaTime * Vector3.up);
            if (!auSrc.isPlaying)
            {
                auSrc.Play();
            }
        } else
        {
            auSrc.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0) // Rotate clockwise
        {
            ApplyRotation(rotationSpeed);
        }
        else if (rotationInput > 0) // Rotate counter-clockwise
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Prevent rotation from physics during player rotation
        transform.Rotate(rotationThisFrame * Time.fixedDeltaTime * Vector3.forward);
        rb.freezeRotation = false;
    }
}
