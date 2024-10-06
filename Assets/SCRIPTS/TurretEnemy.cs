using UnityEngine;

public class TurretEnemy : Enemy
{
    public float visionAngle = 45f; // Ángulo de visión de la torreta
    public float visionDistance = 5f; // Distancia de visión
    public float rotationSpeed = 30f; // Velocidad de rotación del cono de visión
    public Transform firePoint; // Punto de disparo
    public GameObject bulletPrefab; // Prefab de bala
    public float fireRate = 2f; // Velocidad de disparo
    public float bulletSpeed = 10f; // Velocidad de la bala

    private bool playerInSight = false; // Si el jugador está en el campo de visión
    private Transform player; // Referencia al jugador
    private float nextFireTime = 0f; // Tiempo para el próximo disparo

    void Update()
    {
        RotateVision(); // Rotar el cono de visión

        // Detectar al jugador
        if (playerInSight && Time.time >= nextFireTime)
        {
            ShootAtPlayer(); // Disparar al jugador
            nextFireTime = Time.time + 1f / fireRate; // Establecer el tiempo para el próximo disparo
        }
    }

    void RotateVision()
    {
        // Lógica para rotar el cono de visión cuando no se detecta al jugador
        if (!playerInSight)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime); // Rotar lentamente
        }
    }

    void ShootAtPlayer()
    {
        if (player != null)
        {
            Debug.Log("Disparando hacia el jugador"); // Mensaje de depuración
            // Crear una bala que se dirija hacia la posición actual del jugador
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Vector2 direction = (player.position - firePoint.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed; // Usar bulletSpeed
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Verificar si el jugador está dentro del campo de visión
            player = collider.transform;
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            float angle = Vector2.Angle(transform.up, directionToPlayer);

            if (angle < visionAngle / 2f && Vector2.Distance(transform.position, player.position) <= visionDistance)
            {
                playerInSight = true;
            }
            else
            {
                playerInSight = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInSight = false; // Dejar de detectar al jugador
        }
    }

    
}
