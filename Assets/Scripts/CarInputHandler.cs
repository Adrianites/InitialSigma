using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInputHandler : MonoBehaviour
{   
    #region Variables
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
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        
        carController.SetInputVector(inputVector);

        if(Input.GetButtonDown("Jump"))
        {
            carController.Jump(1f, 0f);
        }
    }
    #endregion

}
