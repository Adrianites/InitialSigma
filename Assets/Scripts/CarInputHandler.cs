using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInputHandler : MonoBehaviour
{
    TopDownCarController carController;

    void Awake()
    {
        carController = GetComponent<TopDownCarController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 inputVector = Vector2.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        
        
        carController.SetInputVector(inputVector);
    }
/*
    public void KMmovement()
    {
        Vector2 inputVector = Vector2.zero;
        inputVector.x = amongus
        inputVector.y = sus
    }

    public void Controllermovement()
    {
        Vector2 inputVector = Vector2.zero;
        inputVector.x = amongus
        inputVector.y = sus
    }
    */
}
