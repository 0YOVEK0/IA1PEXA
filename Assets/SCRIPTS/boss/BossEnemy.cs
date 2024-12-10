using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float detectionRange = 10f;
    public float meleeRange = 2f; // Este puede ser innecesario si solo usas ataque a distancia
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

    private Animator animator;
    public AudioSource bossMusic;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = true;
        currentState = EnemyState.Patrolling;

        animator = GetComponent<Animator>(); // Obtener el Animator del Boss

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

        // Cambiar animación a caminar
        animator.SetFloat("Speed", agent.velocity.magnitude);

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

        if (bossMusic != null && !bossMusic.isPlaying)
        {
            bossMusic.Play();
        }
    }

    private void HandleChasingState()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

        // Cambiar animación a caminar mientras persigue al jugador
        animator.SetFloat("Speed", agent.velocity.magnitude);

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
        // Cambiar animación a ataque a distancia
        animator.SetTrigger("RangeAttack");

        // Si está dentro del rango, hacer un disparo
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            // Aquí se simula un ataque a distancia (puede ser disparar un proyectil)
            ShootAtPlayer();
        }

        // Después de atacar, esperar un poco antes de volver a perseguir o patrullar
        currentState = EnemyState.Resting;
    }

    private void HandleRestingState()
    {
        // Cambiar animación a idle (reposo)
        animator.SetFloat("Speed", 0f);

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
        animator.SetTrigger("RangeAttack");  // Activar animación de disparo

        // Añadir lógica de disparo aquí, por ejemplo, creando un proyectil.
        yield return new WaitForSeconds(shotCooldown); // Espera el tiempo de cooldown
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
