using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInputHandler : MonoBehaviour
{   
    #region Variables
    public int playerID = 1;
    public Vector2 movement;
    CarController carController;
    UIManager uiManager;
    #endregion

    #region Awake
    void Awake()
    {
        carController = GetComponent<CarController>();
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }
    #endregion

    public void OnMove(InputValue value)
    {
        movement=value.Get<Vector2>();
    }

    #region Update
    void Update()
    {
        carController.SetInputVector(movement);

        if( Input.GetKeyDown(KeyCode.Tab))
        {
            uiManager.PauseGame();
        }
    }
    #endregion
}
