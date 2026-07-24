using UnityEngine;

public class Collectable: MonoBehaviour
{
    [SerializeField]int value = 100;
    [SerializeField]Sprite[] peixeSprites;
    [SerializeField]Sprite[] ossoSprites;
    [SerializeField]SpriteRenderer spriteRenderer;
    [SerializeField]Collider2D collider2d;
    [SerializeField]AudioClip[] sonsColetar;
    bool podeColidir = true;

    public void Setup()
    {
        // GameManager.Instance.coletadosAtualMudou+=AtualizarSprite;
        AtualizarSprite(0);
    }

    void OnDestroy()
    {
        // GameManager.Instance.coletadosAtualMudou-=AtualizarSprite;
    }

    void AtualizarSprite(int sequencia)
    {
        switch (GameManager.Instance.IdPersonagemAtual)
        {
            case 0:
                if (sequencia > 3)
                {
                    sequencia = 3;
                }else if (sequencia < 0)
                {
                    sequencia = 0;
                }
                spriteRenderer.sprite = peixeSprites[sequencia];
                break;
            case 1:
                if (sequencia > 3)
                {
                    sequencia = 3;
                }else if (sequencia < 0)
                {
                    sequencia = 0;
                }
                spriteRenderer.sprite = ossoSprites[sequencia];
                break;
            default:
                if (sequencia > 3)
                {
                    sequencia = 3;
                }else if (sequencia < 0)
                {
                    sequencia = 0;
                }
                spriteRenderer.sprite = peixeSprites[sequencia];
                break;
        }
    }

    public void Habilitar()
    {
        // gameObject.SetActive(true);
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
        if(podeColidir == false)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            int sequencia = GameManager.Instance.SequenciaAtual;
            if (sequencia > 3)
            {
                sequencia = 3;
            }else if (sequencia < 0)
            {
                sequencia = 0;
            }
            SFXManager.Instance.PlaySound(sonsColetar[sequencia], 1f, false);
            // Debug.Log("a");
            podeColidir = false;
            GameManager.Instance.IncreaseScore(value);
            // gameObject.SetActive(false);
            LevelManager.Instance.HabilitarAleatorio();
        }
    }
}
