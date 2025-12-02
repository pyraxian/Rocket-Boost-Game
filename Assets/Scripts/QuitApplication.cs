using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        RespondToKeyPress();
    }

    private void RespondToKeyPress()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Quit application
            Debug.Log("Tried to quit application");
        }
    }
}
