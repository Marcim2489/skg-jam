using UnityEngine;

public class BouncingObstacle : Obstacle
{
    [SerializeField]Rigidbody2D rigidbody2d;
    [SerializeField]ObstacleWallDetector wallDetector;
    [SerializeField]float velocidade = 8;
    [SerializeField]Vector2 initialDirection;

    void Start()
    {
        wallDetector.hitWall+=ChangeDirection;
        rigidbody2d.linearVelocity = initialDirection.normalized * velocidade;
    }

    void ChangeDirection(Vector2 direction)
    {
        rigidbody2d.linearVelocity = direction.normalized * velocidade;
    }

    void OnDisable()
    {
        wallDetector.hitWall-=ChangeDirection;
    }
}
