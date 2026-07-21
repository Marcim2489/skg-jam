using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float velocidade = 50f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InputAction movement;

    [Header("Visual")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Partículas")]
    [SerializeField] private ParticleSystem runDust;
    [SerializeField] private Transform dustPoint;

    [SerializeField] private float dustInterval = 0.12f;
    [SerializeField] private int particlesPerStep = 3;

    private float dustTimer;
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

        // Movimento
        rb.linearVelocity = direcao.normalized * velocidade;

        // Animação
        bool moving = direcao.sqrMagnitude > 0.01f;
        animator.SetBool("moving", moving);

        // Virar sprite
        if (direcao.x > 0)
        {
            spriteRenderer.flipX = false;
            dustPoint.localPosition = new Vector3(-0.12f, -0.18f, 0);
        }
        else if (direcao.x < 0)
        {
            spriteRenderer.flipX = true;
            dustPoint.localPosition = new Vector3(0.12f, -0.18f, 0);
        }

        // Emissão de poeira
        if (moving)
        {
            dustTimer += Time.deltaTime;

            if (dustTimer >= dustInterval)
            {
                dustTimer = 0f;
                runDust.Emit(particlesPerStep);
            }
        }
        else
        {
            dustTimer = dustInterval;
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
