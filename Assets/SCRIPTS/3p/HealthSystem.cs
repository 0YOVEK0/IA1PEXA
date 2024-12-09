using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Getter público para obtener la salud actual
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject);
    }
}
