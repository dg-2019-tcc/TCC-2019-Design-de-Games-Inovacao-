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

    bool jump;

    Vector3 velocity;

    Controller2D controller;

    [SerializeField]
    public Joystick joyStick;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2* Mathf.Abs(gravity)*minJumpHeight);

        joyStick = FindObjectOfType<Joystick>();
    }

    void Update()
    {

        Vector2 input = new Vector2(joyStick.Horizontal, joyStick.Vertical);

        if (jump == true && controller.collisions.below)
        {
            velocity.y = maxJumpHeight;
        }


        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, input);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
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
