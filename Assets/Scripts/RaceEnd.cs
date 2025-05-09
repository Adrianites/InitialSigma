using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceEnd : MonoBehaviour
{
    public GameObject winCanvas = null;
    public bool lastLevel = false;
    UIManager uiManager;

    void Awake()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }

    #region OnTriggerEnter2D
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.ChangeGameState(GameState.RaceOver);

            if (winCanvas != null && lastLevel)
            {
                winCanvas.SetActive(true);
                uiManager.isPaused = true;
                GameManager.instance.OnRaceEnded();
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;

            }
        }
    }
    #endregion
}
