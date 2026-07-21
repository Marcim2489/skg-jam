using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]GameObject newRecordText;
    [SerializeField]TextMeshProUGUI obtainedScoreDisplay;
    // [SerializeField]TextMeshProUGUI aliquotaFinalDisplay;

    void Start()
    {
        obtainedScoreDisplay.text = $"Final score: {GameManager.Instance.Score}";
        // aliquotaFinalDisplay.text = $"Final goal: {GameManager.Instance.Aliquota}";
        newRecordText.SetActive(GameManager.Instance.BrokeRecord);
    }

    public void Retry()
    {
        GameManager.Instance.StartRun(0);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
