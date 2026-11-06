using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public HealthScript targetHealthScript; 
    private TextMeshProUGUI healthText;

    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();

        if (targetHealthScript == null)
        {
            targetHealthScript = FindAnyObjectByType<HealthScript>();
        }
    }

    void Update()
    {
        if (targetHealthScript != null && healthText != null)
        {
            string prefix = "Health: ";

            if (targetHealthScript.CompareTag("Player"))
            {
                prefix = "Player Health: ";
            }
            else if (targetHealthScript.CompareTag("Enemy"))
            {
                prefix = "Enemy Health: ";
            }

            healthText.text = prefix + Mathf.Ceil(targetHealthScript.currentHealth);
        }
    }
}
