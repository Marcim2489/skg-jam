using UnityEngine;
using TMPro;
// using Unity.Services.Leaderboards.Models;
using UnityEngine.UI;
using Unity.Services.Leaderboards;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System;
using UnityEngine.SceneManagement;
// using 



// public override void Initialize()
// {

// }
[System.Serializable]
public class ScoreMetadata
{
    public string Nome;
    public int CharId;

    // public ScoreMetadata(string n, int id)
    // {
    //     Name = n;
    //     CharId = id;
    // }
}

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField]LeaderboardItem[] entries;
    [SerializeField]TextMeshProUGUI scoreDisplay;
    [SerializeField]Button adderButton;
    [SerializeField]TMP_InputField inputField;
    int selectedCharacter = 0;

    const string LEADERBOARD_ID = "snackHunters";


    // async void Awake()
    // {
    //     try
    //     {
    //         // Initializes all installed Unity Gaming Services
    //         await UnityServices.InitializeAsync();
    //         Debug.Log("Unity Services successfully initialized!");
            
    //         // Proceed to authenticate or load your game features here
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogError($"Unity Services failed to initialize: {e.Message}");
    //     }
    // }

 private async void Start()
    {
        try
        {
            // 1. Initialize the core Unity Services container
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services Initialized Successfully.");

            // 2. Sign in the player (Anonymous authentication example)
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log($"Player signed in anonymously: {AuthenticationService.Instance.PlayerId}");
            }

            // 3. Now it is completely safe to use the Leaderboard Service
            // LoadLeaderboardScores();
            LoadScores();
        // GetLeaderboard();
            selectedCharacter = GameManager.Instance.IdPersonagemAtual;
            DisplayScore(selectedCharacter);
        }
        catch (Exception e)
        {
            Debug.LogError($"Initialization failed: {e.Message}");
        }
    }


    // void Start()
    // {
    //     LoadScores();
    //     // GetLeaderboard();
    //     selectedCharacter = GameManager.Instance.IdPersonagemAtual;
    //     DisplayScore(selectedCharacter);
    // }


    public async void AddScoreAsync()
    {
        string username = inputField.text;
        if (username.Equals("") || username == null)
        {
            return;
        }
        if (username.Length > 10)
        {
            username = username.Substring(0, 10);
        }
        
        int score = GameManager.Instance.GetRecord(selectedCharacter);
        adderButton.interactable = false;
        // AddPlayerScoreOptions options = new AddPlayerScoreOptions();
        //     options.Metadata = new ScoreMetadata()
        //     {
        //         Name = username,
        //         CharId = selectedCharacter
        //     };
        await LeaderboardsService.Instance.AddPlayerScoreAsync(LEADERBOARD_ID, score, new AddPlayerScoreOptions()
        {
            Metadata = new ScoreMetadata()
            {
                Nome = username,
                CharId = selectedCharacter
            }
        });
        LoadScores();
        // try
        // {
        //     AddPlayerScoreOptions options = new AddPlayerScoreOptions();
        //     options.Metadata = new ScoreMetadata(username, selectedCharacter);
        //     var entry = await LeaderboardsService.Instance.AddPlayerScoreAsync(LEADERBOARD_ID, score, options);
        //     LoadScores();
        //     // LeaderboardEntry
        // }
        // catch
        // {
        //     Debug.Log("se lascou peão");
        // }
        

        adderButton.interactable = true;
    }

    public async void LoadScores()
    {
            GetScoresOptions options = new GetScoresOptions();
            options.Limit = 10;
            options.IncludeMetadata = true;
            var scores = await LeaderboardsService.Instance.GetScoresAsync(LEADERBOARD_ID, options);
            for (int i = 0; i  < entries.Length; i++)
            {
                if(i < scores.Results.Count)
                {
                    Debug.Log(scores.Results[i].Metadata);
                    ScoreMetadata metadata = JsonUtility.FromJson<ScoreMetadata>(scores.Results[i].Metadata);
                    Debug.Log($"nome: {metadata.Nome} | id: {metadata.CharId} ");
                    entries[i].gameObject.SetActive(true);
                    entries[i].UpdateDisplay(metadata.Nome, scores.Results[i].Score.ToString(), metadata.CharId);
                    // LeaderboardEntry
                }
                else
                {
                    entries[i].gameObject.SetActive(false);
                }
            }
        // try
        // {
        //     GetScoresOptions options = new GetScoresOptions();
        //     options.Limit = 10;
        //     var scores = await LeaderboardsService.Instance.GetScoresAsync(LEADERBOARD_ID, options);
        //     for (int i = 0; i  < entries.Length; i++)
        //     {
        //         if(i < scores.Results.Count)
        //         {
        //             ScoreMetadata metadata = JsonUtility.FromJson<ScoreMetadata>(scores.Results[i].Metadata);
        //             entries[i].gameObject.SetActive(true);
        //             entries[i].UpdateDisplay(metadata.name, scores.Results[i].Score.ToString(), metadata.charId);
        //             // LeaderboardEntry
        //         }
        //         else
        //         {
        //             entries[i].gameObject.SetActive(false);
        //         }
        //     }
        // }
        // catch (Exception ex)
        // {
        //     Debug.Log(ex.Message);
        //     for (int i = 0; i  < entries.Length; i++)
        //     {
        //         // if(i < scores.Results.Count)
        //         // {
        //         //     ScoreMetadata metadata = JsonUtility.FromJson<ScoreMetadata>(scores.Results[i].Metadata);
        //         //     entries[i].gameObject.SetActive(true);
        //         //     entries[i].UpdateDisplay(metadata.name, scores.Results[i].Score.ToString(), metadata.charId);
        //         //     // LeaderboardEntry
        //         // }
        //         // else
        //         // {
        //         entries[i].gameObject.SetActive(false);
        //         // }
        //     }
        // }
    }

    // public void GetLeaderboard()
    // {
    //     // LeaderboardCreator.GetLeaderboard(PUBLIC_KEY, ((msg) =>
    //     // {
    //     //     for (int i = 0; i  < entries.Length; i++)
    //     //     {
    //     //         if(i < msg.Length)
    //     //         {
    //     //             entries[i].gameObject.SetActive(true);
    //     //             entries[i].UpdateDisplay(msg[i].Username, msg[i].Score.ToString(), int.Parse(msg[i].Extra));
    //     //         }
    //     //         else
    //     //         {
    //     //             entries[i].gameObject.SetActive(false);
    //     //         }
                
    //     //     }
    //     // }));
    // }

    // public void SetLeaderboardEntry(string username, int score, int charId)
    // {
    //     // LeaderboardCreator.UploadNewEntry(PUBLIC_KEY, username, score, charId.ToString(), ((msg) =>
    //     // {
    //     //     GetLeaderboard();
    //     // }));
    // }


    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void DisplayScore(int charId)
    {
        selectedCharacter = charId;
        scoreDisplay.text = GameManager.Instance.GetRecord(selectedCharacter).ToString();
    }
}
