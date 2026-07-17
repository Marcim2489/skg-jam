using UnityEngine;
using UnityEngine.Events;

public class Collectable: MonoBehaviour
{
    [SerializeField]int value = 100;
    public event UnityAction collected = delegate {};

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.IncreaseScore(value);
            collected.Invoke();
            // Destroy(gameObject);
        }
    }
}
