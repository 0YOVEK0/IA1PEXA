using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10; // Daño que inflige la bala, ahora es un int
    public float lifeTime = 2f; // Tiempo de vida de la bala antes de que se destruya

    void Start()
    {
        // Destruir la bala después de un tiempo
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            // Aquí puedes aplicar el daño al jugador
            PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damage); // Llamar al método TakeDamage
                Debug.Log("Impacto con jugador. Daño infligido: " + damage);
            }

            Destroy(gameObject); // Destruir la bala
        }
        else if (hitInfo.CompareTag("Wall"))
        {
            Debug.Log("Impacto con pared");
            Destroy(gameObject); // Destruir la bala
        }
    }
}
