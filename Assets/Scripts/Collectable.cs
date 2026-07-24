using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] int value = 100;
    [SerializeField] Sprite[] peixeSprites;
    [SerializeField] Sprite[] ossoSprites;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D collider2d;
    [SerializeField] AudioClip[] sonsColetar;

    [Header("Floating Score")]
    [SerializeField] private FloatingScore floatingScorePrefab;
    [SerializeField] private float verticalOffset = 0.8f; // Ajuste conforme necessário

    bool podeColidir = true;

    public void Setup()
    {
        AtualizarSprite(0);
    }

    void AtualizarSprite(int sequencia)
    {
        int seq = Mathf.Clamp(sequencia, 0, 3);
        
        switch (GameManager.Instance.IdPersonagemAtual)
        {
            case 0:
                spriteRenderer.sprite = peixeSprites[seq];
                break;
            case 1:
                spriteRenderer.sprite = ossoSprites[seq];
                break;
            default:
                spriteRenderer.sprite = peixeSprites[seq];
                break;
        }
    }

    public void Habilitar()
    {
        spriteRenderer.enabled = true;
        collider2d.enabled = true;
        podeColidir = true;
        AtualizarSprite(GameManager.Instance.SequenciaAtual);
    }

    public void Desabilitar()
    {
        spriteRenderer.enabled = false;
        collider2d.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!podeColidir || !collision.gameObject.CompareTag("Player"))
            return;

        podeColidir = false;

        // Toca o som
        int sequencia = Mathf.Clamp(GameManager.Instance.SequenciaAtual, 0, 3);
        SFXManager.Instance.PlaySound(sonsColetar[sequencia], 1f, false);

        // Aumenta a pontuação e recebe o valor REAL adicionado (com multiplicador)
        int pontosAdicionados = GameManager.Instance.IncreaseScore(value);

        // Cria o Floating Score com o valor correto
        if (floatingScorePrefab != null)
        {
            Vector3 spawnPos = transform.position + Vector3.up * verticalOffset;
            
            Camera mainCam = Camera.main;
            Transform canvasParent = GameObject.Find("FloatingScoreCanvas")?.transform;

            if (canvasParent != null && mainCam != null)
            {
                FloatingScore fs = Instantiate(floatingScorePrefab);
                fs.Setup(pontosAdicionados, spawnPos, mainCam, canvasParent);
            }
            else
            {
                Debug.LogWarning("FloatingScoreCanvas ou Camera não encontrado!");
            }
        }

        // Chama o próximo coletável
        LevelManager.Instance.HabilitarAleatorio();
    }
}