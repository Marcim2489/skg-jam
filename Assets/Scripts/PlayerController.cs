using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]float velocidade = 50f;
    [SerializeField]Rigidbody2D rigidbody2d;
    [SerializeField]InputAction movement;
    [SerializeField]Animator animator;
    [SerializeField]SpriteRenderer spriteRenderer;
    [SerializeField]ObstacleDetector obstacleDetector;

    void Start()
    {
        movement.Enable();
        obstacleDetector.immunityStarted+=AplicarEfeitoImunidade;
        obstacleDetector.immunityEnded+=DesaplicarEfeitoImunidade;
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

    void OnDestroy()
    {
        obstacleDetector.immunityStarted-=AplicarEfeitoImunidade;
        obstacleDetector.immunityEnded-=DesaplicarEfeitoImunidade;
    }

    void AplicarEfeitoImunidade()
    {
        spriteRenderer.color = new Color(0.6f,0.6f,0.6f,1);
    }

    void DesaplicarEfeitoImunidade()
    {
        spriteRenderer.color = new Color(1,1,1,1);
    }

}
