using UnityEngine;

public class RotateSaw : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 360f; // Graus por segundo

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}