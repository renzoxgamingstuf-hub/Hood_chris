using UnityEngine;
using UnityEngine.InputSystem; // Added for New Input System support

public class AdminScript : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            GameData.ResetProgress();
            Debug.Log("PlayerPrefs cleared! Restart the scene to see base state.");
        }
    }
}
