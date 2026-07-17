using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI displayText;

    void Start()
    {
        GameManager.Instance.scoreChanged += DisplayScore;
        displayText.text = $"Score: {GameManager.Instance.Score}";
    }

    void OnDisable()
    {
        GameManager.Instance.scoreChanged -= DisplayScore;
    }

    void DisplayScore(int currentScore)
    {
        displayText.text = $"Score: {currentScore}";
    }
}
