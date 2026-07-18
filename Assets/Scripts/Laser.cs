using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]GameObject laser;
    [SerializeField]float maxDistance = 10;
    [SerializeField]float timeToShoot = 2.2f;
    [SerializeField]float laserDuration = 0.6f;
    [SerializeField]LayerMask wallLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Transform laserInstance;
    void Start()
    {
        StartCoroutine(Shoot());
    }

    void Update()
    {
        ManageLaserSize();
    }

    void ManageLaserSize()
    {
        if (laserInstance == null)
        {
            return;
        }
        float size;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, maxDistance, wallLayer);
        if (hit)
        {
            size = hit.distance;
        }
        else
        {
            size = maxDistance;
        }
        laserInstance.localScale = new Vector2(laserInstance.localScale.x, size);
        laserInstance.position = transform.position - transform.up * 0.5f * size;
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(timeToShoot);
        laserInstance = Instantiate(laser, transform).transform;
        
        ManageLaserSize();
        yield return new WaitForSeconds(laserDuration);
        Destroy(laserInstance.gameObject);
        laserInstance = null;
        StartCoroutine(Shoot());
    }
}
