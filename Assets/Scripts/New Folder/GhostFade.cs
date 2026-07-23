using UnityEngine;

public class GhostFade : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float lifeTime = 0.4f;

    [SerializeField] private AnimationCurve alphaCurve;

    private float timer;

    public void SetSprite(Sprite sprite, bool flipX)
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.flipX = flipX;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float t = timer / lifeTime;

        Color color = spriteRenderer.color;
        color.a = alphaCurve.Evaluate(1 - t);

        spriteRenderer.color = color;

        if (timer >= lifeTime)
            Destroy(gameObject);
    }
}