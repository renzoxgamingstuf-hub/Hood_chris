using UnityEngine;

public class PunchHitbox : MonoBehaviour
{
    [Header("Hitbox Ownership")]
    public bool isPlayerHitbox = true;

    [Header("Damage Settings")]
    public float playerDamage = 10f;
    public float enemyDamage = 5f;
    public string targetTag = "Enemy";

    [Header("Hit Cooldown")]
    public float hitCooldown = 0.5f;
    private float cooldownTimer = 0f;

    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void ResetHit()
    {
        cooldownTimer = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cooldownTimer > 0f) return;

        if (other.CompareTag(targetTag))
        {
            HealthScript targetHealth = other.GetComponent<HealthScript>();
            if (targetHealth == null) targetHealth = other.GetComponentInParent<HealthScript>();
            if (targetHealth == null) targetHealth = other.transform.root.GetComponent<HealthScript>();

            if (targetHealth != null)
            {
                float damageToDeal = isPlayerHitbox ? playerDamage : enemyDamage;
                targetHealth.TakeDamage(damageToDeal);
                cooldownTimer = hitCooldown;
            }
        }
    }
}
