using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float velocidade = 50f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction interact;

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
    [SerializeField]float jumpPeakDuration = 0.1f;
    [SerializeField]float jumpHeight = 1f;
    [SerializeField]ObstacleDetector hurtbox;
    [SerializeField]bool onGameOver = false;
    private float dustTimer;
    // [SerializeField]ObstacleDetector obstacleDetector;
    MenuButton currentButton;

    Vector2 Direcao => movement.ReadValue<Vector2>();
    float VelocidadeTotal
    {
        get
        {
            bool pressed = interact.IsPressed();
            if (gatoSelecionado || pressed == false || onGameOver)
            {
                return velocidade;
            }
            return velocidade * 1.5f;
        }
    }


    bool pulando = false;
    bool gatoSelecionado = false;

    private void OnEnable()
    {
        movement.Enable();
        interact.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        interact.Disable();
        GameManager.Instance.trocouPersonagem-=SetCharacter;
    }

    void Start()
    {
        SetCharacter(GameManager.Instance.IdPersonagemAtual);
        GameManager.Instance.trocouPersonagem+=SetCharacter;
    }

    void Update()
    {
        // Movimento
        rb.linearVelocity = Direcao.normalized * VelocidadeTotal;

        // Animação
        bool moving = Direcao != Vector2.zero;
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

        if (interact.WasPressedThisFrame() && pulando==false)
        {
            if(currentButton != null)
            {
                currentButton.Press();
                return;
            }
            if (gatoSelecionado && pulando == false && onGameOver==false)
            {
                StartCoroutine(Jump());
                return;
            }
        }
    }

    IEnumerator Jump()
    {
        pulando = true;
        animator.Play("PlayerJump");
        hurtbox.gameObject.SetActive(false);
        while((spriteRendererGato.transform.position.y - transform.position.y) <= jumpHeight)
        {
            spriteRendererGato.transform.position += Vector3.up * jumpForce * Time.deltaTime;
            yield return null;
        }

        if(jumpPeakDuration > 0)
        {
            yield return new WaitForSeconds(jumpPeakDuration);
        }

        while((spriteRendererGato.transform.position.y - transform.position.y) >= 0f)
        {
            spriteRendererGato.transform.position -= Vector3.up * jumpForce * Time.deltaTime;
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

    public void SetCharacter(int charId)
    {
        if (charId == 0)
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
