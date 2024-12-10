using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossCombatWithObstacle : MonoBehaviour
{
    public GameObject countdownTextObject; // Objeto que contiene el texto del conteo regresivo
    public Text countdownText;             // Texto para mostrar el conteo regresivo
    public GameObject boss;                // El objeto del jefe que se activará
    public GameObject obstacle;            // El obstáculo que se destruirá
    public Slider slider;                  // Slider que se ocultará
    public float countdownTime = 5f;       // Tiempo del conteo regresivo en segundos

    private bool combatStarted = false;    // Para evitar iniciar el combate más de una vez

    void Start()
    {
        // Asegúrate de que el texto del conteo regresivo, el slider y el jefe estén inactivos al inicio
        countdownTextObject.SetActive(false);
        if (slider != null)
        {
            slider.gameObject.SetActive(false);
        }
        if (boss != null)
        {
            boss.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si el obstáculo ha sido destruido
        if (obstacle == null && !combatStarted)
        {
            // Iniciar el conteo regresivo después de destruir el obstáculo
            StartCombatCountdown();
        }
    }

    private void StartCombatCountdown()
    {
        Debug.Log("Starting combat countdown.");
        combatStarted = true; // Evitar que se inicie más de una vez
        countdownTextObject.SetActive(true); // Mostrar el texto del conteo regresivo

        // Mostrar el slider si está configurado
        if (slider != null)
        {
            slider.gameObject.SetActive(true);
        }

        StartCoroutine(CombatCountdownCoroutine());
    }

    IEnumerator CombatCountdownCoroutine()
    {
        float currentCountdownTime = countdownTime;

        // Ejecutar el conteo regresivo
        while (currentCountdownTime > 0)
        {
            countdownText.text = Mathf.Ceil(currentCountdownTime).ToString("F0");

            // Actualizar el slider si está configurado
            if (slider != null)
            {
                slider.value = currentCountdownTime / countdownTime; // Actualizar valor del slider
            }

            currentCountdownTime -= Time.deltaTime;
            yield return null;
        }

        // Cuando el tiempo llegue a cero, activar al jefe
        countdownText.text = "FIGHT!";
        Debug.Log("Combat started.");

        if (boss != null)
        {
            boss.SetActive(true); // Activar al jefe
            Debug.Log("Boss activated.");
        }

        yield return new WaitForSeconds(1f);

        countdownTextObject.SetActive(false); // Desactivar el texto del conteo

        // Ocultar el slider si está configurado
        if (slider != null)
        {
            slider.gameObject.SetActive(false);
        }
    }
}
