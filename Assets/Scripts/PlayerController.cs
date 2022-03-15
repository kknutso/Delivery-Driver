using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] float driftFactor = 0.05f;
    [SerializeField] float accelerationFactor = 30f;
    [SerializeField] float turnFactor = 3.5f;
    [SerializeField] float minSpeedBeforeAllowTurn = 8f;
    [SerializeField] float maxSpeed = 20f;

    float accelerationInput = 0f;
    float steeringInput = 0f;

    float rotationAngle = 0f;

    float velocityVsUp = 0f;

    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        //Calculate how much "forward" we are going in terms of direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, rb2d.velocity);

        //Limit so we cannot go faster than the max speed in the "forward" direction
        if(velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        //Limit so we cannot go faster than 50% of max speed in reverse direction
        if(velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        //Limit so we cannot go faster in any direction while accelerating
        if (rb2d.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        //Apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
        if (accelerationInput == 0)
        {
            rb2d.drag = Mathf.Lerp(rb2d.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else rb2d.drag = 0;

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //Apply force and pushes the car forward
        rb2d.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        //Limit the cars ability to turn when moving slowly
        float minSpeedBeforeAllowTurningFactor = (rb2d.velocity.magnitude / minSpeedBeforeAllowTurn);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        //Apply sterring by rotating the car object
        rb2d.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb2d.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb2d.velocity, transform.right);

        rb2d.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public void IncreaseForce(float value)
    {
        Vector2 boosterForce = transform.up * accelerationInput * accelerationFactor * value;
        rb2d.AddRelativeForce(boosterForce, ForceMode2D.Impulse);
    }

}
