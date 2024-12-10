using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI; 

public class DeathPanelController : MonoBehaviour
{
    public GameObject deathPanel;  
    public Button restartButton;     
    public Button quitButton;      

    void Start()
    {
        // Asegurarse de que el panel esté desactivado al inicio
        deathPanel.SetActive(false);

        // Asignar los métodos a los botones
        restartButton.onClick.AddListener(RestartLevel);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void ShowDeathPanel()
    {
        // Activar el panel de muerte cuando el jugador muera
        deathPanel.SetActive(true);
        Time.timeScale = 0f; // Detener el tiempo en el juego (pausar)
    }

    void RestartLevel()
    {
        // Reiniciar el nivel actual
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Cargar el nivel actual nuevamente
    }

    

    void QuitGame()
    {
        // Salir del juego
        Time.timeScale = 1f; // Asegurarse de que el tiempo esté normal antes de salir
        Application.Quit();  // Cierra la aplicación
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Detener el juego en el editor de Unity
        #endif
    }
}
