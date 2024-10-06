using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 100; // Vida máxima del enemigo
    protected int currentHP; // Vida actual del enemigo
    public int damageToPlayer = 10; // Daño que hace al jugador al tocarlo

    void Start()
    {
        currentHP = maxHP; // Inicializar vida
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage; // Reducir vida
        Debug.Log(gameObject.name + " tiene " + currentHP + " HP.");

        if (currentHP <= 0)
        {
            Die(); // Matar al enemigo si su vida llega a 0
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        Destroy(gameObject); // Destruir el enemigo al morir
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con el jugador (tag "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageToPlayer); // Hacer daño al jugador
        }
    }
}
