using UnityEngine;

public class LaserInstance : MonoBehaviour
{
    [SerializeField]SpriteRenderer spriteRenderer;
    [SerializeField]BoxCollider2D boxCollider;

    public void ManageSize(float size)
    {
        Vector2 newSize = new Vector2(0.6252f, size);
        spriteRenderer.size = newSize;
        boxCollider.size = newSize;
        boxCollider.offset = new Vector2(boxCollider.offset.x, -boxCollider.size.y*0.5f);
    }
}
