using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    public float speed = 10f;

    [SerializeField] 
    public float lifeTime = 3f;

    public float damage = 10f;

    Rigidbody2D rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate() 
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AI") || other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Enemy takes damage: " + damage);
                enemy.TakeDamage(damage); 
            }
            
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                Debug.Log("Player takes damage: " + damage);
                playerStats.TakeDamage(damage); 
            }

            Destroy(gameObject);
        }
    }

}
