using UnityEngine;

public class Turret : MonoBehaviour
{
    public float viewAngle = 60f; // Ángulo del cono de visión
    public float viewDistance = 5f; // Distancia del cono de visión
    public float rotationSpeed = 30f; // Velocidad de rotación
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto de disparo
    public float fireRate = 1f; // Tasa de disparo
    private float fireTimer; // Temporizador para el disparo
    private bool playerInSight = false; // Indica si el jugador está en el cono de visión

    void Update()
    {
        RotateTurret();
        if (playerInSight)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                Shoot();
                fireTimer = fireRate; // Reiniciar temporizador
            }
        }
    }

    private void RotateTurret()
    {
        // Rotar el cono de visión
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        // Crear el proyectil en el punto de disparo
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        
        // Obtener el Rigidbody2D y darle velocidad
        Rigidbody2D bulletRb = projectile.GetComponent<Rigidbody2D>();
        bulletRb.velocity = firePoint.up * bulletRb.velocity.magnitude; // Dispara en la dirección del fuego
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInSight = true; // El jugador está dentro del cono de visión
            fireTimer = 0f; // Permitir disparar inmediatamente
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInSight = false; // El jugador salió del cono de visión
        }
    }
}
