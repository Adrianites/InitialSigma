using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CarInputHandler : MonoBehaviour
{   
    #region Variables
    public int playerID = 1;
    public Vector2 movement;
    CarController carController;
    UIManager uiManager;
    Gun gun;
    #endregion

    #region Awake
    void Awake()
    {
        carController = GetComponent<CarController>();
        uiManager = GameObject.FindObjectOfType<UIManager>();
        gun = GameObject.FindObjectOfType<Gun>();
    }
    #endregion

    #region OnMove
    public void OnMove(InputValue value)
    {
        movement=value.Get<Vector2>();
    }
    #endregion

    #region OnShoot
    public void OnShoot()
    {
        gun.PlayerShoot();
    }
    #endregion

    #region OnPause
    public void OnPause()
    {
        if (uiManager.isPaused == false)
        {
            uiManager.PauseGame();
        }
        else
        {
            uiManager.ResumeGame();
        }
    }
    #endregion

    #region Update
    void Update()
    {
        carController.SetInputVector(movement);

            #region Cheats
            if (Input.GetKeyDown(KeyCode.F1))
            {
                SceneManager.LoadScene(NameStrings.MainMenu);
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                SceneManager.LoadScene(NameStrings.SingleplayerLevel1);
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                SceneManager.LoadScene(NameStrings.SingleplayerLevel2);
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                SceneManager.LoadScene(NameStrings.SingleplayerLevel3);
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                SceneManager.LoadScene(NameStrings.SingleplayerLevel4);
            }
            #endregion
    }
    #endregion
}
