using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    Animation anim;

    public float damageAmount = 100f;
     
    PlayerStats playerStats;
    Animator animator;

    void Awake()
    {
      anim = GetComponent<Animation>();   
    }

    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    public void Detected(Collider2D collision)
    {
        if (playerStats.CompareTag("Player"))
        animator.SetBool("Detected", true);
    }

}
