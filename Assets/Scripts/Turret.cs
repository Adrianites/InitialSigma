using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public Transform player; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player detected by turret.");
            player = collision.transform;
        }

    }
    void OnTriggerEnter2D()
    {
        Debug.Log("Turret Start method called.");
        
        //find the player GameObject by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; //assign the players transform
            Debug.Log("Player found: " + playerObject.name);
        }
        else
        {
            Debug.Log("Player not found!");
        }
    }
    void FixedUpdate()
    {
        //sDebug.Log("Turret Update method called.");
        Debug.Log($"player {player}");
        if (player != null)
        {
            //calculating the direction
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //smooth rotation
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); //adjust for sprite rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            Debug.Log("Turret rotating towards player. Angle: " + angle);
            Debug.Log("Current Rotation: " + transform.rotation.eulerAngles);
            Debug.Log("Target Rotation: " + targetRotation.eulerAngles);
        }
        /*else
        {
            Debug.LogWarning("Player reference is null.");
        } */
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited turret range.");
            player = null;
        }
    }
}