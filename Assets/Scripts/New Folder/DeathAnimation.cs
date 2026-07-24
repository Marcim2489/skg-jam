using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    [Header("Salto")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.35f;

    [Header("Queda")]
    [SerializeField] private float fallSpeed = 7f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float fallDistance = 12f;

    private bool playing = false;

    public void PlayDeath()
    {
        if (playing) return;

        playing = true;
        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        Vector3 start = transform.position;

        // SALTO
        float t = 0;

        while (t < jumpDuration)
        {
            t += Time.deltaTime;

            float x = t / jumpDuration;

            // Parábola
            float y = 4 * jumpHeight * x * (1 - x);

            transform.position = start + Vector3.up * y;

            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        // QUEDA
        float fallen = 0;

        while (fallen < fallDistance)
        {
            float move = fallSpeed * Time.deltaTime;

            transform.position += Vector3.down * move;
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            fallen += move;

            yield return null;
        }

        // Aqui você chama sua tela de Game Over
        // GameManager.Instance.GameOver();
    }
}