using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                inputVector.x = Input.GetAxis("Horizontal_P" + playerID);
                inputVector.y = Input.GetAxis("Vertical_P" + playerID);
        carController.SetInputVector(inputVector);
    }
    #endregion

}
