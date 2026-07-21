using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public int Score {get; private set;}
    public int ScoredNow => Score - scoreAtTheStart;
    // public int Aliquota => aliquota;
    public int CiclesToGo => cenarios.Count - cenariosPercorridos;
    public event UnityAction levelStarted = delegate {};
    public event UnityAction<int> scoreIncreased = delegate {};
    public event UnityAction<int> scoreDecreased = delegate {};
    public event UnityAction<int> scoreChanged = delegate {};
    public event UnityAction<int> coletadosAtualMudou = delegate {};

    List<string> cenarios = new List<string>(4){"Cenario 1","Cenario 2","Cenario 3","Cenario 4"};
    int sequenciaAtual = 0;
    int cenariosPercorridos = 0;
    // int aliquota = 5000;
    public bool BrokeRecord {get; private set;}
    int scoreAtTheStart;


    string[] personagens = {
        "Puffles", //gato - 0
        "Cachorro" // - 1
        };

    public int IdPersonagemAtual {get; private set;}
    public string NomePersonagemAtual => personagens[IdPersonagemAtual];
    public int SequenciaAtual => sequenciaAtual;

    Dictionary<string, int> recordes = new Dictionary<string, int>(2);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            for (int i = 0; i < personagens.Length; i++)
            {
                recordes[personagens[i]] = GetRecord(i);
            }
            // foreach (string personagem in personagens)
            // {
            //     recordes[personagem] = GetRecord(personagem);
            // }
            foreach (string p in recordes.Keys)
            {
                Debug.Log($"{p} -- {recordes[p]}");
            }
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool SaveRecord(string personagem, int score)
    {
        if (recordes.ContainsKey(personagem) == false || score > recordes[personagem])
        {
            recordes[personagem] = score;
            PlayerPrefs.SetInt($"Recorde_{personagem}", score);
            return true;
        }
        return false;
    }

    public int GetRecord(int idPersonagem)
    {
        return PlayerPrefs.GetInt($"Recorde_{personagens[idPersonagem]}");
    }

    public void StartRun(int idPersonagem)
    {
        for (int i = 0; i < cenarios.Count; i++)
        {
            int randomIndex = Random.Range(0, cenarios.Count);
            string temp = cenarios[i];
            cenarios[i] = cenarios[randomIndex];
            cenarios[randomIndex] = temp;
        }
        IdPersonagemAtual = idPersonagem;
        Score = 0;
        scoreAtTheStart = 0;
        // aliquota = 5000;
        BrokeRecord = false;
        StartNewLevel();
    }

    public void StartNewLevel()
    {
        if (cenariosPercorridos >= cenarios.Count)
        {
            // if (Score < aliquota)
            // {
            //     EndRun();
            //     return;
            // }
            // aliquota += 1000;
            cenariosPercorridos = 0;
            int rid = Random.Range(0, cenarios.Count-1);
            string tip = cenarios[0];
            cenarios[0] = cenarios[rid];
            cenarios[rid] = tip;
            for (int i = 1; i < cenarios.Count; i++)
            {
                int randomIndex = Random.Range(1, cenarios.Count);
                string temp = cenarios[i];
                cenarios[i] = cenarios[randomIndex];
                cenarios[randomIndex] = temp;
            }
        }
        sequenciaAtual = 0;
        scoreAtTheStart = Score;
        SceneManager.LoadScene(cenarios[cenariosPercorridos]);
        cenariosPercorridos++;
        levelStarted.Invoke();
    }

    public void DecreaseScore(int amount)
    {
        int prevScore = Score;
        Score -= amount;
        if (Score < 0)
        {
            Score = 0;
        }
        sequenciaAtual = 0;
        coletadosAtualMudou.Invoke(sequenciaAtual);
        int totalDecrease = prevScore - Score;
        scoreDecreased.Invoke(totalDecrease);
        scoreChanged.Invoke(Score);
    }

    public void IncreaseScore(int amount)
    {
        sequenciaAtual++;
        int totalIncrease;
        if (sequenciaAtual <= 3)
        {
            totalIncrease = amount;
        }else if (sequenciaAtual <= 6)
        {
            totalIncrease = amount * 5;
        }else
        {
            totalIncrease = amount * 5 + Mathf.RoundToInt(amount*5 * (0.1f*(sequenciaAtual-3)));
        }
        Score += totalIncrease;
        
        coletadosAtualMudou.Invoke(sequenciaAtual);
        scoreIncreased.Invoke(totalIncrease);
        scoreChanged.Invoke(Score);
    }

    public void EndRun()
    {
        BrokeRecord = SaveRecord(NomePersonagemAtual, Score);
        sequenciaAtual = 0;
        cenariosPercorridos = 0;
        // Score = 0;
        foreach (string p in recordes.Keys)
        {
            Debug.Log($"{p} -- {recordes[p]}");
        }
        SceneManager.LoadScene("Game over");
    }
}
