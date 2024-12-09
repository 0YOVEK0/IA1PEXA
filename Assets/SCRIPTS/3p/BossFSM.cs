using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossFSM : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;

    public float detectionRange = 10f; // Rango de detección
    public float meleeRange = 2f; // Rango para ataque cuerpo a cuerpo
    public float attackCooldown = 1f; // Tiempo entre ataques

    private HealthSystem healthSystem; // Referencia al sistema de vida del jefe

    public GameObject projectilePrefab; // Prefabricado del proyectil (para el ataque a distancia)
    public Transform firePoint; // Punto desde donde se dispara el proyectil

    private enum BossState { Idle, Melee, Range, Dead }
    private BossState currentState;

    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        healthSystem = GetComponent<HealthSystem>(); // Obtener el componente de salud
        currentState = BossState.Idle;
        agent.speed = 3f;
        agent.acceleration = 6f;
    }

    void Update()
    {
        // Ejecutamos la lógica del estado actual
        switch (currentState)
        {
            case BossState.Idle:
                HandleIdleState();
                break;

            case BossState.Melee:
                HandleMeleeState();
                break;

            case BossState.Range:
                HandleRangeState();
                break;

            case BossState.Dead:
                HandleDeadState();
                break;
        }
    }

    // Estado en el que el jefe espera o patrulla
    private void HandleIdleState()
    {
        animator.SetBool("isMoving", false); // Desactivar animación de movimiento

        // Si el jugador está dentro del rango de detección, cambia al estado adecuado
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < meleeRange)
        {
            currentState = BossState.Melee;
        }
        else if (distanceToPlayer < detectionRange)
        {
            currentState = BossState.Range;
        }
    }

    // Estado de ataque cuerpo a cuerpo
    private void HandleMeleeState()
    {
        animator.SetBool("isMoving", false); // Desactivar animación de movimiento
        animator.SetTrigger("MeleeAttack"); // Activar animación de ataque cuerpo a cuerpo

        // Ejecutar el ataque cuerpo a cuerpo si el jefe está lo suficientemente cerca
        if (!isAttacking)
        {
            StartCoroutine(MeleeAttack());
        }
    }

    // Estado de ataque a distancia
    private void HandleRangeState()
    {
        animator.SetBool("isMoving", true); // Activar animación de movimiento
        agent.SetDestination(player.position); // El jefe se mueve hacia el jugador

        // Si está lo suficientemente cerca, ataca a distancia
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= meleeRange)
        {
            currentState = BossState.Melee;
        }
        else
        {
            // Ejecutar el ataque a distancia
            if (!isAttacking)
            {
                StartCoroutine(RangedAttack());
            }
        }
    }

    // Estado cuando el jefe está muerto
    private void HandleDeadState()
    {
        animator.SetTrigger("Die"); // Activar animación de muerte
        Destroy(gameObject); // Destruir el objeto del Boss
    }

    // Método para ataque cuerpo a cuerpo
    private IEnumerator MeleeAttack()
    {
        isAttacking = true;
        Debug.Log("Boss realizando ataque cuerpo a cuerpo");
        
        // Lógica para aplicar daño al jugador (puedes modificar esto para que sea más realista)
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= meleeRange)
        {
            player.GetComponent<HealthSystem>().TakeDamage(20); // Llamamos al sistema de vida del jugador
        }

        yield return new WaitForSeconds(attackCooldown); // Esperar el tiempo de recarga entre ataques
        isAttacking = false;
    }

    // Método para ataque a distancia (por ejemplo, lanzar un proyectil)
    private IEnumerator RangedAttack()
    {
        isAttacking = true;
        Debug.Log("Boss realizando ataque a distancia");

        // Instanciar el proyectil
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (player.position - firePoint.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 10f; // Asumimos que el proyectil tiene un Rigidbody2D

        animator.SetTrigger("RangeAttack"); // Activar animación de ataque a distancia

        yield return new WaitForSeconds(attackCooldown); // Esperar el tiempo de recarga entre ataques
        isAttacking = false;
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage); // Llamamos al sistema de vida para reducir la salud
        Debug.Log("Boss ha recibido daño.");

        // Usamos el getter de la salud para verificar si el jefe ha muerto
        if (healthSystem.GetCurrentHealth() <= 0)
        {
            currentState = BossState.Dead; // Cambiar al estado de muerte
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}
