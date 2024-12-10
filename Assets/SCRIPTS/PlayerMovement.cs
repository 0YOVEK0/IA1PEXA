using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public int maxHP = 100; // Vida máxima del personaje
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint; // Punto desde donde se disparan las balas
    public float bulletSpeed = 10f; // Velocidad de la bala

    private int currentHP; // Vida actual del personaje
    private Vector2 movement; // Dirección del movimiento
    private Rigidbody2D rb; // Rigidbody para el personaje

    public GameObject deathPanel; // Panel de muerte (debe asignarse en el Inspector)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP; // Inicializar HP

        if (deathPanel != null)
        {
            deathPanel.SetActive(false); // Asegurarse de que el panel esté oculto al inicio
        }
    }

    void Update()
    {
        // Obtener entrada del jugador (teclas WASD o flechas)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalizar el vector de movimiento para que las diagonales no sean más rápidas
        movement = movement.normalized;

        // Llamar a la función para rotar el personaje
        RotateCharacter();

        // Llamar a la función de disparo cuando se presiona el botón de disparo (espacio o clic)
        if (Input.GetButtonDown("Fire1")) // Por defecto "Fire1" está asignado a clic izquierdo o la barra espaciadora
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // Mover al personaje
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void RotateCharacter()
    {
        if (movement != Vector2.zero) // Solo rotar si el personaje se está moviendo
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg; // Calcular ángulo de rotación
            rb.rotation = angle; // Aplicar la rotación
        }
    }

    void Shoot()
    {
        // Crear la bala en el punto de disparo con la rotación del personaje
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Obtener el Rigidbody2D de la bala y darle velocidad en la dirección hacia la que está mirando
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = firePoint.right * bulletSpeed; // Mueve la bala hacia adelante (eje X local)
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un enemigo (tag "Enemy")
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10); // Recibir daño al tocar un enemigo (puedes ajustar el valor)
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage; // Reducir HP
        Debug.Log("HP actual: " + currentHP);

        if (currentHP <= 0)
        {
            Die(); // Llamar a la función de muerte si el HP es 0 o menos
        }
    }

    void Die()
    {
        Debug.Log("El personaje ha muerto");

        // Si tienes un Panel de muerte en tu Canvas, activarlo
        if (deathPanel != null)
        {
            deathPanel.SetActive(true); // Activar el panel de muerte
        }

        
        Time.timeScale = 0f; // Detener el tiempo (si lo deseas) para que el jugador vea el panel
    }
}
