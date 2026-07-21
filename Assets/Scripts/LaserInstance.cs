using UnityEngine;

public class LaserInstance : MonoBehaviour
{
    [SerializeField]SpriteRenderer spriteRenderer;
    [SerializeField]BoxCollider2D boxCollider;

    public void ManageSize(float size)
    {
        Vector2 newSize = new Vector2(1, size);
        spriteRenderer.size = newSize;
        boxCollider.size = newSize;
    }
}
