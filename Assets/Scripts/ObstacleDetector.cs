using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleDetector : MonoBehaviour
{
    [SerializeField]float immunityTime = 1f;
    [SerializeField]Collider2D[] colliders;
    public event UnityAction ImmunityStarted = delegate {};
    public event UnityAction ImmunityEnded = delegate {};

    void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            GameManager.Instance.DecreaseScore(obstacle.ValueToDecrease);
            if (immunityTime > 0f)
            {
                StartCoroutine(ImmunityTimer());
            }
        }
    }

    void EnableColliders(bool enable)
    {
        foreach(Collider2D collider2D in colliders)
        {
            collider2D.enabled = enable;
        }
    }

    IEnumerator ImmunityTimer()
    {
        EnableColliders(false);
        ImmunityStarted.Invoke();
        yield return new WaitForSeconds(immunityTime);
        EnableColliders(true);
        ImmunityEnded.Invoke();
    }
}
