using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance {get; private set;}
    [SerializeField]GameObject scoreText;
    [SerializeField]TextMeshProUGUI timeText;
    [SerializeField]GameObject panel;
    // [SerializeField]TextMeshProUGUI timesUpText;
    [SerializeField]TextMeshProUGUI scoredNowText;
    [SerializeField]TextMeshProUGUI totalScoreText;
    // [SerializeField]TextMeshProUGUI goalText;
    [SerializeField]TextMeshProUGUI ciclesText;

    void Awake()
    {
        Instance = this;
    }

    void OnDisable()
    {
        Instance = null;
    }

    void Start()
    {
        panel.SetActive(false);
    }

    public void DisplayEndPanel()
    {
        Time.timeScale = 0;
        scoreText.SetActive(false);
        timeText.text = "0";
        scoredNowText.text = $"Scored now: {GameManager.Instance.ScoredNow}";
        totalScoreText.text = $"Total score: {GameManager.Instance.Score}";
        // goalText.text = $"Goal: {GameManager.Instance.Aliquota}";
        ciclesText.text = $"{GameManager.Instance.CiclesToGo}";
        panel.SetActive(true);
    }

    public void Proceed()
    {
        Time.timeScale = 1;
        GameManager.Instance.StartNewLevel();
    }

    public void GiveUp()
    {
        Time.timeScale = 1;
        GameManager.Instance.EndRun();
    }
}
