using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]GameObject prefab;
    [SerializeField]float timeToSpawn = 2f;
    [SerializeField]Vector2 spawnOffset = Vector2.zero;
    [SerializeField]Quaternion startingRotation;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeToSpawn);
        Transform instance = Instantiate(prefab, transform).transform;
        instance.position = transform.position + (Vector3)spawnOffset;
        instance.rotation = startingRotation;
        StartCoroutine(Spawn());
    }
}
