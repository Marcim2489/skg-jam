using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {get; private set;}
    float timeLeft = 15;
    Collectable[] coletaveis;
    Collectable ultimoEscolhido;
    public event UnityAction timeUp = delegate {};

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
        foreach(Collectable coletavel in coletaveis)
        {
            coletavel.Setup();
        }
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
            GameManager.Instance.StartNewLevel();
            // CanvasManager.Instance.DisplayEndPanel();
            
            ended = true;
        }
    }

    public void StopTimer()
    {
        ended=true;
    }

    public void HabilitarAleatorio()
    {
        foreach(Collectable coletavel in coletaveis)
        {
            // if (coletavel.gameObject.activeInHierarchy)
            // {
            //     coletavel.gameObject.SetActive(false);
            // }
            coletavel.Desabilitar();
        }
        List<Collectable>provisorio = coletaveis.ToList();
        if (ultimoEscolhido != null && provisorio.Contains(ultimoEscolhido) && provisorio.Count > 1)
        {
            provisorio.Remove(ultimoEscolhido);
        }
        Collectable escolhido = provisorio[Random.Range(0, provisorio.Count)];
        escolhido.Habilitar();
        ultimoEscolhido = escolhido;
    }

}
