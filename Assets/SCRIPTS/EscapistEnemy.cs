using UnityEngine;
using System.Collections;

public class EscapistEnemy : Enemy
{
    public float fleeDuration = 3f; // Duración del flee
    public float restDuration = 2f; // Duración de la pausa
    public float fleeSpeed = 3f; // Velocidad de escape
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
        rb.velocity = direction * fleeSpeed;
    }
}
