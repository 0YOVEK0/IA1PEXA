using UnityEngine;
using System.Collections;

public class EscapistEnemy : Enemy
{
    public float fleeDuration = 3f; // Duración del flee
    public float restDuration = 2f; // Duración de la pausa
    public float fleeSpeed = 3f; // Velocidad de escape
    public LayerMask wallLayerMask; // Capa para los muros
    private Transform player; // Referencia al jugador
    private Rigidbody2D rb; // Rigidbody del enemigo
    private bool isFleeing = true; // Indica si está en modo flee o descanso

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FleeBehavior()); // Iniciar el ciclo de flee/descanso
    }

    IEnumerator FleeBehavior()
    {
        while (true)
        {
            if (isFleeing)
            {
                FleeFromPlayer();
                yield return new WaitForSeconds(fleeDuration);
            }
            else
            {
                rb.velocity = Vector2.zero; // Detenerse
                yield return new WaitForSeconds(restDuration);
            }
            isFleeing = !isFleeing; // Alternar entre flee y descanso
        }
    }

    void FleeFromPlayer()
    {
        // Lógica para escapar del jugador
        Vector2 direction = (transform.position - player.position).normalized;

        // Verificar si hay un muro en la dirección de huida
        if (IsWallBlocking(direction))
        {
            // Si hay un muro, ajusta la dirección para esquivarlo (aquí simplemente se invierte)
            direction = AdjustDirectionAwayFromWall(direction);
        }

        rb.velocity = direction * fleeSpeed;
    }

    bool IsWallBlocking(Vector2 direction)
    {
        // Usar un Raycast2D para verificar si hay un muro en la dirección de huida
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, wallLayerMask);
        
        // Si el raycast impacta algo, significa que hay un muro en el camino
        return hit.collider != null;
    }

    Vector2 AdjustDirectionAwayFromWall(Vector2 originalDirection)
    {
        // Ajusta la dirección para evitar el muro (puedes mejorar esta lógica)
        // Aquí se toma una dirección perpendicular como ejemplo
        Vector2 perpendicularDirection = new Vector2(-originalDirection.y, originalDirection.x);

        // Verificar si la nueva dirección también tiene un muro; si no, usarla
        if (!IsWallBlocking(perpendicularDirection))
        {
            return perpendicularDirection;
        }
        // Intentar la dirección opuesta si también está bloqueada
        return -perpendicularDirection;
    }
}
