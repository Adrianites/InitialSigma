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
    public HealthBar healthBar;

    void Awake()
    {   
        currentHealth = maxHealth;
        uiManager = FindObjectOfType<UIManager>();
        //Debug.Log("Player health initialized to: " + currentHealth);
    }

    void Start()
    {
        //currentHealth = maxHealth;
        //Debug.Log("Player health initialized to: " + currentHealth);  
        //healthBar.SetMaxHealth(maxHealth); //error here
    }

    void Update()   //DELETE AFTER TESTING HEALTH
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(50);
        }
    }

   public void TakeDamage(float damage)
    {
        Debug.Log("Player takes damage: " + damage);
        currentHealth -= damage;
        //healthBar.SetHealth(currentHealth); //error here
        Debug.Log("Player current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    } 

   /* public void DamageZoneTakeDamage(float damage) //HAVE TO IMPROVE THIS 
    {
        Debug.Log("Player takes damage: " + damage);
        currentHealth -= damage;
        Debug.Log("Player current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    } */
    
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
                TakeDamage(damageZone.damageAmount); //change to damageZoneTakeDamage when fixed
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
                MineTakeDamage(mine.damageAmount);
                Object.Destroy(mine.gameObject);
            }
        }

        else if (collision.CompareTag("Spike"))
        {
            Spike spike = collision.GetComponent<Spike>();
            if (spike != null)
            {
                Debug.Log("Player entered SpikeRange: " + spike.gameObject.name);
                TakeDamage(spike.damage);
                Object.Destroy(spike.gameObject);
            }
        }
    }
}