using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public Slider healthSlider;  
    public Enemy enemy;         

    void Update()
    {
        // Asegurarnos de que el slider y el enemigo estén asignados
        if (healthSlider != null && enemy != null)
        {
            // Actualizar el valor del Slider con la vida actual del enemigo
            healthSlider.value = enemy.GetCurrentHP();
        }
    }
}
