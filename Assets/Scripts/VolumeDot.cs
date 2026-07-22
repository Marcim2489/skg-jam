using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VolumeDot : MonoBehaviour
{
    [SerializeField] private Sprite filledSprite;
    [SerializeField] private Sprite emptySprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetState(bool active)
    {
        spriteRenderer.sprite = active ? filledSprite : emptySprite;
    }
}