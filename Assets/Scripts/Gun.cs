using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float fireTimer;

    [Header("Gun")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firingPoint;
    [SerializeField] private Transform firePoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;

    public void OnButton()
    {
        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    public void PlayerShoot()
    {
        Shoot();

        // if (fireTimer <= 0f)
        // {
        //     Shoot();
        //     fireTimer = fireRate;
        // }
        // else
        // {
        //     fireTimer -= Time.deltaTime;
        // }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.transform.position, firingPoint.transform.rotation);
    }

}
