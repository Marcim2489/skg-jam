using UnityEngine;

public class BouncingObstacle : MovingObstacle
{
    [SerializeField]ObstacleWallDetector wallDetector;

    protected override void Start()
    {
        base.Start();
        wallDetector.hitWall+=ChangeDirection;
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
