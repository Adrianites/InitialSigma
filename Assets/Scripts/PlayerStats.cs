using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Health")]
    public float maxHealth = 100;
    public float currentHealth = 100;

    void Awake()
    {
        currentHealth = maxHealth;
        Debug.Log("Player health initialized to: " + currentHealth);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player takes damage: " + damage);
        currentHealth -= damage;
        Debug.Log("Player current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //public void OnFire()
    //{
        //TakeDamage(10);
    //}

    public void Die()
    {
        Debug.Log("Player died");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageZone"))
        {
            // Assuming the damage amount is specified in the DamageZone script
            DamageZone damageZone = collision.GetComponent<DamageZone>();
            if (damageZone != null)
            {
                Debug.Log("Player entered DamageZone: " + damageZone.gameObject.name);
<<<<<<< Updated upstream
                TakeDamage(damageZone.damageAmount);
=======
                DamageZoneTakeDamage(damageZone.damageAmount);
            }
        }
        else if (collision.CompareTag("EndGame"))
        {
            uiManager.WinMenu();
        }
        else if (collision.CompareTag("Mine"))
        {   
            Mine mine = collision.GetComponent<Mine>();
            if (mine != null)
            {
                Debug.Log("Player entered MineRange: " + mine.gameObject.name);
                mine.GetComponent<Animation>();
                mine.Detected();
                MineTakeDamage(mine.damageAmount);
                Destroy(mine.gameObject);
>>>>>>> Stashed changes
            }
        }
    }
}