using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]float velocidade = 50f;
    [SerializeField]Rigidbody2D rigidbody2d;
    [SerializeField]InputAction movement;
    [SerializeField]Animator animator;
    [SerializeField]SpriteRenderer spriteRenderer;

    void Start()
    {
        movement.Enable();
    }

    void Update()
    {
        Vector2 direcao = movement.ReadValue<Vector2>();

        rigidbody2d.linearVelocity = direcao.normalized*velocidade;
        animator.SetBool("moving", direcao != Vector2.zero);
        if (direcao.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direcao.x < 0)
        {
            spriteRenderer.flipX = true;
        }

    }
}
