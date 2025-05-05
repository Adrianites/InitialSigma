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

    public AudioSource MineAudioSource;
    public AudioSource SpikeAudioSource;
    Animator animator;

    void Awake()
    {   
        uiManager = FindObjectOfType<UIManager>();

        GameObject healthBarObject = GameObject.Find("HealthBar");
        if (healthBarObject != null)
        {
            healthBar = healthBarObject.GetComponent<HealthBar>();
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

   public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

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
        currentHealth -= damage;
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
        uiManager.Death();
    }

    #region Trigger Events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageZone"))
        {
            DamageZone damageZone = collision.GetComponent<DamageZone>();
            if (damageZone != null)
                {
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
                
                TakeDamage(mine.damageAmount);

                Animator mineAnimator = mine.GetComponent<Animator>();
                if (mineAnimator != null)
                {
                    mineAnimator.SetBool("isDetected", true);
                }
                
                AudioSource mineAudio = mine.GetComponent<AudioSource>();
                if (mineAudio != null)
                {
                    MineAudioSource.Play(); // Play the sound
                }

                StartCoroutine(DestroyMineAfterDelay(mine.gameObject, 0.7f));
            }
        }

        else if (collision.CompareTag("Spike"))
        {
            Spike spike = collision.GetComponent<Spike>();
            if (spike != null)
            {
                TakeDamage(spike.damage);

                AudioSource spikeAudio = spike.GetComponent<AudioSource>();
                if (spikeAudio != null)
                {
                    SpikeAudioSource.Play();
                    Object.Destroy(spike.gameObject); // Play the sound
                }
                Object.Destroy(spike.gameObject);
            }
        }

        else if (collision.CompareTag("Player"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage); // Apply damage from the mine
                //Destroy(bullet.gameObject); // Destroy the mine
            }
        }
    }
    #endregion

    #region Trigger Exit Events
    private IEnumerator DestroyMineAfterDelay(GameObject mine, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(mine);
        }
    #endregion
}