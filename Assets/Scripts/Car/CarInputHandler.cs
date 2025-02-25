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
    #endregion

    #region Awake
    void Awake()
    {
        carController = GetComponent<CarController>();
    }
    #endregion


    public void OnMove(InputValue value)
    {
        movement=value.Get<Vector2>();
    }

    #region Update
    void Update()
    {
        // Vector2 inputVector = Vector2.zero;
        //         inputVector.x = Input.GetAxis("Horizontal_P" + playerID);
        //         inputVector.y = Input.GetAxis("Vertical_P" + playerID);
                
        carController.SetInputVector(movement);
    }
    #endregion

}
