using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    // private void OnCollisionEnter(Collision collision)
    // {
    //       if (collision.gameObject.CompareTag("PlayerHand"))
    //     {
    //         health -= 10f;
    //         if (health <= 0f)
    //         {
    //             Die();
    //         }
    //     }
    // }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            health -= 10f;
            if (health <= 0f)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Debug.Log("Enemy is dead!");
        Destroy(gameObject);
    }
}