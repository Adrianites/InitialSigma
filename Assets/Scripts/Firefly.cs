using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyAnimationController : MonoBehaviour
{
    private Animator animator;
    
    
    public float animationSpeed = 1.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing on " + gameObject.name);
            return;
        }
        
        SetAnimationSpeed(animationSpeed);
    }

    public void SetAnimationSpeed(float speed)
    {
        if (animator != null)
        {
            animator.speed = speed;
        }
    }
}

