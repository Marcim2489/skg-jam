using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public int Score {get; private set;}
    public int ScoredNow {get; private set;}
    public int Aliquota => aliquota;
    public int CiclesToGo => cenarios.Count - cenariosPercorridos;
    public event UnityAction levelStarted = delegate {};
    public event UnityAction<int> scoreIncreased = delegate {};
    public event UnityAction<int> scoreChanged = delegate {};

    List<string> cenarios = new List<string>(4){"Cenario 1","Cenario 2","Cenario 3","Cenario 4"};
    int coletadosNoCenarioAtual = 0;
    int cenariosPercorridos = 0;
    int aliquota = 5000;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartRun()
    {
        for (int i = 0; i < cenarios.Count; i++)
        {
            int randomIndex = Random.Range(0, cenarios.Count);
            string temp = cenarios[i];
            cenarios[i] = cenarios[randomIndex];
            cenarios[randomIndex] = temp;
        }
        Score = 0;
        StartNewLevel();
    }

    public void StartNewLevel()
    {
        ScoredNow = 0;
        coletadosNoCenarioAtual = 0;
        if (cenariosPercorridos >= cenarios.Count)
        {
            if (Score < aliquota)
            {
                LoseGame();
                return;
            }
            aliquota += 1000;
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
        SceneManager.LoadScene(cenarios[cenariosPercorridos]);
        cenariosPercorridos++;
        levelStarted.Invoke();
    }

    public void IncreaseScore(int amount)
    {
        int totalIncrease = Mathf.RoundToInt(amount * (1 + 0.1f*coletadosNoCenarioAtual));
        Score += totalIncrease;
        ScoredNow += totalIncrease;
        coletadosNoCenarioAtual++;
        scoreIncreased.Invoke(totalIncrease);
        scoreChanged.Invoke(Score);
    }

    public void LoseGame()
    {
        coletadosNoCenarioAtual = 0;
        cenariosPercorridos = 0;
        ScoredNow = 0;
        SceneManager.LoadScene("Game over");
    }
}
