using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Método para comenzar el juego
    public void PlayGame()
    {
        // Asegúrate de que la escena "GameScene" esté añadida en el Build Settings
        SceneManager.LoadScene("lvl1");
    }

    // Método para abrir la configuración (puedes implementar un nuevo menú aquí)
    public void OpenSettings()
    {
        // Aquí podrías abrir otro menú de configuración
        Debug.Log("Abriendo Configuración...");
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Esta línea solo funciona en el build del juego, no en el editor.
    }
}
