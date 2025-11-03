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
                    LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
                } else
                {
                    Debug.Log("You've reached the end of the game.");
                }
                break;
            default:
                Debug.Log("You hit a hazard and died");
                LoadLevel(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }

    void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);   
    }
}
