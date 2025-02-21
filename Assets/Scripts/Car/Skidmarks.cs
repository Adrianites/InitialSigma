using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skidmarks : MonoBehaviour
{
    #region Variables
    public bool isBridgeEmitter = false;
    CarController carController;
    TrailRenderer trailR;
    CarLayerHandler carLH;
    #endregion

    #region Awake
    void Awake()
    {
        carController = GetComponentInParent<CarController>();
        carLH = GetComponentInParent<CarLayerHandler>();
        trailR = GetComponent<TrailRenderer>();
        trailR.emitting = false;
    }
    #endregion

    #region Update
    void Update()
    {
        trailR.emitting = false;

        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (carLH.IsDrivingOnBridge() && isBridgeEmitter)
            {
                trailR.emitting = true;
            }
            
            if (!carLH.IsDrivingOnBridge() && !isBridgeEmitter)
            {
                trailR.emitting = true;
            }
        }
    }
    #endregion
}
