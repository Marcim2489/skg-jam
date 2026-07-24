using UnityEngine;

public class BulletGlow : MonoBehaviour
{
    [SerializeField] private Transform glow;
    [SerializeField] private float pulseSpeed = 8f;
    [SerializeField] private float pulseAmount = 0.08f;

    private Vector3 initialScale;

    private void Start()
    {
        initialScale = glow.localScale;
    }

    private void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        glow.localScale = initialScale * scale;
    }
}