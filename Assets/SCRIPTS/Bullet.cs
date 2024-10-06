using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f; // Tiempo de vida de la bala antes de que se destruya
    public int damage = 10; // Daño que causa la bala

    void Start()
    {
        // Destruir la bala después de un tiempo
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            // Aquí puedes aplicar el daño al enemigo
            Debug.Log("Impacto con enemigo");
            Enemy enemy = hitInfo.GetComponent<Enemy>(); // Obtén el componente Enemy
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Aplica el daño
            }
            Destroy(gameObject); // Destruir la bala
        }
        else if (hitInfo.CompareTag("Wall"))
        {
            Wall wall = hitInfo.GetComponent<Wall>();
            if (wall != null && wall.isDestructible)
            {
                Debug.Log("Impacto con muro destructible");
                Destroy(wall.gameObject); // Destruir el muro
            }
            Destroy(gameObject); // Destruir la bala
        }
    }
}
