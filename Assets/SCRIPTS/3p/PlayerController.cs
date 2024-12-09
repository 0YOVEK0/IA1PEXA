using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  // Velocidad del jugador
    public int maxHealth = 100;  // Vida máxima
    private int currentHealth;  // Vida actual

    public GameObject bulletPrefab;  // Prefab del proyectil
    public Transform firePoint;  // Punto de disparo

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        HandleMovement();
        HandleShooting();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        rb.velocity = movement * speed;
    }

    private void HandleMovement()
    {
        // Obtener entradas del jugador
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        // Actualizar animaciones
        animator.SetBool("isRunning", movement.magnitude > 0);
    }

    private void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1"))  // Botón izquierdo del mouse
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        animator.SetTrigger("isTakingDamage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        rb.velocity = Vector2.zero;  // Detener movimiento

        Debug.Log("Player ha muerto.");
        // Opcional: Reiniciar el nivel o mostrar pantalla de muerte.
    }
}

