using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Complete;

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
    public InputController inputController;

    public GameObject player;
    public GameObject dog;

    public UnityEngine.Transform dogPosInicial;

    public float delay =0.1f;
    bool isMoving;
    bool isJumping;
    float jumpTimes;
    bool isDown;
    bool isFar;
    public bool trick;

    private Animator anim;
    
    DogAnim dogAnim;

    public BoolVariable buildPC;
    public int trickIndex;

    #region Unity Function

    void Start()
    {
        buildPC = Resources.Load<BoolVariable>("BuildPC");
        anim = GetComponent<Animator>();
        dogAnim = GetComponent<DogAnim>();
        dogController = GetComponent<AIController2D>();
        inputController = FindObjectOfType<InputController>();

        CalculateJumpStats();
    }

    void Update()
    {
        if (GameManager.pausaJogo) return;
        joyInput = inputController.joyInput;

        ShouldSlowFall();
        ShouldJump();
        CheckIfIsFar();
        ShouldRotate();
        ShouldStopMove();
        UpdateMoveAndAnim();
    }

    #endregion

    #region Public Functions

    public void DogJump()
    {
        isJumping = true;
        if (jumpTimes > 0)
        {
            velocity.y = maxJumpHeight + (jumpTimes * 5f);
        }
        else
        {
            velocity.y = maxJumpHeight;
        }
    }

    #endregion

    #region Private Functions
    void CalculateJumpStats()
    {
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void ShouldSlowFall()
    {
        if (playerMove.slowFall == false){ velocity.y += gravity * Time.deltaTime;}
        else{ velocity.y = -3.5f; }
    }

    void ShouldJump()
    {
        if (playerMove.jump == false)
        {
            if (dogController.collisions.below)
            {
                isJumping = false;
                velocity.y = -0.00001f;
            }
        }
        else
        {
            if (isJumping == false)
            {
                jumpTimes = 0;
                isJumping = true;
                Invoke("DogJump", delay);
            }
        }
    }

    void CheckIfIsFar()
    {
        if (transform.position.y - player.transform.position.y > 1 || transform.position.y - player.transform.position.y < -2 && isJumping == false) { IsFar();}
        else { input.y = joyInput.y;}
    }

    void IsFar()
    {
        if (transform.position.y - player.transform.position.y > 1)
        {
            input.y = -1;
        }

        if (transform.position.y - player.transform.position.y < -2 && isJumping == false)
        {
            DogJump();
            jumpTimes++;

            if (transform.position.x - player.transform.position.x > 0)
            {
                velocity.x = -1 * Mathf.Abs(transform.position.x - player.transform.position.x) - 1;
            }

            if (transform.position.x - player.transform.position.x < 0)
            {
                velocity.x = 1 * Mathf.Abs(transform.position.x - player.transform.position.x) + 1;
            }
        }
    }

    void ShouldRotate()
    {
        if (transform.position.x > player.transform.position.x)
        {
            dog.transform.rotation = Quaternion.Slerp(dog.transform.rotation, Quaternion.Euler(0, 180, 0), 0.5f);

        }
        else if (transform.position.x < player.transform.position.x)
        {
            dog.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.5f);
        }
    }

    void ShouldStopMove()
    {
        if (transform.position.y - player.transform.position.y >= 0)
        {
            jumpTimes = 0;
        }

        if (transform.position.x < player.transform.position.x && joyInput.x < 0f || transform.position.x > player.transform.position.x && joyInput.x > 0f || playerTriggerController.collisions.hitDog)
        {
            velocity.x = 0;
        }
    }

    void UpdateMoveAndAnim()
    {
        if (playerTriggerController.collisions.caixaDagua)
        {
            velocity.y = maxJumpHeight * 1.8f;
        }

        targetVelocityX = joyInput.x * speed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (dogController.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        dogController.Move(velocity * Time.deltaTime, input);
        dogAnim.ChangeDogAnim(velocity, joyInput);
    }
    #endregion

}
