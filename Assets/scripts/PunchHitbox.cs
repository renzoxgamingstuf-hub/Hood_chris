using UnityEngine;

public class PunchHitbox : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damage = 10f;
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
                targetHealth.TakeDamage(damage);
                cooldownTimer = hitCooldown;
            }
        }
    }
}
