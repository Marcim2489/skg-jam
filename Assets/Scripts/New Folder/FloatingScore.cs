using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(CanvasGroup))]
public class FloatingScore : MonoBehaviour
{
    [Header("Animação")]
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float moveSpeed = 85f;
    [SerializeField] private float popScale = 1.25f;
    [SerializeField] private float popDuration = 0.25f;

    [Header("Cores por Multiplicador")]
    [SerializeField] private Color baseColor = Color.white;
    [SerializeField] private Color x2Color = new Color(0.3f, 1f, 0.3f);     // Verde
    [SerializeField] private Color x3Color = new Color(1f, 0.9f, 0.2f);    // Amarelo
    [SerializeField] private Color x4Color = new Color(1f, 0.5f, 0.1f);    // Laranja
    [SerializeField] private Color highColor = new Color(1f, 0.2f, 0.6f);  // Rosa/Roxo

    private TextMeshProUGUI textMesh;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Setup(int score, Vector3 worldPosition, Camera cam, Transform parentCanvas)
    {
        // Posicionamento
        Vector3 screenPos = cam.WorldToScreenPoint(worldPosition);
        transform.SetParent(parentCanvas, false);
        GetComponent<RectTransform>().position = screenPos;
        startPosition = transform.position;

        // Texto
        textMesh.text = $"+{score}";
        
        // Cor conforme o multiplicador
        textMesh.color = GetColorByScore(score);

        canvasGroup.alpha = 1f;
        transform.localScale = Vector3.one * 0.6f; // Começa menor

        StartCoroutine(Animate());
    }

    private Color GetColorByScore(int score)
    {
        if (score >= 800) return highColor;
        if (score >= 400) return x4Color;
        if (score >= 300) return x3Color;
        if (score >= 200) return x2Color;
        return baseColor;
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;

        // === POP IN ===
        while (elapsed < popDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / popDuration;
            float scale = Mathf.Lerp(0.6f, popScale, progress);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }

        // === MOVIMENTO + FADE OUT ===
        elapsed = 0f;
        while (elapsed < lifeTime)
        {
            elapsed += Time.deltaTime;

            // Movimento para cima com leve curva
            float yOffset = moveSpeed * elapsed;
            float curve = Mathf.Sin(elapsed * 2f) * 8f; // leve ondulação
            transform.position = startPosition + new Vector3(curve, yOffset, 0);

            // Fade out
            if (elapsed > lifeTime * 0.6f)
            {
                float fadeProgress = (elapsed - lifeTime * 0.6f) / (lifeTime * 0.4f);
                canvasGroup.alpha = 1 - fadeProgress;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}