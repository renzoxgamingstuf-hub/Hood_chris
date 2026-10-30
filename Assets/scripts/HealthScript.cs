using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Combat Feedback")]
    public bool isPlayer = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        string characterName = isPlayer ? "Player" : "Enemy";
        Debug.Log($"<color=red>[DAMAGE APPLIED]</color> {characterName} ({gameObject.name}) took {damageAmount} damage! Remaining HP: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        string characterName = isPlayer ? "Player" : "Enemy";
        Debug.Log($"*** {characterName} HAS BEEN KNOCKED OUT! ***");

        if (!isPlayer)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Game Over!");
        }
    }
}
