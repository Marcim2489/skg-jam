using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float velocidade = 50f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction interactPressed;

    [Header("Visual")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRendererGato;
    [SerializeField] private SpriteRenderer spriteRendererCachorro;

    [Header("Partículas")]
    [SerializeField] private ParticleSystem runDust;
    [SerializeField] private Transform dustPoint;

    [SerializeField] private float dustInterval = 0.12f;
    [SerializeField] private int particlesPerStep = 3;
    [SerializeField]float jumpForce = 0.1f;
    [SerializeField]ObstacleDetector hurtbox;

    private float dustTimer;
    // [SerializeField]ObstacleDetector obstacleDetector;
    MenuButton currentButton;

    Vector2 Direcao => movement.ReadValue<Vector2>();

    bool pulando = false;
    bool gatoSelecionado = false;

    private void OnEnable()
    {
        movement.Enable();
        interactPressed.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        interactPressed.Disable();
    }

    void Start()
    {
        if (GameManager.Instance.IdPersonagemAtual == 0)
        {
            gatoSelecionado = true;
            spriteRendererGato.gameObject.SetActive(true);
            spriteRendererCachorro.gameObject.SetActive(false);
        }
        else
        {
            gatoSelecionado = false;
            spriteRendererGato.gameObject.SetActive(false);
            spriteRendererCachorro.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        // Movimento
        rb.linearVelocity = Direcao.normalized * velocidade;

        // Animação
        bool moving = Direcao.sqrMagnitude > 0.01f;
        animator.SetBool("moving", moving);

        // Virar sprite
        if (Direcao.x > 0)
        {
            spriteRendererGato.flipX = false;
            spriteRendererCachorro.flipX = false;
            dustPoint.localPosition = new Vector3(-0.12f, -0.18f, 0);
        }
        else if (Direcao.x < 0)
        {
            spriteRendererGato.flipX = true;
            spriteRendererCachorro.flipX = true;
            dustPoint.localPosition = new Vector3(0.12f, -0.18f, 0);
        }

        // Emissão de poeira
        if (moving && pulando == false)
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

        if (interactPressed.WasPressedThisFrame())
        {
            if (gatoSelecionado && pulando == false)
            {
                StartCoroutine(Jump());
                return;
            }
            if(currentButton != null)
            {
                currentButton.Press();
            }
        }
    }


    IEnumerator Jump()
    {
        pulando = true;
        animator.Play("PlayerJump");
        hurtbox.gameObject.SetActive(false);
        while((spriteRendererGato.transform.position.y - transform.position.y) <= 1f)
        {
            spriteRendererGato.transform.position += Vector3.up * jumpForce;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        while((spriteRendererGato.transform.position.y - transform.position.y) >= 0f)
        {
            spriteRendererGato.transform.position -= Vector3.up * jumpForce;
            if ((spriteRendererGato.transform.position.y - transform.position.y) <= 0f)
            {
                break;
            }
            yield return null;
        }
        spriteRendererGato.transform.position = transform.position;
        if (Direcao == Vector2.zero)
        {
            animator.Play("PlayerIdle");
        }
        else
        {
            animator.Play("PlayerWalk");
        }
        hurtbox.gameObject.SetActive(true);
        pulando = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out MenuButton button))
        {
            currentButton = button;
            currentButton.Select();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out MenuButton button))
        {
            button.Deselect();
            if (currentButton == button)
            {
                currentButton = null;
            }
        }
    }

}
