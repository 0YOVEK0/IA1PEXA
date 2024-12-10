using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // El objeto jugador que la cámara seguirá
    public Vector3 offset;          // El offset de la cámara respecto al jugador
    public float followSpeed = 10f; // La velocidad con la que la cámara seguirá al jugador

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned in CameraFollow2D script.");
            return;
        }

        // Establecer el offset en el inicio
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Solo seguir el jugador en los ejes X y Y (en 2D)
            Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }
    }
}