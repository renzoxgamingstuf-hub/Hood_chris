using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttackScript : MonoBehaviour
{
    private Animator animator;
    public GameObject EnemyLeftHand;
    public GameObject EnemyRightHand;

    public int punchYesNo = 0;
    public int punchCooldown = 0;
    public int punchDecisionCooldown = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        decidePunch();
        if (punchYesNo == 2 && punchCooldown <= 0)
        {
            animator.SetTrigger("CPunchL");
            EnemyLeftHand.SetActive(true);
            EnemyRightHand.SetActive(false);
            punchYesNo = 0;
            punchCooldown = 100;
        }
        if (punchYesNo == 1 && punchCooldown <= 0)
        {
            animator.SetTrigger("CPunchR");
            EnemyRightHand.SetActive(true);
            EnemyLeftHand.SetActive(false);
            punchYesNo = 0;
            punchCooldown = 100;
        }
        if (punchCooldown > 0)
        {
            punchCooldown--;
        }
    }
    void decidePunch()
    {
        if (punchDecisionCooldown <= 0)
        {
            punchYesNo = Random.Range(1, 3);
            punchDecisionCooldown = 100;
        }
        if (punchDecisionCooldown > 0)
        {
            punchDecisionCooldown--;
        }
    }
}
