using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    private Animator animator;
    public GameObject EnemyLeftHand;
    public GameObject EnemyRightHand;

    private PunchHitbox leftHandHitbox;
    private PunchHitbox rightHandHitbox;

    public int punchCooldown = 0;

    [Header("Enemy Combat Settings")]
    public EnemyLeaning enemyLeaning;     
    [Range(0f, 1f)] public float dodgeChance = 0.7f; 
    
    private enum EnemyState { Idle, WindingUp, Attacking, Dodging }
    private EnemyState currentState = EnemyState.Idle;

    private float stateTimer = 0f;
    private float idleAggressionTimer = 0f;
    private int pendingPunchType = 1;

    private const float PUNCH_ANIMATION_LENGTH = 1.967f;
    private const float WINDUP_TIME = 0.25f;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (enemyLeaning == null)
        {
            enemyLeaning = GetComponentInChildren<EnemyLeaning>();
        }

        if (EnemyLeftHand != null) leftHandHitbox = EnemyLeftHand.GetComponent<PunchHitbox>();
        if (EnemyRightHand != null) rightHandHitbox = EnemyRightHand.GetComponent<PunchHitbox>();

        DisableAllHitboxes();
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
                    pendingPunchType = Random.Range(1, 5);
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
                    DisableAllHitboxes();
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

        bool isLeftHand = (punchType == 1 || punchType == 3);

        if (EnemyLeftHand != null) EnemyLeftHand.SetActive(isLeftHand);
        if (EnemyRightHand != null) EnemyRightHand.SetActive(!isLeftHand);

        PunchHitbox activeHitbox = isLeftHand ? leftHandHitbox : rightHandHitbox;
        if (activeHitbox != null) activeHitbox.ResetHit();

        switch (punchType)
        {
            case 1:
                animator.SetTrigger("JabL"); 
                break;
            case 2:
                animator.SetTrigger("JabR"); 
                break;
            case 3:
                animator.SetTrigger("CPunchL"); 
                break;
            case 4:
                animator.SetTrigger("CPunchR"); 
                break;
        }
    }

    public void DisableAllHitboxes()
    {
        if (EnemyLeftHand != null) EnemyLeftHand.SetActive(false);
        if (EnemyRightHand != null) EnemyRightHand.SetActive(false);
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
            DisableAllHitboxes();
            currentState = EnemyState.Idle;
            ResetAggressionTimer();
            return;
        }

        float roll = Random.value;
        if (roll <= dodgeChance)
        {
            currentState = EnemyState.Dodging;
            stateTimer = PUNCH_ANIMATION_LENGTH * 0.7f;
            DisableAllHitboxes();

            if (playerPunchSide == "Right")
            {
                if (enemyLeaning != null) enemyLeaning.SetLeanInput(1f);
            }
            else if (playerPunchSide == "Left")
            {
                if (enemyLeaning != null) enemyLeaning.SetLeanInput(-1f);
            }
        }
    }
}