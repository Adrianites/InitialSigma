using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Health")]
    public float maxHealth = 100;
    public float currentHealth;

    public UIManager uiManager;
    public HealthBar healthBar;

    void Awake()
    {   
        uiManager = FindObjectOfType<UIManager>();
        //Debug.Log("Player health initialized to: " + currentHealth);

        GameObject healthBarObject = GameObject.Find("HealthBar");
        if (healthBarObject != null)
        {
            healthBar = healthBarObject.GetComponent<HealthBar>();
        }
        else
        {
            Debug.LogWarning("HealthBar GameObject not found in the scene!");
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            Debug.LogWarning("HealthBar reference is missing in PlayerStats script.");
        }
       
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
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
        uiManager.Death();
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
                TakeDamage(mine.damageAmount);
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