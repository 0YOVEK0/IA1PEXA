using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f; // Daño que causará el proyectil
    public float lifetime = 2f; // Tiempo de vida del proyectil

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destruir el proyectil después de un tiempo
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Lógica para causar daño al jugador
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.TakeDamage((int)damage); // Llama al método para causar daño
            }
            Destroy(gameObject); // Destruir el proyectil al colisionar
        }
    }
}
