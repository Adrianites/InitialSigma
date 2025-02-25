using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public PlayerStats playerStats;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    //health
    public void TakeDamage(float damage)
    {
        playerStats.health -= damage;
        if (playerStats.health <= 0)
        {
            playerStats.health = 0;
        }
    }

    

}
