using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerMovement))]

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delayTimer = 2.0f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource auSrc;
    private bool isControllable = true; // Prevent the player from moving the rocket after crashing or finishing level

    void Start()
    {
        auSrc = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isControllable) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You've collided with a friendly object");
                break;
            case "Fuel":
                Debug.Log("You've collided with a fuel pickup");
                break;
            case "Finish":
                Debug.Log("You reached the end of the level");
                if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
                {
                    SuccessSequence();
                }
                else
                {
                    Debug.Log("You've reached the end of the game.");
                    auSrc.PlayOneShot(successSFX, 1.0F);
                }
                break;
            default:
                CrashSequence();
                break;
        }
    }

    private void SuccessSequence()
    {
        // todo: add particles
        isControllable = false;
        auSrc.Stop();
        auSrc.PlayOneShot(successSFX, 1.0F);
        successParticles.Play();
        GetComponent<PlayerMovement>().enabled = false;
        Invoke(nameof(LoadNextLevel), delayTimer);
    }

    private void CrashSequence()
    {
        Debug.Log("You hit a hazard and died");
        isControllable = false;
        auSrc.Stop();
        auSrc.PlayOneShot(crashSFX, 1.0F);
        crashParticles.Play();
        GetComponent<PlayerMovement>().enabled = false;
        Invoke(nameof(ReloadLevel), delayTimer);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
