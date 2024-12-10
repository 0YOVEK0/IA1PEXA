using UnityEngine;
using UnityEngine.UI;

public class BossDetection : MonoBehaviour
{
    public GameObject boss;           
    public GameObject gameOverPanel;  
    void Update()
    {
        // Comprobar si el boss ha sido destruido (si su referencia es nula)
        if (boss == null)
        {
            // Si el boss ha muerto, activar el panel
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true); // Activa el panel
            }
        }
    }
}
