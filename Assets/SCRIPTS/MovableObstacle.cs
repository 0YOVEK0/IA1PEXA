using UnityEngine;
using NavMeshPlus.Components; // Usa NavMeshPlus
using System.Collections;

public class MovableObstacle : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;  // Cambiado a NavMeshSurface
    public Vector3 moveDirection = Vector3.right;
    public float moveDistance = 1f;
    public float moveTime = 2f;

    private Vector3 initialPosition;
    private bool isMoving = false;

    void Start()
    {
        initialPosition = transform.position;
        InvokeRepeating("MoveObstacle", 0f, moveTime);
    }

    void MoveObstacle()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine());
        }
    }

    IEnumerator MoveCoroutine()
    {
        isMoving = true;
        Vector3 targetPosition = initialPosition + moveDirection * moveDistance;
        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        navMeshSurface.BuildNavMesh();  // Actualiza el NavMesh
        isMoving = false;
    }
}
