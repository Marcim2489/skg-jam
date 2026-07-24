// using System.Collections;
using UnityEngine;
using UnityEngine.Events;
// using UnityEngine.Events;

public class ObstacleDetector : MonoBehaviour
{
    // [SerializeField]float immunityTime = 1f;
    // [SerializeField]Collider2D[] colliders;
    // public event UnityAction immunityStarted = delegate {};
    // public event UnityAction immunityEnded = delegate {};

    public event UnityAction died = delegate {};

    void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            // GameManager.Instance.DecreaseScore(obstacle.ValueToDecrease);
            // if (immunityTime > 0f)
            // {
            //     StartCoroutine(ImmunityTimer());
            // }
            // GameManager.Instance.EndRun();
            died.Invoke();
        }
    }

    // void EnableColliders(bool enable)
    // {
    //     foreach(Collider2D collider2D in colliders)
    //     {
    //         collider2D.enabled = enable;
    //     }
    // }

    // IEnumerator ImmunityTimer()
    // {
    //     EnableColliders(false);
    //     immunityStarted.Invoke();
    //     yield return new WaitForSeconds(immunityTime);
    //     EnableColliders(true);
    //     immunityEnded.Invoke();
    // }
}
