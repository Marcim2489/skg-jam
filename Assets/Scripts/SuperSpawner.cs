using UnityEngine;
using System.Collections;

public class SuperSpawner : MonoBehaviour
{
    [SerializeField]Projectile projectile;
    [SerializeField]float timeToSpawn = 1f;
    [SerializeField]float spawnOffset = 0.8f;
    [SerializeField]float projectileSpeed = 20f;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeToSpawn);
        Projectile p = Instantiate(projectile);
        p.transform.position = transform.position + transform.up*spawnOffset;
        p.Launch(projectileSpeed, transform.up);

        Projectile p2 = Instantiate(projectile);
        p2.transform.position = transform.position + transform.right*spawnOffset;
        p2.Launch(projectileSpeed, transform.right);

        Projectile p3 = Instantiate(projectile);
        p3.transform.position = transform.position - transform.up*spawnOffset;
        p3.Launch(projectileSpeed, -transform.up);

        Projectile p4 = Instantiate(projectile);
        p4.transform.position = transform.position - transform.right*spawnOffset;
        p4.Launch(projectileSpeed, -transform.right);

        StartCoroutine(Spawn());
    }
}
