using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skidmarks : MonoBehaviour
{
    #region Variables
    CarController carController;
    TrailRenderer trailR;
    #endregion

    #region Awake
    void Awake()
    {
        carController = GetComponentInParent<CarController>();
        trailR = GetComponent<TrailRenderer>();
        trailR.emitting = false;
    }
    #endregion

    #region Update
    void Update()
    {
        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            trailR.emitting = true;
        }
        else
        {
            trailR.emitting = false;
        }
    }
    #endregion
}
