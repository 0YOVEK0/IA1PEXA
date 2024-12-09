using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    public int maxHealth = 100; // Salud máxima del jugador
    public int currentHealth;   // Salud actual del jugador
    public HealthSystem healthSystem; // Referencia al sistema de salud (opcional si lo tienes separado)

    public float moveSpeed = 5f; // Velocidad de movimiento del jugador
    public float meleeAttackRange = 2f; // Rango de ataque cuerpo a cuerpo
    public int meleeDamage = 20; // Daño del ataque cuerpo a cuerpo

    private Animator animator; // Referencia al animador del jugador
    private bool isAttacking = false; // Para evitar que ataque constantemente

    void Start()
    {
        currentHealth = maxHealth;
        healthSystem = GetComponent<HealthSystem>(); // Si usas un sistema de salud separado
        animator = GetComponent<Animator>(); // Si usas animaciones
    }

    void Update()
    {
        HandleMovement(); // Llamada al movimiento del jugador
        HandleMeleeAttack(); // Llamada al ataque cuerpo a cuerpo
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;

        transform.Translate(movement, Space.World);

        // Aquí podrías agregar animaciones, como mover al jugador
        animator.SetFloat("MoveX", horizontal);
        animator.SetFloat("MoveY", vertical);
        animator.SetBool("isMoving", movement.magnitude > 0);
    }

    void HandleMeleeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking) // Usamos la tecla espacio para atacar
        {
            isAttacking = true;
            StartCoroutine(MeleeAttack());
        }
    }

    private IEnumerator MeleeAttack()
    {
        animator.SetTrigger("MeleeAttack"); // Activamos la animación de ataque cuerpo a cuerpo

        // Esperar un poco antes de realizar el ataque
        yield return new WaitForSeconds(0.5f);

        // Verificar si el jefe está cerca para aplicarle el daño
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, meleeAttackRange);
        foreach (var enemy in hitEnemies)
        {
            if (enemy.CompareTag("Boss")) // Asegúrate de que el jefe tenga la etiqueta "Boss"
            {
                enemy.GetComponent<BossFSM>().TakeDamage(meleeDamage);
                Debug.Log("¡El jugador ha atacado al jefe!");
            }
        }

        yield return new WaitForSeconds(1f); // Tiempo de recarga entre ataques
        isAttacking = false;
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSystem.TakeDamage(damage); // Si tienes un sistema de salud separado

        Debug.Log("El jugador ha recibido daño. Salud restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die"); // Activar animación de muerte
        Destroy(gameObject); // Destruir el objeto jugador
        Debug.Log("El jugador ha muerto");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange); // Rango de ataque cuerpo a cuerpo
    }
}
