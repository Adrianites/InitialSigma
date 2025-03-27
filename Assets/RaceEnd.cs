using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceEnd : MonoBehaviour
{
    #region OnTriggerEnter2D
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.ChangeGameState(GameState.RaceOver);
        }
    }
    #endregion
}
