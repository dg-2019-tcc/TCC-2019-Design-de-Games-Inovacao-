using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Controller2D))]
public class NewPlayerMovent : MonoBehaviour
{
    public FloatVariable moveSpeed;
    float velocityXSmoothing;
    float accelerationTimeAirborne = 0.35f;
    float accelerationTimeGrounded = 0.175f;

    float maxJumpVelocity;
    float minJumpVelocity;
    float gravity;
    public FloatVariable maxJumpHeight;
    public FloatVariable minJumpHeight;
    public FloatVariable timeToJumpApex;

    float pipaMoveSpeed = 6;
    float pipaVelocityXSmoothing;
    float pipaAccelerationTimeAirborne = 0.4f;
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
    bool stopJump;

    public BoolVariable carroActive;
    public BoolVariable pipaActive;
    public BoolVariable levouDogada;

    Vector3 velocity;
    Vector3 carroVelocity;
    Vector3 pipaVelocity;
    Vector3 motoVelocity;

    public Vector2 oldPosition;
    Vector2 input;
    Vector2 joyInput;

    Controller2D controller;
    TriggerCollisionsController triggerController;
    Player2DAnimations animations;

    [SerializeField]
    public Joystick joyStick;


    private PhotonView pv;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        triggerController = GetComponent<TriggerCollisionsController>();
        animations = GetComponent<Player2DAnimations>();

        gravity = -(2 * maxJumpHeight.Value) / Mathf.Pow(timeToJumpApex.Value, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex.Value);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight.Value);

        pv = GetComponent<PhotonView>();
        joyStick = FindObjectOfType<Joystick>();

        //Utilizado para fazer os sons dos passos tocarem
        InvokeRepeating("CallFootsteps", 0, 0.25f);
    }

    void Update()
    {
        if (!pv.IsMine && PhotonNetwork.InRoom) return;
        if (levouDogada.Value) return;
        joyInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);

        if (carroActive.Value == false && pipaActive.Value == false)
        {
            if (joyInput.x > 0.3f || joyInput.x < -0.3f)
            {
                input.x = joyInput.x;
            }

            else
            {
                input.x = 0;
            }

            if (jump || stopJump)
            {
                animations.ChangeMoveAnim(velocity, oldPosition, input, jump, stopJump);
            }

            input.y = joyInput.y;
            float targetVelocityX = input.x * moveSpeed.Value;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);


            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime, input);
            triggerController.MoveDirection(velocity);
            animations.ChangeMoveAnim(velocity, oldPosition, input, jump, stopJump);
            if (controller.collisions.above || controller.collisions.below)
            {
                if (stopJump == true)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Queda", GetComponent<Transform>().position);
                }
                velocity.y = 0;
                jump = false;
                stopJump = false;
            }

            if (/*controller.collisions.below &&*/ triggerController.collisions.caixaDagua)
            {
                velocity.y = maxJumpHeight.Value * 1.8f ;

            }


        }

        else
        {

            input = joyInput;

            if (carroActive.Value == true)
            {
                if (jump == true && controller.collisions.below)
                {
                    carroVelocity.y = maxJumpHeight.Value;
                }

                float targetVelocityX = input.x * carroMoveSpeed;
                carroVelocity.x = Mathf.SmoothDamp(carroVelocity.x, targetVelocityX, ref carroVelocityXSmoothing, (controller.collisions.below) ? carroAccelerationTimeGrounded : carroAccelerationTimeAirborne);
                carroVelocity.y += gravity * Time.deltaTime;
                controller.Move(carroVelocity * Time.deltaTime, input);
                triggerController.MoveDirection(carroVelocity);
                animations.ChangeMoveAnim(velocity, oldPosition, input, jump, stopJump);

                if (controller.collisions.above || controller.collisions.below)
                {
                    carroVelocity.y = 0;
                    stopJump = false;
                }

                if (/*controller.collisions.below &&*/ triggerController.collisions.caixaDagua)
                {
                    velocity.y = maxJumpHeight.Value * 1.8f;

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
                animations.ChangeMoveAnim(velocity, oldPosition, input, jump, stopJump);
            }
        }
    }
    private void LateUpdate()
    {
        oldPosition = velocity;
    }

    public void Jump()
    {
        jump = true;
        //animations.ChangeMoveAnim(velocity, oldPosition, input, jump, stopJump);
        /*if (animations.state != Player2DAnimations.State.Chutando)
        {
            animations.StartPulo();
        }*/
        //stopJump = false;
        if (controller.collisions.below && jump /*&& !stopJump*/)
        {
            velocity.y = maxJumpHeight.Value;
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Pulo", GetComponent<Transform>().position);
        }
    }

    public void StopJump()
    {
        stopJump = true;
        //jump = false;
        //animations.ChangeMoveAnim(velocity, oldPosition, input, jump, stopJump);
        //animations.TransitionAir();
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
            //Debug.Log(velocity.y);
        }
        jump = false;
    }

    public void CallFootsteps()
    {
        if (carroActive.Value == false && pipaActive.Value == false)
        {
            if (joyInput.x > 0.3f || joyInput.x < -0.3f)
            {
                if (controller.collisions.below && jump == false)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Passos", GetComponent<Transform>().position);
                }
            }
        }
    }
}
