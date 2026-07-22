using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Leaderboards;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField]LeaderboardItem[] entries;
    [SerializeField]TextMeshProUGUI scoreDisplayGato;
    [SerializeField]TextMeshProUGUI scoreDisplayCachorro;
    [SerializeField]Button adderButton;
    [SerializeField]TMP_InputField inputField;

    const string LEADERBOARD_ID = "snackHunters";

    private async void Start()
    {
        adderButton.interactable = false;
        for (int i = 0; i  < entries.Length; i++)
        {
            entries[i].gameObject.SetActive(false);
        }
        DisplayScores();
        try
        {
            
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services Initialized Successfully.");
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log($"Player signed in anonymously: {AuthenticationService.Instance.PlayerId}");
            }
            LoadScores();
            // selectedCharacter = GameManager.Instance.IdPersonagemAtual;
            // DisplayScore(selectedCharacter);
        }
        catch (Exception e)
        {
            Debug.LogError($"Initialization failed: {e.Message}");
        }
        adderButton.interactable = true;
    }

    public async void AddScoreAsync()
    {
        adderButton.interactable = false;
        string username = inputField.text;
        if (username.Equals("") || username == null)
        {
            adderButton.interactable = true;
            return;
        }
        if (username.Length > 10)
        {
            username = username.Substring(0, 10);
        }
        inputField.text = "";
        int bestCharacter;
        int gatoRecord = GameManager.Instance.GetRecord(0);
        int cachorroRecord = GameManager.Instance.GetRecord(1);
        int score;
        if (gatoRecord > cachorroRecord)
        {
            bestCharacter = 0;
            score = GameManager.Instance.GetRecord(bestCharacter);
        }
        else if (gatoRecord < cachorroRecord)
        {
            bestCharacter = 1;
            score = GameManager.Instance.GetRecord(bestCharacter);
        }
        else
        {
            bestCharacter = 2;
            score = GameManager.Instance.GetRecord(0);
        }
        
        await LeaderboardsService.Instance.AddPlayerScoreAsync(LEADERBOARD_ID, score, new AddPlayerScoreOptions()
        {
            Metadata = new ScoreMetadata()
            {
                Nome = username,
                CharId = bestCharacter
            }
        });
        LoadScores();
        adderButton.interactable = true;
    }

    public async void LoadScores()
    {
        try
        {
            GetScoresOptions options = new GetScoresOptions();
            options.Limit = 10;
            options.IncludeMetadata = true;
            var scores = await LeaderboardsService.Instance.GetScoresAsync(LEADERBOARD_ID, options);
            for (int i = 0; i  < entries.Length; i++)
            {
                if(i < scores.Results.Count)
                {
                    // Debug.Log(scores.Results[i].Metadata);
                    ScoreMetadata metadata = JsonUtility.FromJson<ScoreMetadata>(scores.Results[i].Metadata);
                    // Debug.Log($"nome: {metadata.Nome} | id: {metadata.CharId} ");
                    entries[i].gameObject.SetActive(true);
                    entries[i].UpdateDisplay(metadata.Nome, scores.Results[i].Score.ToString(), metadata.CharId);
                }
                else
                {
                    entries[i].gameObject.SetActive(false);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            for (int i = 0; i  < entries.Length; i++)
            {
                entries[i].gameObject.SetActive(false);
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void DisplayScores()
    {
        scoreDisplayGato.text = GameManager.Instance.GetRecord(0).ToString();
        scoreDisplayCachorro.text = GameManager.Instance.GetRecord(1).ToString();
    }
}

[Serializable]
public class ScoreMetadata
{
    public string Nome;
    public int CharId;
}