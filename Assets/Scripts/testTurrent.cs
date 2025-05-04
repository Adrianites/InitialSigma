using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTurrent : MonoBehaviour
{
    [Header("Turret Settings")]
    public float rotationSpeed = 5f; 
    public float fireRate = 1f; 

    [Header("References")]
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    private Transform player; 

    private bool isPlayerInRange = false; 
    private float fireTimer = 0f; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player detected by turret.");
            player = collision.transform; 
            isPlayerInRange = true; 
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited turret range.");
            player = null; 
            isPlayerInRange = false; 
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust for sprite rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

       
        if (isPlayerInRange)
        {
            HandleFiring();
        }
    }

    void HandleFiring()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            FireBullet();
            fireTimer = fireRate;
        }
    }

    void FireBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("Turret fired a bullet.");
        }
        else
        {
            Debug.LogWarning("Bullet prefab or fire point is not assigned.");
        }
    }
}
