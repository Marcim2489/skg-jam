using UnityEngine;

public class StarBlink : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float speed = 2f;

    [SerializeField] [Range(0f,1f)]
    private float minAlpha = 0.4f;

    private Color color;
    private float offset;

    void Start()
    {
        color = spriteRenderer.color;

        // Cada estrela começa em um momento diferente
        offset = Random.Range(0f, 100f);
    }

    void Update()
    {
        float alpha = Mathf.Lerp(
            minAlpha,
            1f,
            (Mathf.Sin((Time.time + offset) * speed) + 1f) * 0.5f
        );

        color.a = alpha;
        spriteRenderer.color = color;
    }
}