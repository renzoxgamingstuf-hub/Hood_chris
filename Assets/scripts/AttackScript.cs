using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{
    private Animator animator;
    public GameObject LeftHand;
    public GameObject RightHand;
    public int cooldown;

    [Header("AI Reference")]
    public EnemyAttackScript enemyAI;

    [Header("Leaning Reference")]
    public PlayerLeaning playerLeaning;

    private PunchHitbox leftHandHitbox;
    private PunchHitbox rightHandHitbox;

    private const float PUNCH_ANIMATION_LENGTH = 1.967f;
    private float attackLockoutTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        if (enemyAI == null)
        {
            enemyAI = FindAnyObjectByType<EnemyAttackScript>();
        }
        if (playerLeaning == null)
        {
            playerLeaning = GetComponentInChildren<PlayerLeaning>();
        }

        if (LeftHand != null) leftHandHitbox = LeftHand.GetComponent<PunchHitbox>();
        if (RightHand != null) rightHandHitbox = RightHand.GetComponent<PunchHitbox>();
    }

    void Update()
    {
        if (attackLockoutTimer > 0f)
        {
            attackLockoutTimer -= Time.deltaTime;
        }

        bool isLeaning = playerLeaning != null && playerLeaning.IsLeaning();
        bool isAttacking = attackLockoutTimer > 0f;

        if (!isLeaning && !isAttacking)
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && cooldown <= 0)
            {
                animator.SetTrigger("CPunchL");
                LeftHand.SetActive(true);
                RightHand.SetActive(false);
                
                if (leftHandHitbox != null) leftHandHitbox.ResetHit();

                cooldown = 100;
                attackLockoutTimer = PUNCH_ANIMATION_LENGTH;

                if (enemyAI != null)
                {
                    enemyAI.OnPlayerAttack("Left");
                }
            }
            else if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame && cooldown <= 0)
            {
                animator.SetTrigger("CPunchR");
                RightHand.SetActive(true);
                LeftHand.SetActive(false);
                
                if (rightHandHitbox != null) rightHandHitbox.ResetHit();

                cooldown = 100;
                attackLockoutTimer = PUNCH_ANIMATION_LENGTH;

                if (enemyAI != null)
                {
                    enemyAI.OnPlayerAttack("Right");
                }
            }
        }

        if (cooldown > 0)
        {
            cooldown--;
        }
    }

    public bool IsAttacking()
    {
        return attackLockoutTimer > 0f;
    }
}
