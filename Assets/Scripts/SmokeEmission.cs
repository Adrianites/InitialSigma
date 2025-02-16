using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEmission : MonoBehaviour
{
    #region Variables
    float particleEmissionRate = 0;
    CarController carController;
    ParticleSystem ps;
    ParticleSystem.EmissionModule psem;
    #endregion

    #region Awake
    void Awake()
    {
        carController = GetComponentInParent<CarController>();
        ps = GetComponent<ParticleSystem>();
        psem = ps.emission;
        psem.rateOverTime = 0;
    }
    #endregion

    #region Update
    void Update()
    {
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        psem.rateOverTime = particleEmissionRate;

        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
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
    #endregion

}
