using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CarController : MonoBehaviour
{
    #region Variables
    [Header("Car Settings")]
    public float driftFactor = 0.30f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 60;
    float accelerationInput = 0;
    float steeringInput = 0; 
    float rotationAngle = 0;
    float velocityVsUp = 0;
    Rigidbody2D rb;

    [Header("Indepth Adjustments")]
    public float IsTireScreechingValue = 4.0f;
    public float ApplySteeringValue = 8.0f;

    #endregion

    #region Awake
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    #endregion

    #region Fixed Update
    void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }
    #endregion

    #region Apply Engine Force
    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);

        if(velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if(velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        if(rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if(accelerationInput == 0)
        {
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.deltaTime * 3);
        }
        else
        {
            rb.drag = 0;
        }

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }
    #endregion

    #region Apply Steering
    void ApplySteering()
    {
        float minSpeedBeforeTurningFactor = (rb.velocity.magnitude / ApplySteeringValue);
        minSpeedBeforeTurningFactor = Mathf.Clamp01(minSpeedBeforeTurningFactor);

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeTurningFactor;
        
        rb.MoveRotation(rotationAngle);
    }
    #endregion

    #region Kill Orthogonal Velocity
    void KillOrthogonalVelocity()
    {
        Vector2 fowardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = fowardVelocity + rightVelocity * driftFactor;
    }
    #endregion

    #region Set Input Vector
    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
    #endregion
    
    #region Lateral Velocity
    public float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, rb.velocity);
    }
    #endregion

    #region Tire Screeching
    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        if (Mathf.Abs(GetLateralVelocity()) > IsTireScreechingValue)
        {
            return true;
        }

        return false;
    }
    #endregion
}
