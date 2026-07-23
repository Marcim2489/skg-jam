using UnityEngine;

public class GhostEffects : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Flutuação")]
    [SerializeField] private float floatAmplitude = 0.08f;
    [SerializeField] private float floatSpeed = 2f;

    [Header("Inclinação")]
    [SerializeField] private float maxTiltAngle = 12f;
    [SerializeField] private float tiltSmooth = 8f;

    private Vector3 initialLocalPosition;
    private float randomOffset;

    void Start()
    {
        initialLocalPosition = transform.localPosition;
        randomOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void LateUpdate()
    {
        if (rb == null)
            return;

        FloatEffect();
        TiltEffect();
        FlipSprite();
    }

    private void FloatEffect()
    {
        float yOffset = Mathf.Sin((Time.time + randomOffset) * floatSpeed) * floatAmplitude;

        transform.localPosition = initialLocalPosition + Vector3.up * yOffset;
    }

    private void TiltEffect()
    {
        float speedX = rb.linearVelocity.x;

        float targetAngle = Mathf.Clamp(-speedX * maxTiltAngle, -maxTiltAngle, maxTiltAngle);

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * tiltSmooth
        );
    }

    private void FlipSprite()
    {
        if (spriteRenderer == null)
            return;

        if (rb.linearVelocity.x > 0.05f)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.linearVelocity.x < -0.05f)
        {
            spriteRenderer.flipX = true;
        }
    }
}