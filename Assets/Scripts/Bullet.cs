using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //make public later on
    [Range(1, 10)]
    [SerializeField] 
    private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] 
    private float lifeTime = 3f;

    Rigidbody2D rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate() 
    {
        rb.velocity = transform.up * speed;
    }

}
