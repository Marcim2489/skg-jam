using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {get; private set;}
    [SerializeField]float timeLeft = 5;
    Collectable[] coletaveis;
    int ultimoEscolhido = -1;

    public float TimeLeft => timeLeft;
    bool ended = false;
    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    void Start()
    {
        coletaveis = FindObjectsByType<Collectable>(0);
        HabilitarAleatorio();
    }

    void Update()
    {
        if (ended)
        {
            return;
        }
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            // GameManager.Instance.StartNewLevel();
            CanvasManager.Instance.DisplayEndPanel();
            ended = true;
        }
    }

    public void HabilitarAleatorio()
    {
        foreach(Collectable coletavel in coletaveis)
        {
            coletavel.gameObject.SetActive(false);
        }
        List<Collectable>provisorio = coletaveis.ToList();
        if (ultimoEscolhido >= 0 && ultimoEscolhido < provisorio.Count)
        {
            provisorio.RemoveAt(ultimoEscolhido);
        }
        int escolhido = Random.Range(0, provisorio.Count);
        provisorio[escolhido].gameObject.SetActive(true);
        ultimoEscolhido = escolhido;
    }

}
