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

    private enum EnemyState { Active, Tired }
    private EnemyState currentState;
    private float tiredStateTimer;
    private float activeStateTimer;
    private bool isShooting;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        currentState = EnemyState.Active;
        agent.speed = 2f; // Adjust for lighter movement
        agent.acceleration = 6f;
    }

    // Update is called once per frame
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
        
        // Line of sight check
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRange);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // If enemy has line of sight, stop moving
            agent.SetDestination(transform.position);
            if (!isShooting)
            {
                StartCoroutine(ShootAtPlayer());
            }
        }
        else
        {
            // If no line of sight, move towards player
            agent.SetDestination(player.position);
        }

        // If player is within detection range and enemy is not tired, flee
        if (distanceToPlayer < detectionRange && currentState != EnemyState.Tired)
        {
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            Vector3 fleePosition = transform.position + fleeDirection * fleeDistance;
            agent.SetDestination(fleePosition);
            StartCoroutine(HandleFleeCooldown());
        }

        // Transition to Tired state after active state duration
        activeStateTimer += Time.deltaTime;
        if (activeStateTimer >= activeStateDuration)
        {
            currentState = EnemyState.Tired;
            activeStateTimer = 0f;
        }
    }

    private void HandleTiredState()
    {
        // Stop movement when tired
        agent.SetDestination(transform.position);
        
        // Shoot at player while tired
        if (!isShooting)
        {
            StartCoroutine(ShootAtPlayer());
        }

        // Transition back to Active state after tired state duration
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
        // Implement shooting logic here (e.g., instantiate a bullet or projectile)
        Debug.Log("Shooting at player!");
        yield return new WaitForSeconds(shotCooldown);
        isShooting = false;
    }

    private IEnumerator HandleFleeCooldown()
    {
        // Handle fleeing cooldown before state transition
        yield return new WaitForSeconds(2f);
        currentState = EnemyState.Tired; // Transition to Tired state after fleeing
    }

    private void OnDrawGizmos()
    {
        // Debugging Gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Debugging Raycast (line of sight)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRange);
        if (hit.collider != null)
        {
            Gizmos.DrawLine(transform.position, hit.point);
        }
    }
}
