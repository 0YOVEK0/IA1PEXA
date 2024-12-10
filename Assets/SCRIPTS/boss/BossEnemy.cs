using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float detectionRange = 10f;
    public float meleeRange = 2f;
    public float shotCooldown = 1f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    private bool isPatrolling = true;

    private enum EnemyState { Patrolling, Chasing, Attacking, Resting }
    private EnemyState currentState;
    private float shotCooldownTimer;
    private bool isShooting;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = true;
        currentState = EnemyState.Patrolling;

        agent.speed = patrolSpeed;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                HandlePatrollingState();
                break;
            case EnemyState.Chasing:
                HandleChasingState();
                break;
            case EnemyState.Attacking:
                HandleAttackingState();
                break;
            case EnemyState.Resting:
                HandleRestingState();
                break;
        }
    }

    private void HandlePatrollingState()
    {
        if (patrolPoints.Length == 0) return;

        agent.speed = patrolSpeed;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);

        // Si llegamos al punto de patrullaje, ir al siguiente
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        // Verificar si el jugador está dentro del rango para empezar a perseguir
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            currentState = EnemyState.Chasing;
        }
    }

    private void HandleChasingState()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

        // Verificar si el jugador está dentro del rango de melee
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < meleeRange)
        {
            currentState = EnemyState.Attacking;
        }
        else if (distanceToPlayer > detectionRange)
        {
            currentState = EnemyState.Patrolling;
        }

        // Intentar disparar al jugador si está dentro del rango
        if (distanceToPlayer <= detectionRange && !isShooting)
        {
            StartCoroutine(ShootAtPlayer());
        }
    }

    private void HandleAttackingState()
    {
        // Ataque cuerpo a cuerpo
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < meleeRange)
        {
            MeleeAttack();
        }

        // Después de atacar, esperar un poco antes de volver a perseguir o patrullar
        currentState = EnemyState.Resting;
    }

    private void HandleRestingState()
    {
        // Descansar durante un corto periodo después de un ataque
        StartCoroutine(RestCooldown());
    }

    private void MeleeAttack()
    {
        // Simular ataque cuerpo a cuerpo aquí
        Debug.Log("Melee attack!");
        // Aquí podrías hacer que el boss cause daño al jugador en el rango de melee
    }

    private IEnumerator ShootAtPlayer()
    {
        isShooting = true;
        Debug.Log("Shooting at player!");
        // Aquí puedes implementar el daño a distancia, por ejemplo, instanciando proyectiles.
        yield return new WaitForSeconds(shotCooldown);
        isShooting = false;
    }

    private IEnumerator RestCooldown()
    {
        yield return new WaitForSeconds(2f);  // Descanso después de un ataque
        currentState = EnemyState.Chasing;   // Regresar al estado de persecución
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Debug para los puntos de patrullaje
        foreach (var point in patrolPoints)
        {
            Gizmos.DrawSphere(point.position, 0.5f);
        }
    }
}
