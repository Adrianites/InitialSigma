using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float rotationSpeed = 5f; // Speed at which the turret rotates
    private Transform player; // Reference to the player's transform

    void Start()
    {
        Debug.Log("Turret Start method called.");
        
        // Find the player GameObject by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; // Assign the player's transform
            Debug.Log("Player found: " + playerObject.name);
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
    }

    void Update()
    {
        //Debug.Log("Turret Update method called.");
        
        if (player != null)
        {
            //calculating the direction
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //smooth rotation
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust for sprite rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            Debug.Log("Turret rotating towards player. Angle: " + angle);
            Debug.Log("Current Rotation: " + transform.rotation.eulerAngles);
            Debug.Log("Target Rotation: " + targetRotation.eulerAngles);
        }
        else
        {
            Debug.LogWarning("Player reference is null.");
        }
    }
}