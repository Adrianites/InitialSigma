using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SmokeEmission : MonoBehaviour
{
    #region Variables
    public bool isTunnelEmitter = false;
    float particleEmissionRate = 0;
    CarController carController;
    ParticleSystem ps;
    ParticleSystem.EmissionModule psem;
    CarLayerHandler carLH;
    #endregion

    #region Awake
    void Awake()
    {
        carController = GetComponentInParent<CarController>();
        carLH = GetComponentInParent<CarLayerHandler>();
        ps = GetComponent<ParticleSystem>();
        psem = ps.emission;
        psem.rateOverTime = 0;
        psem.enabled = false;
    }
    #endregion

    #region Update
    void Update()
    {
        psem.enabled = false;

        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        psem.rateOverTime = particleEmissionRate;

        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (carLH.IsDrivingOnBridge() && isTunnelEmitter)
            {
                psem.enabled = true;
                
                if (isBraking)
                {
                    particleEmissionRate = 30;
                }
                else
                {
                    particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
                }
            }

            if (!carLH.IsDrivingOnBridge() && !isTunnelEmitter)
            {
                psem.enabled = true;

                if (isBraking)
                {
                    particleEmissionRate = 30;
                }
                else
                {
                    particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
                }
            }
        }
    }
    #endregion
}
