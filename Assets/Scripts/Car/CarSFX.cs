using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSFX : MonoBehaviour
{
    #region Variables
    [Header("Audio Sources")]
    public AudioSource tireScreechSource;
    public AudioSource engineSource;
    public AudioSource crashSource;
    public AudioSource jumpSource;
    public AudioSource landingSource;
    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;
    CarController carController;
    #endregion

    #region Awake
    private void Awake()
    {
        carController = GetComponentInParent<CarController>();
    }
    #endregion

    #region Update
    private void Update()
    {
        UpdateEngineSFX();
        UpdateTireScreechSFX();
    }
    #endregion

    #region Update Engine SFX
    void UpdateEngineSFX()
    {
        float velocityMagnitude = carController.GetVelocityMagnitude();

        float engineVolume = velocityMagnitude * 0.05f;
        engineVolume = Mathf.Clamp(engineVolume, 0.2f, 1f);

        engineSource.volume = Mathf.Lerp(engineSource.volume, engineVolume, Time.deltaTime * 10);

        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        engineSource.pitch = Mathf.Lerp(engineSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }
    #endregion

    #region Update Tire Screech SFX
    void UpdateTireScreechSFX()
    {
        if(carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if(isBraking)
            {
                tireScreechSource.volume = Mathf.Lerp(tireScreechSource.volume, 1.0f, Time.deltaTime * 10);
                tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                tireScreechSource.volume = Mathf.Abs(lateralVelocity) * 0.85f;
                tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        else
        {
            tireScreechSource.volume = Mathf.Lerp(tireScreechSource.volume, 0.0f, Time.deltaTime * 10);
        }
    }
    #endregion

    #region Jump SFX
    public void PlayJumpSFX()
    {
        jumpSource.Play();
    }
    #endregion

    #region Landing SFX
    public void PlayLandingSFX()
    {
        landingSource.Play();
    }
    #endregion

    #region Crash SFX
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        float relativeVelocity = collision2D.relativeVelocity.magnitude;

        float volume = relativeVelocity * 0.1f;

        crashSource.volume = volume;
        crashSource.pitch = Random.Range(0.95f, 1.05f);

        if(!crashSource.isPlaying)
        {
            crashSource.Play();
        }
    }
    #endregion
}
