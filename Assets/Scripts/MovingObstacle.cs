using System.Collections;
using UnityEngine;

public class MovingObstacle : Obstacle
{
    [SerializeField]protected Rigidbody2D rigidbody2d;
    [SerializeField]protected float velocidade = 8;
    [SerializeField]protected Vector2 initialDirection;
    [SerializeField]protected float lifeTime = 5f;

    protected virtual void Start()
    {
        rigidbody2d.linearVelocity = initialDirection.normalized * velocidade;
        if(lifeTime > 0)
        {
            StartCoroutine(AutoDestroy(lifeTime));
        }
    }

    protected IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
