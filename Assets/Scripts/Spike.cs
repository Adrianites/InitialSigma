using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float damage = 1f;
    public float knockback = 1f;
    public float knockbackDuration = 0.1f;
    public float knockbackHeight = 1f;
    public float knockbackSpeed = 1f;

    PlayerStats playerStats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Debug.Log("Player hit by spike");
        {
            playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
                //playerStats.Knockback(knockback, knockbackDuration, knockbackHeight, knockbackSpeed);
            }
        }
    }
}
