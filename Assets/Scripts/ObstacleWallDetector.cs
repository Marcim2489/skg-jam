using UnityEngine;
using UnityEngine.Events;

public class ObstacleWallDetector : MonoBehaviour
{
    public event UnityAction<Vector2> hitWall = delegate {};

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            hitWall.Invoke(collision.GetContact(0).normal);
        }
    }

}
