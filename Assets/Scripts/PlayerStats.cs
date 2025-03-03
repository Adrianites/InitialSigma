using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Health")]
    public float maxHealth = 100;
    public float currentHealth = 100;

    public UIManager uiManager;
    Mine mine;



    void Awake()
    {   
        currentHealth = maxHealth;
        uiManager = FindObjectOfType<UIManager>();
        Debug.Log("Player health initialized to: " + currentHealth);
    }

    public void DamageZoneTakeDamage(float damage)
    {
        Debug.Log("Player takes damage: " + damage);
        currentHealth -= damage;
        Debug.Log("Player current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void MineTakeDamage(float damage)
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
        uiManager.PauseGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageZone"))
        {
            DamageZone damageZone = collision.GetComponent<DamageZone>();
            if (damageZone != null)
            {
                Debug.Log("Player entered DamageZone: " + damageZone.gameObject.name);
                DamageZoneTakeDamage(damageZone.damageAmount);
            }
        }
        else if (collision.CompareTag("EndGame"))
        {
            uiManager.WinMenu();
        }
        else if (collision.CompareTag("Mine"))
        {   
            Debug.Log("Player entered MineRange: " + gameObject.name);
            MineTakeDamage(mine.damageAmount);
        }
    }
}