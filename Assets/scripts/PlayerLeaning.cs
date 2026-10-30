using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLeaning : MonoBehaviour
{
    public float leanDistance = 0.5f;
    public float leanSpeed = 10f; 
    public float leanAngle = 15f;

    [Header("Dodge Timing & Cooldown")]
    public float dodgeDuration = 0.8f; 
    public float dodgeCooldown = 0.4f;
    
    private float currentLean = 0f;
    private float targetLean = 0f;
    private float dodgeDurationTimer = 0f;
    private float dodgeCooldownTimer = 0f;
    
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public AttackScript attackScript;

    void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        if (attackScript == null) attackScript = GetComponentInParent<AttackScript>();
    }

    void Update()
    {
        if (dodgeCooldownTimer > 0f)
        {
            dodgeCooldownTimer -= Time.deltaTime;
        }

        if (dodgeDurationTimer > 0f)
        {
            dodgeDurationTimer -= Time.deltaTime;
            if (dodgeDurationTimer <= 0f)
            {
                targetLean = 0f;
                dodgeCooldownTimer = dodgeCooldown;
            }
        }

        if (dodgeDurationTimer <= 0f && dodgeCooldownTimer <= 0f && (attackScript == null || !attackScript.IsAttacking()))
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                targetLean = -1f;
                dodgeDurationTimer = dodgeDuration;
            }
            else if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                targetLean = 1f;
                dodgeDurationTimer = dodgeDuration;
            }
        }

        currentLean = Mathf.Lerp(currentLean, targetLean, Time.deltaTime * leanSpeed);

        Vector3 targetPosition = originalPosition + new Vector3(currentLean * leanDistance, 0f, 0f);
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0f, 0f, -currentLean * leanAngle);

        transform.localPosition = targetPosition;
        transform.localRotation = targetRotation;
    }

    public bool IsLeaning()
    {
        return Mathf.Abs(currentLean) > 0.1f;
    }
}