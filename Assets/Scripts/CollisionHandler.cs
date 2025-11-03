using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
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
                    Invoke(nameof(LoadNextLevel), 2f);
                } else
                {
                    Debug.Log("You've reached the end of the game.");
                }
                break;
            default:
                CrashSequence();
                break;
        }
    }

    private void CrashSequence()
    {
        Debug.Log("You hit a hazard and died");
        Invoke(nameof(ReloadLevel), 2f);
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
