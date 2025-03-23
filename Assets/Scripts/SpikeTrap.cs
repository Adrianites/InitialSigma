using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [Header("Spike Trap Settings")]
    public float damage = 10f; 
    public float delay = 1f; 
    public float duration = 1f; 
    public float cooldown = 1f; 
    public bool active = true; //whether the trap is active

    [Header("Spike Settings")]
    public GameObject spikePrefab; 
    public Transform[] spikeSpawnPoints; 
    public float spikeLifetime = 2f; 

    private void Start()
    {
        StartCoroutine(Trap());
    }

    private IEnumerator Trap()
    {
        while (active)
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(ActivateSpikes());
            yield return new WaitForSeconds(duration);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator ActivateSpikes()
    {
        foreach (Transform spawnPoint in spikeSpawnPoints)
        {
            //instantiate spike at each spawn point
            GameObject spike = Instantiate(spikePrefab, spawnPoint.position, spawnPoint.rotation);
            Destroy(spike, spikeLifetime); // Destroy the spike after its lifetime
        }

        //deal damage to player if they are in range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerStats playerStats = collider.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(damage);
                }
            }
        }

        yield return null;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        active = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}