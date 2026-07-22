using UnityEngine;


using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
     [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private float selectedScale = 1.15f;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Select()
    {
        spriteRenderer.color = selectedColor;
        transform.localScale = originalScale * selectedScale;
    }

    public void Deselect()
    {
        spriteRenderer.color = normalColor;
        transform.localScale = originalScale;
    }

    public virtual void Press()
    {
      
    }
}