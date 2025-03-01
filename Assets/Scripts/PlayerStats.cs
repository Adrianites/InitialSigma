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
    } 

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void OnFire()
    {
        
        TakeDamage(10);
        
    }
    public void Die()
    { 
    Debug.Log("Player died");
    }
       
    
}
