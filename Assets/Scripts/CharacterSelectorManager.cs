using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectorManager : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI[] recordTexts;

    void Start()
    {
        for (int i = 0; i < recordTexts.Length; i++)
        {
            recordTexts[i].text = $"Best score: {GameManager.Instance.GetRecord(i)}";
        }
    }

    public void Play(int charId)
    {
        GameManager.Instance.StartRun();
    }

    public void a()
    {
        SceneManager.LoadScene("Leaderboard");
    }

}
