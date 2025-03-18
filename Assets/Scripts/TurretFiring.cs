using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFiring : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firingPoint;
    [SerializeField] private Transform firePoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;

    private float fireTimer;

    public bool inZone = false;

    PlayerStats playerStats;
    //Bullet bullet;
    
    public void OnTriggerEnter2D()
    {
        if (inZone)
        {
            Debug.Log("Player entered turret range.");
            if (fireTimer <= 0f)
            {
                Shoot();
                fireTimer = fireRate;
            }
            else
            {
                fireTimer -= Time.deltaTime;
            }
            Debug.Log("Fire");
        }

    /* Debug.Log("Player entered turret range.");
        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
        Debug.Log("Fire"); */
    }

    void FixedUpdate()
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

    public void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.transform.position, firingPoint.transform.rotation);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player left turret range.");
            playerStats = null;
        }
    }

    /* public void OnTriggetEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                bullet = collision.GetComponent<Bullet>();
                if (bullet != null)
                {
                    playerStats.TakeDamage(damage);
                }
            }
        }*/

}
