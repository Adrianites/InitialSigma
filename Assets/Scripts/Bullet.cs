using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //make public later on
    //[Range(1, 10)]
    [SerializeField] 
    public float speed = 10f;

    //[Range(1, 10)]
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
        Debug.Log("Bullet hit: " + other.gameObject.name);
        // Check if the bullet collides with an enemy
        if (other.CompareTag("Enemy"))
        {
            // Get the Enemy script and call TakeDamage
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // Deal damage to the enemy
            }

            // Destroy the bullet after it hits something
            Destroy(gameObject);
        }
    }

}
