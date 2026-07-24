using UnityEngine;

public class SerraSpawner : MonoBehaviour
{
    [SerializeField]BouncingObstacle serra;
    [SerializeField]Transform[] posicoesPossiveis;
    [SerializeField]float minDirection = 0.2f;
    [SerializeField]float maxDirection = 1f;
    PlayerController player;

    void Start()
    {
        if (GameManager.Instance.Tier < 4)
        {
            return;
        }

        player = FindFirstObjectByType<PlayerController>();

        if (GameManager.Instance.Tier < 5)
        {
            SpawnarSerra(posicoesPossiveis[Random.Range(0,posicoesPossiveis.Length)].position);
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                SpawnarSerra(posicoesPossiveis[i].position);
            }
        }

        player = FindFirstObjectByType<PlayerController>();
    }

    void SpawnarSerra(Vector2 posicao)
    {
        BouncingObstacle s = Instantiate(serra);
        s.transform.position = posicao;
        Vector2 playerPosition = player.transform.position;
        Vector2 direction = Vector2.zero;

        if (posicao.x > playerPosition.x)
        {
            direction.x = Random.Range(minDirection, maxDirection);
        }else if (posicao.x < playerPosition.x)
        {
            direction.x = Random.Range(-minDirection, -maxDirection);
        }
        else
        {
            direction.x = Random.Range(-maxDirection, maxDirection);
        }

        if (posicao.y > playerPosition.y)
        {
            direction.y = Random.Range(minDirection, maxDirection);
        }else if (posicao.y < playerPosition.y)
        {
            direction.y = Random.Range(-minDirection, -maxDirection);
        }
        else
        {
            direction.y = Random.Range(-maxDirection, maxDirection);
        }

        if (direction == Vector2.zero)
        {
            direction = (posicao-playerPosition).normalized;
        }

        s.ChangeDirection(direction);
    }
}
