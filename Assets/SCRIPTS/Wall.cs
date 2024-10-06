using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool isDestructible = true; // Variable para determinar si el muro es destructible

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobar si el objeto que colisionó es una bala
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Verificar si el muro es destructible
            if (isDestructible)
            {
                Destroy(gameObject); // Destruir el muro
            }
            Destroy(collision.gameObject); // Destruir la bala al chocar con el muro
        }
    }
}
