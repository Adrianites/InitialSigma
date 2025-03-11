using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerUp : MonoBehaviour
{
    public AudioClip collectSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayCollectSound();
            Destroy(gameObject, collectSound.length);
            
        }
    }

    private void PlayCollectSound()
    {
        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }


}
