using UnityEngine;

public class TimeBar : MonoBehaviour
{
    [SerializeField]RectTransform bar;
    float initialWidth;
    float initialTime;

    void Start()
    {
        initialWidth = bar.rect.width;
        initialTime = LevelManager.Instance.TimeLeft;
    }

    void Update()
    {
        bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, initialWidth * (LevelManager.Instance.TimeLeft/initialTime));
    }
}
