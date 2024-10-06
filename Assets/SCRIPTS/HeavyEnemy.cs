using UnityEngine;

public class HeavyEnemy : Enemy
{
    public float acceleration = 0.5f; // Aceleración
    public float maxSpeed = 2f; // Velocidad máxima
    private Transform player; // Referencia al jugador
    private Rigidbody2D rb; // Rigidbody del enemigo

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Lógica para seguir al jugador con aceleración
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = Vector2.Lerp(rb.velocity, direction * maxSpeed, acceleration * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Al chocar con una pared, detener el movimiento
            rb.velocity = Vector2.zero;
        }
    }
}
