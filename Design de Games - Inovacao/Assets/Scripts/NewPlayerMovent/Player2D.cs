using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player2D : MonoBehaviour
{
    [Header("Movimentação Horizontal")]
    float moveSpeed = 6;
    float velocityXSmoothing;
    float accelerationTimeAirborne = 0.1f;
    float accelerationTimeGrounded = 0.05f;

    [Header("Movimentação Vertical")]
    float maxJumpVelocity;
    float minJumpVelocity;
    float gravity;
    public float maxJumpHeight = 4;
    public float minJumpHeight = 2;
    public float timeToJumpApex = 0.4f;
    bool jump;

    Vector2 velocity;

    Controller2D controller;

    Vector2 directionalInput;


    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void FixedUpdate()
    {   CalculateVelocity();

        if (jump == true && controller.collisions.below)
        {
            velocity.y = maxJumpHeight;
        }    

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

    public void OnJumpInputDown()
    {
        if (controller.collisions.below)
        {
            jump = true;
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
        jump = false;
    }
}
