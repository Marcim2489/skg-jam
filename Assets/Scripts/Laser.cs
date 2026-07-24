using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]LaserInstance laser;
    [SerializeField]float maxDistance = 10;
    [SerializeField]float timeToShoot = 2.2f;
    [SerializeField]float laserDuration = 0.6f;
    [SerializeField]LayerMask wallLayer;
    [SerializeField]Vector2 laserOffset;
    [SerializeField]GameObject laserSprite;
    [SerializeField]Animator animator;
    [SerializeField]AudioClip laserSound;
    LaserInstance laserInstance;

    void Start()
    {
        laserSprite.SetActive(false);
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position+(Vector3)laserOffset, -transform.up, maxDistance, wallLayer);
        if (hit)
        {
            size = hit.distance;
        }
        else
        {
            size = maxDistance;
        }
        laserInstance.ManageSize(size);
        laserInstance.transform.position = transform.position+(Vector3)laserOffset - transform.up * 0f * size;
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(timeToShoot);
        laserSprite.SetActive(true);
        animator.Play("PreparingLaser");
        yield return new WaitForSeconds(0.3f);
        laserInstance = Instantiate(laser, transform);
        SFXManager.Instance.PlaySound(laserSound, transform.position, 1f, false);
        ManageLaserSize();
        yield return new WaitForSeconds(laserDuration);
        laserSprite.SetActive(false);
        animator.Play("New State");
        Destroy(laserInstance.gameObject);
        laserInstance = null;
        StartCoroutine(Shoot());
    }
}
