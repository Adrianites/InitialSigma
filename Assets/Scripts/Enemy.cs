using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;  // The health of the enemy

    // Method to apply damage to the enemy
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    // Method to handle the enemy's death
    private void Die()
    {
        Destroy(gameObject);  // Destroys the enemy game object
    }
}
