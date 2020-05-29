using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public float speed;
    float velocityXSmoothing;
    float targetVelocityX;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;

    float maxJumpVelocity;
    float minJumpVelocity;
    float gravity;
    public float maxJumpHeight;
    public float minJumpHeight;
    public float timeToJumpApex;

    Vector3 velocity;
    Vector2 input;
    Vector2 joyInput;

    [SerializeField]
    public FloatingJoystick joyStick;

    AIController2D dogController;

    public NewPlayerMovent playerMove;
    public TriggerCollisionsController playerTriggerController;

    public GameObject player;
    public GameObject dog;

    public Transform dogPosInicial;

    public float delay =0.1f;

    bool isMoving;
    bool isJumping;

    float jumpTimes;


    void Start()
    {
        dogController = GetComponent<AIController2D>();

        joyStick = FindObjectOfType<FloatingJoystick>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }




    void LateUpdate()
    {
        joyInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);
        //joyInput = playerMove.joyInput;
        if (joyInput.x > 0.3f || joyInput.x < -0.3f)
        {
            input.x = joyInput.x;
        }

        else
        {
            input.x = 0;
        }

        targetVelocityX = input.x * speed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (dogController.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        if (playerMove.slowFall == false)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -3.5f;
        }

        if (dogController.collisions.below && playerMove.jump && isJumping == false)
        {
            //DogJump();
            isJumping = true;
            Invoke("DogJump", delay);
        }

        if (dogController.collisions.above || dogController.collisions.below)
        {
            isJumping = false;
            velocity.y = 0;
        }

        if (transform.position.x > player.transform.position.x)
        {
            dog.transform.rotation = Quaternion.Slerp(dog.transform.rotation, Quaternion.Euler(0, 180, 0), 0.5f);

        }
        else if (transform.position.x < player.transform.position.x)
        {
            dog.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.5f);
        }

        if(transform.position.y - player.transform.position.y > 1)
        {
            input.y = -1;
        }

        else
        {
            input.y = joyInput.y;
        }

        if(transform.position.y - player.transform.position.y < -2 && isJumping == false)
        {
            DogJump();
            jumpTimes++;
        }

        if(transform.position.y - player.transform.position.y > 0)
        {
            jumpTimes = 0;
        }

        if (transform.position.x < player.transform.position.x && joyInput.x < 0f ||transform.position.x > player.transform.position.x && joyInput.x > 0f || playerTriggerController.collisions.hitDog)
        {
            velocity.x = 0;
        }
        dogController.Move(velocity * Time.deltaTime, input);
    }



    public void DogJump()
    {
        isJumping = true;
        if (jumpTimes > 1)
        {
            velocity.y = maxJumpHeight + (jumpTimes * 5f);
        }
        else
        {
            velocity.y = maxJumpHeight;
        }
    }
}
