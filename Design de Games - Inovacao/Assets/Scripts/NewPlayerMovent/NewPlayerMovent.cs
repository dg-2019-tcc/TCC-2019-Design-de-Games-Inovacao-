using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class NewPlayerMovent : MonoBehaviour
{
    float moveSpeed = 6;
    float velocityXSmoothing;
    float accelerationTimeAirborne = 0.1f;
    float accelerationTimeGrounded = 0.05f;

    float maxJumpVelocity;
    float minJumpVelocity;
    float gravity;
    public float maxJumpHeight = 4;
    public float minJumpHeight = 2;
    public float timeToJumpApex = 0.4f;

    float pipaMoveSpeed = 4;
    float pipaVelocityXSmoothing;
    float pipaAccelerationTimeAirborne = 0.8f;
    public float pipaGravity; 

    float carroMoveSpeed = 12;
    float carroVelocityXSmoothing;
    float carroAccelerationTimeAirborne = 0.5f;
    float carroAccelerationTimeGrounded = 0.3f;

    float motoMoveSpeed = 6;
    float motoVelocityXSmoothing;
    float motoAccelerationTimeAirborne = 0.1f;
    float motoAccelerationTimeGrounded = 0.05f;

    float motoMaxJumpVelocity;
    float motoMinJumpVelocity;
    float motoGravity;
    public float motoMaxJumpHeight = 4;
    public float motoMinJumpHeight = 2;
    public float motoTimeToJumpApex = 0.4f;

    bool jump;

    public BoolVariable carroActive;
    public BoolVariable pipaActive;

    Vector3 velocity;
    Vector3 carroVelocity;
    Vector3 pipaVelocity;
    Vector3 motoVelocity;

    Controller2D controller;

    TriggerCollisionsController triggerController;

    [SerializeField]
    public Joystick joyStick;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        triggerController = GetComponent<TriggerCollisionsController>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2* Mathf.Abs(gravity)*minJumpHeight);

        joyStick = FindObjectOfType<Joystick>();
    }

    void Update()
    {

        Vector2 input = new Vector2(joyStick.Horizontal, joyStick.Vertical);

        if (carroActive.Value == false && pipaActive.Value == false)
        {

            if (jump == true && controller.collisions.below)
            {
                velocity.y = maxJumpHeight;
            }

            float targetVelocityX = input.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime, input);
            triggerController.MoveDirection(velocity);
            if (controller.collisions.above ||controller.collisions.below)
            {
                velocity.y = 0;
            }
        }

        else
        {
            if (carroActive.Value == true)
            {
                if (jump == true && controller.collisions.below)
                {
                    carroVelocity.y = maxJumpHeight;
                }

                float targetVelocityX = input.x * carroMoveSpeed;
                carroVelocity.x = Mathf.SmoothDamp(carroVelocity.x, targetVelocityX, ref carroVelocityXSmoothing, (controller.collisions.below) ? carroAccelerationTimeGrounded : carroAccelerationTimeAirborne);
                carroVelocity.y += gravity * Time.deltaTime;
                controller.Move(carroVelocity * Time.deltaTime, input);
                triggerController.MoveDirection(carroVelocity);

                if (controller.collisions.above || controller.collisions.below)
                {
                    carroVelocity.y = 0;
                }

            }
            if (pipaActive.Value == true)
            {
                float targetVelocityX = input.x * pipaMoveSpeed;
                pipaVelocity.x = Mathf.SmoothDamp(pipaVelocity.x, targetVelocityX, ref pipaVelocityXSmoothing, pipaAccelerationTimeAirborne);

                if (input.y >= 0)
                {
                    pipaVelocity.y += pipaGravity * Time.deltaTime;
                }
                else
                {
                    pipaVelocity.y -= pipaGravity * Time.deltaTime;
                }

                triggerController.MoveDirection(pipaVelocity);
                controller.Move(pipaVelocity * Time.deltaTime, input);
            }
        }
    }

    public void Jump()
    {
        if (controller.collisions.below)
        {
            jump = true;

        }
    }

    public void StopJump()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
            Debug.Log(velocity.y);
        }
        jump = false;
    }
}
