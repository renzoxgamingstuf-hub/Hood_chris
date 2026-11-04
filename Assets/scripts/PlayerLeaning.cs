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
    
    [Header("Hitbox / Collider Reference")]
    public Collider playerCollider;
    
    private float currentLean = 0f;
    private float targetLean = 0f;
    private float dodgeDurationTimer = 0f;
    private float dodgeCooldownTimer = 0f;
    private float instantAttackBuffer = 0f;
    
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public AttackScript attackScript;

    void Start()
{
    originalPosition = transform.localPosition;
    originalRotation = transform.localRotation;

    if (attackScript == null)
    {
        attackScript = GetComponentInParent<AttackScript>() ?? GetComponentInChildren<AttackScript>() ?? FindAnyObjectByType<AttackScript>();
    }

    if (playerCollider == null)
    {
        playerCollider = GetComponent<Collider>();
        if (playerCollider == null) playerCollider = GetComponentInChildren<Collider>();
    }
}

    void Update()
    {
        if (dodgeCooldownTimer > 0f)
        {
            dodgeCooldownTimer -= Time.deltaTime;
        }

        if (instantAttackBuffer > 0f)
        {
            instantAttackBuffer -= Time.deltaTime;
        }

        if (dodgeDurationTimer > 0f)
        {
            dodgeDurationTimer -= Time.deltaTime;
            if (dodgeDurationTimer <= 0f)
            {
                targetLean = 0f;
                dodgeCooldownTimer = dodgeCooldown;

                if (playerCollider != null) playerCollider.enabled = true;
            }
        }

        bool isPlayerAttacking = (attackScript != null && attackScript.IsAttacking()) || instantAttackBuffer > 0f;

        if (dodgeDurationTimer <= 0f && dodgeCooldownTimer <= 0f && !isPlayerAttacking)
        {
            if (Keyboard.current != null)
            {
                if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    StartDodge(-1f);
                }
                else if (Keyboard.current.dKey.wasPressedThisFrame)
                {
                    StartDodge(1f);
                }
            }
        }

        currentLean = Mathf.Lerp(currentLean, targetLean, Time.deltaTime * leanSpeed);

        Vector3 targetPosition = originalPosition + new Vector3(currentLean * leanDistance, 0f, 0f);
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0f, 0f, -currentLean * leanAngle);

        transform.localPosition = targetPosition;
        transform.localRotation = targetRotation;
    }

    public void NotifyAttackStarted()
    {
        instantAttackBuffer = 0.2f;
    }

    private void StartDodge(float direction)
    {
        if ((attackScript != null && attackScript.IsAttacking()) || instantAttackBuffer > 0f) 
            return;

        targetLean = direction;
        dodgeDurationTimer = dodgeDuration;

        if (playerCollider != null) playerCollider.enabled = false;
    }

    public bool IsLeaning()
    {
        return Mathf.Abs(currentLean) > 0.1f;
    }
}