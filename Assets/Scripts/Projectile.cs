using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]Rigidbody2D rigidbody2d;
    [SerializeField]float lifeTime = 5f;

    void Start()
    {
        if(lifeTime <= 0)
        {
            return;
        }
        Destroy(gameObject, lifeTime);
    }

    public void Launch(float speed, Vector2 direcion)
    {
        rigidbody2d.linearVelocity = direcion*speed;
        
        transform.right = rigidbody2d.linearVelocity.normalized;
        // transform.LookAt(transform.position + 10*(Vector3)direcion);
        // transform.rotation = Quaternion.LookRotation(transform.position + 10*(Vector3)direcion);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject, 0.2f);
        }
    }
}
