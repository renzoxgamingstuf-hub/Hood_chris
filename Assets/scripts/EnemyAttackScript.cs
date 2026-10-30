using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    private Animator animator;
    public GameObject EnemyLeftHand;
    public GameObject EnemyRightHand;

    public int punchCooldown = 0;

    [Header("Enemy Combat Settings")]
    public EnemyLeaning enemyLeaning;     
    [Range(0f, 1f)] public float dodgeChance = 0.7f; 
    
    private enum EnemyState { Idle, WindingUp, Attacking, Dodging }
    private EnemyState currentState = EnemyState.Idle;

    private float stateTimer = 0f;
    private float idleAggressionTimer = 0f;
    private int pendingPunchType = 0;

    private const float PUNCH_ANIMATION_LENGTH = 1.967f;
    private const float WINDUP_TIME = 0.25f;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (enemyLeaning == null)
        {
            enemyLeaning = GetComponentInChildren<EnemyLeaning>();
        }
        ResetAggressionTimer();
    }

    void Update()
    {
        if (punchCooldown > 0)
        {
            punchCooldown--;
        }

        switch (currentState)
        {
            case EnemyState.Idle:
                idleAggressionTimer -= Time.deltaTime;
                if (idleAggressionTimer <= 0f && punchCooldown <= 0)
                {
                    pendingPunchType = Random.Range(1, 3);
                    currentState = EnemyState.WindingUp;
                    stateTimer = WINDUP_TIME;
                }
                break;

            case EnemyState.WindingUp:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0f)
                {
                    ExecutePunch(pendingPunchType);
                }
                break;

            case EnemyState.Attacking:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0f)
                {
                    currentState = EnemyState.Idle;
                    ResetAggressionTimer();
                }
                break;

            case EnemyState.Dodging:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0f)
                {
                    if (enemyLeaning != null) enemyLeaning.SetLeanInput(0f);
                    
                    if (Random.value <= 0.8f && punchCooldown <= 0)
                    {
                        pendingPunchType = Random.Range(1, 3);
                        currentState = EnemyState.WindingUp;
                        stateTimer = WINDUP_TIME;
                    }
                    else
                    {
                        currentState = EnemyState.Idle;
                        ResetAggressionTimer();
                    }
                }
                break;
        }
    }

    private void ExecutePunch(int punchType)
    {
        currentState = EnemyState.Attacking;
        stateTimer = PUNCH_ANIMATION_LENGTH;
        punchCooldown = 90;

        if (punchType == 2)
        {
            animator.SetTrigger("CPunchR");
            EnemyLeftHand.SetActive(false);
            EnemyRightHand.SetActive(true);
        }
        else if (punchType == 1)
        {
            animator.SetTrigger("CPunchL");
            EnemyRightHand.SetActive(false);
            EnemyLeftHand.SetActive(true);
        }
    }

    private void ResetAggressionTimer()
    {
        idleAggressionTimer = Random.Range(0.8f, 1.5f);
    }

    public void OnPlayerAttack(string playerPunchSide)
    {
        if (currentState == EnemyState.Attacking || currentState == EnemyState.Dodging) 
            return;

        if (currentState == EnemyState.WindingUp)
        {
            currentState = EnemyState.Idle;
            ResetAggressionTimer();
            return;
        }

        float roll = Random.value;
        if (roll <= dodgeChance)
        {
            currentState = EnemyState.Dodging;
            stateTimer = PUNCH_ANIMATION_LENGTH * 0.7f;

            if (playerPunchSide == "Right")
            {
                if (enemyLeaning != null) enemyLeaning.SetLeanInput(-1f); // Dodge Left
            }
            else if (playerPunchSide == "Left")
            {
                if (enemyLeaning != null) enemyLeaning.SetLeanInput(1f);  // Dodge Right
            }
        }
    }
}
