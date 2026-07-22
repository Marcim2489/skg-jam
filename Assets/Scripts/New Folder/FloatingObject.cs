using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private float altura = 0.1f;
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private float rotacao = 3f;

    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        float movimento = Mathf.Sin(Time.time * velocidade) * altura;

        transform.position = posicaoInicial + new Vector3(0, movimento, 0);

        float giro = Mathf.Sin(Time.time * velocidade * 0.7f) * rotacao;

        transform.rotation = Quaternion.Euler(0, 0, giro);
    }
}