using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInputHandler : MonoBehaviour
{   
    #region Variables
    public int playerID = 1;
    CarController carController;
    #endregion

    #region Awake
    void Awake()
    {
        carController = GetComponent<CarController>();
    }
    #endregion

    #region Update
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        
        switch (playerID)
        {
            case 1:
                inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                break;
            case 2:
                inputVector = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
                break;
        }
        
        carController.SetInputVector(inputVector);
    }
    #endregion

}
