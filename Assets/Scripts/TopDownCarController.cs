using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.30f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20f;

    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0; 

    float rotationAngle = 0;

    float velocityVsUp = 0;

    Rigidbody2D carRigidbody2D;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        //Calculate forward in terms of the direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limit max speed in the forward direction
        if(velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        //limit reverse speed by 50% (can change to suit our needs)
        if(velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        //Limit speed in any direction while accelerating
        if(carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }

        //applies drag if no acceleration input so this stops the car when the player lets go of the button
        if(accelerationInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.deltaTime * 3);
        }
        else
        {
            carRigidbody2D.drag = 0;
        }

        // create a force for the engine
      Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

      //apply the force and pushes the car forward
      carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        //limit cars ability to turn while moving
        float minSpeedBeforeTurningFactor = (carRigidbody2D.velocity.magnitude /8);
        minSpeedBeforeTurningFactor = Mathf.Clamp01(minSpeedBeforeTurningFactor);

        //update rotation angle based on input 
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeTurningFactor;
        
        //apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    //side ways velocity for car
    void KillOrthogonalVelocity()
    {
        Vector2 fowardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = fowardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
