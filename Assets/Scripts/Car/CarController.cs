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

    [Header("Sprites")]
    public SpriteRenderer carSR;
    public SpriteRenderer shadowSR;

    [Header("Jump")]
    public AnimationCurve jumpCurve;
    public ParticleSystem landingPS;

    [Header("Indepth Adjustments")]
    public float IsTireScreechingValue = 4.0f;
    public float ApplySteeringValue = 8.0f;
    public float JumpShadowDisValue = 3.0f;
    public float PushForwardJumpValue = 10.0f;
    public float LandingCheckCircleSize = 1.5f;

    // local
    float accelerationInput = 0;
    float steeringInput = 0; 
    float rotationAngle = 0;
    float velocityVsUp = 0;
    bool isJumping = false;

    // components
    Rigidbody2D rb;
    Collider2D carCollider;
    CarSFX carSFX;
    #endregion

    #region Awake
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        carCollider = GetComponentInChildren<Collider2D>();
        carSFX = GetComponent<CarSFX>();
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
        if(isJumping && accelerationInput < 0)
        {
            accelerationInput = 0;
        }
        if (isJumping)
        {
            return;
        }
        else
        {
            if(accelerationInput == 0)
            {
                rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.deltaTime * 3);
            }
            else
            {
                rb.drag = 0;
            }
        }
    
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);

        if(velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if(velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        if(rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0 && !isJumping)
        {
            return;
        }

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }
    #endregion

    #region Apply Steering
    void ApplySteering()
    {
        if(isJumping)
        {
            return;
        }
        else
        {
            float minSpeedBeforeTurningFactor = (rb.velocity.magnitude / ApplySteeringValue);
            minSpeedBeforeTurningFactor = Mathf.Clamp01(minSpeedBeforeTurningFactor);

            rotationAngle -= steeringInput * turnFactor * minSpeedBeforeTurningFactor;
            
            rb.MoveRotation(rotationAngle);
        }
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

        if (isJumping)
        {
            return false;
        }

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

    #region Get Velocity Magnitude
    public float GetVelocityMagnitude()
    {
        return rb.velocity.magnitude;
    }
    #endregion

    #region Jump Func
    public void Jump(float jumpHeightScale, float jumpPushScale)
    {
        if(!isJumping)
        {
            StartCoroutine(JumpCo(jumpHeightScale, jumpPushScale));
        }
    }
    #endregion

    #region Jump Coroutine
    IEnumerator JumpCo(float jumpHeightScale, float jumpPushScale)
    {
        isJumping = true;
        float jumpStart = Time.time;
        float jumpDuration = rb.velocity.magnitude * 0.05f;

        jumpHeightScale = jumpHeightScale * rb.velocity.magnitude * 0.05f;
        jumpHeightScale = Mathf.Clamp(jumpHeightScale, 0f, 1f);

        carCollider.enabled = false;

        carSFX.PlayJumpSFX();

        carSR.sortingLayerName = "Flying";
        shadowSR.sortingLayerName = "Flying";

        rb.AddForce(rb.velocity.normalized * jumpPushScale * PushForwardJumpValue, ForceMode2D.Impulse);
        
        while (isJumping)
        {
            float jumpCompletedPercentage = (Time.time - jumpStart) / jumpDuration;
            jumpCompletedPercentage = Mathf.Clamp01(jumpCompletedPercentage);

            carSR.transform.localScale = Vector3.one + Vector3.one * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;

            shadowSR.transform.localScale = carSR.transform.localScale * 0.75f;
            shadowSR.transform.localPosition = new Vector3(1, -1, 0f) * JumpShadowDisValue * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;

            if(jumpCompletedPercentage == 1f)
            {
                break;
            }

            yield return null;
        }
        
        if(Physics2D.OverlapCircle(transform.position, LandingCheckCircleSize))
        {
            isJumping = false;
            Jump(0.2f, 0.6f);
        }
        else
        {
            carSR.transform.localScale = Vector3.one;

            shadowSR.transform.localPosition = Vector3.zero;
            shadowSR.transform.localScale = carSR.transform.localScale;

            carCollider.enabled = true;

            carSR.sortingLayerName = "Default";
            shadowSR.sortingLayerName = "Default";

            if (jumpHeightScale > 0.2f)
            {
                landingPS.Play();
                carSFX.PlayLandingSFX();

            }

            isJumping = false;
        }
    }
    #endregion

    #region On Trigger Enter
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("Jump"))
        {
            JumpData jumpData = collider2D.GetComponent<JumpData>();
            Jump(jumpData.jumpHeightScale, jumpData.jumpPushScale);
        }
    }
    #endregion
}
