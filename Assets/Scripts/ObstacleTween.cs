using UnityEngine;
using DG.Tweening;

public class ObstacleTween : MonoBehaviour
{
    [SerializeField]Vector3 moveVector;
    [SerializeField]float moveTime;
    [SerializeField]Ease easing = Ease.InOutQuad;
    [SerializeField]LoopType loopType = LoopType.Yoyo;
    Vector3 startPosition;

    void Start()
    {
        startPosition=transform.position;
        Move();
    }

    void Move()
    {
        transform.DOMove(startPosition+moveVector, moveTime).SetEase(easing).SetLoops(-1,loopType);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
