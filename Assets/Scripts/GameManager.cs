using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public int Score {get; private set;}
    public int ScoredNow => Score - scoreAtTheStart;
    // public int Aliquota => aliquota;
    // public int CiclesToGo => cenarios.Count - cenariosPercorridos;
    public event UnityAction levelStarted = delegate {};
    public event UnityAction<int> scoreIncreased = delegate {};
    public event UnityAction<int> scoreDecreased = delegate {};
    public event UnityAction<int> scoreChanged = delegate {};
    public event UnityAction<int> coletadosAtualMudou = delegate {};

    // List<string> cenarios = new List<string>(4){"Cenario 1","Cenario 2","Cenario 3","Cenario 4"};

    string[] florestasTier1 = {"Floresta 1-1", "Floresta 1-2", "Floresta 1-3", "Floresta 1-4", "Floresta 1-5"};
    string[] florestasTier2 = {"Floresta 1-1", "Floresta 1-2", "Floresta 1-3", "Floresta 1-4", "Floresta 1-5"};
    string[] florestasTier3 = {"Floresta 1-1", "Floresta 1-2", "Floresta 1-3", "Floresta 1-4", "Floresta 1-5"};

    string[] fabricasTier1 = {"Floresta 1-1", "Floresta 1-2", "Floresta 1-3", "Floresta 1-4", "Floresta 1-5"};
    string[] fabricasTier2 = {"Floresta 1-1", "Floresta 1-2", "Floresta 1-3", "Floresta 1-4", "Floresta 1-5"};
    string[] fabricasTier3 = {"Floresta 1-1", "Floresta 1-2", "Floresta 1-3", "Floresta 1-4", "Floresta 1-5"};

    string[] luasTier1 = {"Floresta 1-1", "Floresta 1-2", "Floresta 1-3", "Floresta 1-4", "Floresta 1-5"};
    string[] luasTier2 = {"Floresta 1-1", "Floresta 1-2", "Floresta 1-3", "Floresta 1-4", "Floresta 1-5"};
    string[] luasTier3 = {"Floresta 1-1", "Floresta 1-2", "Floresta 1-3", "Floresta 1-4", "Floresta 1-5"};

    int sequenciaAtual = 0;
    int cenariosPercorridos = 0;
    // int aliquota = 5000;
    public bool BrokeRecord {get; private set;}
    int scoreAtTheStart;

    [SerializeField]AudioSource musicaGato;
    [SerializeField]AudioSource musicaCachorro;
    [SerializeField]AudioSource musicaDerrotaGato;
    [SerializeField]AudioSource musicaDerrotaCachorro;
    [SerializeField]AudioClip deathSound;

    string[] personagens = {
        "Puffles", //gato - 0
        "Cachorro" // - 1
        };

    public int IdPersonagemAtual {get; private set;}
    public event UnityAction<int> trocouPersonagem = delegate{};
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
            // foreach (string p in recordes.Keys)
            // {
            //     Debug.Log($"{p} -- {recordes[p]}");
            // }
            IdPersonagemAtual = 0;
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

    public void StartRun()
    {
        // for (int i = 0; i < cenarios.Count; i++)
        // {
            
        //     int randomIndex = Random.Range(0, cenarios.Count);
        //     string temp = cenarios[i];
        //     cenarios[i] = cenarios[randomIndex];
        //     cenarios[randomIndex] = temp;
        // }
        // IdPersonagemAtual = idPersonagem;
        Score = 0;
        scoreAtTheStart = 0;
        // aliquota = 5000;
        BrokeRecord = false;
        StartNewLevel();
        StartCoroutine(WaitToPlayMusic(1));
    }

    string PegarAleatorio(string[] strings)
    {
        // if (strings.Length <= 1)
        // {
        //     return strings[0];
        // }
        int randomIndex = Random.Range(0, strings.Length);
        return strings[randomIndex];
    }


    public void StartNewLevel()
    {
        cenariosPercorridos++;
        string faseEscolhida = "";
        int resto = cenariosPercorridos % 3;
        int tier = cenariosPercorridos/3;
        if (tier < 1)
        {
            tier = 1;
        }else if (tier > 3)
        {
            tier = 3;
        }

        if (resto == 1)
        {
            if (tier == 1)
            {
                faseEscolhida = PegarAleatorio(florestasTier1);
            }
            if (tier == 2)
            {
                faseEscolhida = PegarAleatorio(florestasTier2);
            }
            if (tier == 3)
            {
                faseEscolhida = PegarAleatorio(florestasTier3);
            }
        }
        else if (resto == 2)
        {
            if (tier == 1)
            {
                faseEscolhida = PegarAleatorio(fabricasTier1);
            }
            if (tier == 2)
            {
                faseEscolhida = PegarAleatorio(fabricasTier2);
            }
            if (tier == 3)
            {
                faseEscolhida = PegarAleatorio(fabricasTier3);
            }
        }
        else
        {
            if (tier == 1)
            {
                faseEscolhida = PegarAleatorio(luasTier1);
            }
            if (tier == 2)
            {
                faseEscolhida = PegarAleatorio(luasTier2);
            }
            if (tier == 3)
            {
                faseEscolhida = PegarAleatorio(luasTier3);
            }
        }
        if (faseEscolhida == "")
        {
            Debug.Log("Deu errado");
            faseEscolhida = "Floresta 1-1";
        }
        // Debug.Log($"cenário {resto} -- tier {tier}");

        sequenciaAtual = 0;
        scoreAtTheStart = Score;
        SceneTransitionManager.Instance.ChangeScene(faseEscolhida);

        levelStarted.Invoke();
    }

    IEnumerator WaitToPlayMusic(int musicId)
    {
        StopAllMusic();
        yield return new WaitForSeconds(0.2f);
        StopAllMusic();
        if (musicId == 1)
        {
            if (IdPersonagemAtual == 0)
            {
                musicaGato.Play();
            }else
            {
                musicaCachorro.Play();
            }
            
        }else if (musicId == 2)
        {
            if (IdPersonagemAtual == 0)
            {
                musicaDerrotaGato.Play();
            }else
            {
                musicaDerrotaCachorro.Play();
            }
        }
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
        if (sequenciaAtual <= 4)
        {
            totalIncrease = amount * sequenciaAtual;
        }else
        {
            totalIncrease = amount * 5 + Mathf.RoundToInt(amount*5 * (0.1f*(sequenciaAtual-4)));
        }
        Score += totalIncrease;
        // Debug.Log(sequenciaAtual);
        coletadosAtualMudou.Invoke(sequenciaAtual);
        scoreIncreased.Invoke(totalIncrease);
        scoreChanged.Invoke(Score);
    }

    public void EndRun()
    {
        SFXManager.Instance.PlaySound(deathSound, 1f, true);
        BrokeRecord = SaveRecord(NomePersonagemAtual, Score);
        sequenciaAtual = 0;
        cenariosPercorridos = 0;
        // Score = 0;
        foreach (string p in recordes.Keys)
        {
            Debug.Log($"{p} -- {recordes[p]}");
        }
        // SceneManager.LoadScene("Game over");
        SceneTransitionManager.Instance.ChangeScene("Game over");
        StartCoroutine(WaitToPlayMusic(2));
    }

    public void StopAllMusic()
    {
        musicaGato.Stop();
        musicaCachorro.Stop();
        musicaDerrotaGato.Stop();
        musicaDerrotaCachorro.Stop();
    }

    public void PlayMusic(int musicId)
    {
        StopAllMusic();
        if (musicId == 1)
        {
            if (IdPersonagemAtual == 0)
            {
                musicaGato.Play();
            }else
            {
                musicaCachorro.Play();
            }
            
        }else if (musicId == 2)
        {
            if (IdPersonagemAtual == 0)
            {
                musicaDerrotaGato.Play();
            }else
            {
                musicaDerrotaCachorro.Play();
            }
        }
    }

    public void SwitchCharacter(int charId)
    {
        IdPersonagemAtual = charId;
        trocouPersonagem.Invoke(IdPersonagemAtual);
    }
}

