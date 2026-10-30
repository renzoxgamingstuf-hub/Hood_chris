using UnityEngine;

public class EnemyLeaning : MonoBehaviour
{
    public float leanDistance = 0.5f;
    public float leanSpeed = 5f;
    public float leanAngle = 15f;
    
    private float currentLean = 0f;
    private float targetLeanInput = 0f;
    
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        currentLean = Mathf.Lerp(currentLean, targetLeanInput, Time.deltaTime * leanSpeed);

        Vector3 targetPosition = originalPosition + new Vector3(currentLean * leanDistance, 0f, 0f);
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0f, 0f, -currentLean * leanAngle);

        transform.localPosition = targetPosition;
        transform.localRotation = targetRotation;
    }

    public void SetLeanInput(float input)
    {
        targetLeanInput = input;
    }
}
