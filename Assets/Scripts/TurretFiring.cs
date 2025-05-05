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
    private bool inZone = false; 
    private Turret turret;

    PlayerStats playerStats;

    void Awake()
    {
        turret = GetComponent<Turret>();
        if (turret == null)
        {
            Debug.LogError("Turret component not found on this GameObject.");
        }
    }

    public void OnTriggerEnter2D()
    {
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
