using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLeaning : MonoBehaviour
{
    public float leanDistance = 0.5f;
    public float leanSpeed = 5f;
    public float leanAngle = 15f;
    private float currentLean = 0f;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        float leanInput = 0f;

        if (Keyboard.current.aKey.isPressed)
        {
            leanInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            leanInput = 1f;
        }

        currentLean = Mathf.Lerp(currentLean, leanInput, Time.deltaTime * leanSpeed);

        Vector3 targetPosition = originalPosition + new Vector3(currentLean * leanDistance, 0f, 0f);
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0f, 0f, -currentLean * leanAngle);

        transform.localPosition = targetPosition;
        transform.localRotation = targetRotation;
    }
}