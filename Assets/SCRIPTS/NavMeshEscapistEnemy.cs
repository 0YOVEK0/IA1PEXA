using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NavMeshEscapistEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float detectionRange = 10f;
    public float fleeDistance = 5f;
    public float activeStateDuration = 5f;
    public float tiredStateDuration = 3f;
    public float lineOfSightDuration = 2f;
    public float shotCooldown = 1f;
    
    public int maxHP = 50; // Vida máxima del enemigo
    private int currentHP; // Vida actual del enemigo

    private enum EnemyState { Active, Tired }
    private EnemyState currentState;
    private float tiredStateTimer;
    private float activeStateTimer;
    private bool isShooting;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        currentState = EnemyState.Active;
        agent.speed = 2f; // Ajustar para movimiento más ligero
        agent.acceleration = 6f;

        currentHP = maxHP; // Inicializar vida
    }

    void Update()
    {
        if (currentState == EnemyState.Active)
        {
            HandleActiveState();
        }
        else if (currentState == EnemyState.Tired)
        {
            HandleTiredState();
        }
    }

    private void HandleActiveState()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRange);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            agent.SetDestination(transform.position);
            if (!isShooting)
            {
                StartCoroutine(ShootAtPlayer());
            }
        }
        else
        {
            agent.SetDestination(player.position);
        }

        if (distanceToPlayer < detectionRange && currentState != EnemyState.Tired)
        {
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            Vector3 fleePosition = transform.position + fleeDirection * fleeDistance;
            agent.SetDestination(fleePosition);
            StartCoroutine(HandleFleeCooldown());
        }

        activeStateTimer += Time.deltaTime;
        if (activeStateTimer >= activeStateDuration)
        {
            currentState = EnemyState.Tired;
            activeStateTimer = 0f;
        }
    }

    private void HandleTiredState()
    {
        agent.SetDestination(transform.position);

        if (!isShooting)
        {
            StartCoroutine(ShootAtPlayer());
        }

        tiredStateTimer += Time.deltaTime;
        if (tiredStateTimer >= tiredStateDuration)
        {
            currentState = EnemyState.Active;
            tiredStateTimer = 0f;
        }
    }

    private IEnumerator ShootAtPlayer()
    {
        isShooting = true;
        Debug.Log("Shooting at player!");
        yield return new WaitForSeconds(shotCooldown);
        isShooting = false;
    }

    private IEnumerator HandleFleeCooldown()
    {
        yield return new WaitForSeconds(2f);
        currentState = EnemyState.Tired;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("HP del enemigo: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El enemigo ha muerto");
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRange);
        if (hit.collider != null)
        {
            Gizmos.DrawLine(transform.position, hit.point);
        }
    }
}
