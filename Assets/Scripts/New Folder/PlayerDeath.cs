using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerController controller;

    [Header("Death Jump")]
    [SerializeField] private float jumpForce = 8f;

    [Header("Restart")]
    [SerializeField] private float restartDelay = 2f;

    private bool dead;

    public void Die()
    {
        if (dead) return;

        dead = true;

        // Desativa o controle
        controller.enabled = false;

        // Remove velocidade anterior
        rb.linearVelocity = Vector2.zero;

        // Faz a física controlar o personagem
        rb.gravityScale = 3f;

        // Pequeno salto
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}