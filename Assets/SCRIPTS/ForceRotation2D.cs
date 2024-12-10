using UnityEngine;
using System.Collections;
public class ForceRotation2D : MonoBehaviour
{
    // Rotación deseada en los ejes Y y Z
    public float targetRotationZ = 0f; // Ángulo en el eje Z (usualmente la rotación 2D)
    public float targetRotationY = 0f; // Ángulo en el eje Y (para objetos 3D o efectos)
    
    void Update()
    {
        // Forzar la rotación en los ejes Y y Z
        transform.rotation = Quaternion.Euler(targetRotationY, targetRotationZ, 0f);
    }
}
