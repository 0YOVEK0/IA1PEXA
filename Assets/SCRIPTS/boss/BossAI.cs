using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public enum BossState { Patrol, Chase, AttackMelee, AttackRanged, Rest }
    public BossState currentState;

    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    public float meleeRange = 2f;
    public float rangedRange = 5f;
    public float detectionRange = 10f;

    private float restTime = 3f;
    private float restTimer = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        currentState = BossState.Patrol;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case BossState.Patrol:
                Patrol();
                break;
            case BossState.Chase:
                Chase();
                break;
            case BossState.AttackMelee:
                AttackMelee();
                break;
            case BossState.AttackRanged:
                AttackRanged();
                break;
            case BossState.Rest:
                Rest();
                break;
        }

        HandleTransitions();
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
        SetAnimationState(isPatrolling: true);
    }

    void Chase()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
        SetAnimationState(isChasing: true);
    }

    void AttackMelee()
    {
        // Aquí implementamos un ataque cuerpo a cuerpo
        Debug.Log("Ataque cuerpo a cuerpo");
        SetAnimationState(isAttackingMelee: true);
    }

    void AttackRanged()
    {
        // Aquí implementamos un ataque a distancia
        Debug.Log("Ataque a distancia");
        SetAnimationState(isAttackingRanged: true);
    }

    void Rest()
    {
        agent.isStopped = true;
        restTimer += Time.deltaTime;
        SetAnimationState(isResting: true);

        if (restTimer >= restTime)
        {
            restTimer = 0;
            currentState = BossState.Patrol;
            agent.isStopped = false;
        }
    }

    void HandleTransitions()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case BossState.Patrol:
                if (distanceToPlayer < detectionRange)
                {
                    currentState = BossState.Chase;
                }
                break;
            case BossState.Chase:
                if (distanceToPlayer < meleeRange)
                {
                    currentState = BossState.AttackMelee;
                }
                else if (distanceToPlayer < rangedRange)
                {
                    currentState = BossState.AttackRanged;
                }
                break;
            case BossState.AttackMelee:
            case BossState.AttackRanged:
                if (distanceToPlayer > detectionRange)
                {
                    currentState = BossState.Patrol;
                }
                break;
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void SetAnimationState(bool isPatrolling = false, bool isChasing = false, 
                           bool isAttackingMelee = false, bool isAttackingRanged = false, 
                           bool isResting = false)
    {
        animator.SetBool("isPatrolling", isPatrolling);
        animator.SetBool("isChasing", isChasing);
        animator.SetBool("isAttackingMelee", isAttackingMelee);
        animator.SetBool("isAttackingRanged", isAttackingRanged);
        animator.SetBool("isResting", isResting);
    }
}
