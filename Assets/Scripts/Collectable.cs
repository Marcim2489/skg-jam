using UnityEngine;

public class Collectable: MonoBehaviour
{
    [SerializeField]int value = 100;
    [SerializeField]Sprite[] peixeSprites;
    [SerializeField]Sprite[] ossoSprites;
    [SerializeField]SpriteRenderer spriteRenderer;

    public void Setup()
    {
        GameManager.Instance.coletadosAtualMudou+=AtualizarSprite;
        AtualizarSprite(0);
    }

    void OnDestroy()
    {
        GameManager.Instance.coletadosAtualMudou-=AtualizarSprite;
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
        gameObject.SetActive(true);
        AtualizarSprite(GameManager.Instance.SequenciaAtual);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.IncreaseScore(value);
            LevelManager.Instance.HabilitarAleatorio();
        }
    }
}
