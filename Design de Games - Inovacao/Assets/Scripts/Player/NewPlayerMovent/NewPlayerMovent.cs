﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Controller2D))]
public class NewPlayerMovent : MonoBehaviour
{
    public FloatVariable moveSpeed;
    float velocityXSmoothing;
    public FloatVariable accelerationAir;
    public FloatVariable accelerationGround;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;

    float targetVelocityX;

    float maxJumpVelocity;
    float minJumpVelocity;
    float gravity;
    float slowGravity;
    public FloatVariable maxJumpHeight;
    public FloatVariable minJumpHeight;
    public FloatVariable timeToJumpApex;

    float pipaMoveSpeed = 6;
    float pipaVelocityXSmoothing;
    float pipaAccelerationTimeAirborne = 0.3f;
    public float pipaGravity;

    float carroMoveSpeed = 12;
    float carroVelocityXSmoothing;
    float carroAccelerationTimeAirborne = 0.35f;
    float carroAccelerationTimeGrounded = 0.175f;


    [HideInInspector]
    public bool jump;
    bool stopJump;

    public BoolVariable carroActive;
    public BoolVariable pipaActive;
    public BoolVariable levouDogada;

    [HideInInspector]
    public Vector3 velocity;
    Vector3 carroVelocity;
    Vector3 pipaVelocity;
    Vector3 motoVelocity;

    public Vector3 oldPosition;
    Vector2 input;
    Vector2 oldInput;
    [HideInInspector]
    public Vector2 joyInput;

    Controller2D controller;
    public Controller2D dogController;
    TriggerCollisionsController triggerController;
    Player2DAnimations animations;

    [SerializeField]
    public FloatingJoystick joyStick;


    private PhotonView pv;

    public BoolVariable aiGanhou;
    public BoolVariable playerGanhou;
    public BoolVariable textoAtivo;

    public bool slowFall;

    public bool ganhou;
    private bool oldStun;


    void Start()
    {
        controller = GetComponent<Controller2D>();
        triggerController = GetComponent<TriggerCollisionsController>();
        animations = GetComponent<Player2DAnimations>();

        gravity = -(2 * maxJumpHeight.Value) / Mathf.Pow(timeToJumpApex.Value, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex.Value);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight.Value);

        slowGravity = gravity * 0.5f;

        pv = GetComponent<PhotonView>();
        joyStick = FindObjectOfType<FloatingJoystick>();

        //Utilizado para fazer os sons dos passos tocarem
        InvokeRepeating("CallFootsteps", 0, 0.3f);

        aiGanhou = Resources.Load<BoolVariable>("AIGanhou");
        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");
        textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");
        playerGanhou.Value = false;

        if(moveSpeed.Value != 6)
        {
            moveSpeed.Value = 6;
        }
    }

    void Update()
    {
        if (moveSpeed.Value != 6)
        {
            moveSpeed.Value = 6;
        }

        if (playerGanhou.Value || !pv.IsMine && PhotonNetwork.InRoom || textoAtivo.Value)
		{
			return;
		}
        if (joyStick == null)
        {
            joyStick = FindObjectOfType<FloatingJoystick>();
        }


        joyInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);

        if (!carroActive.Value && !pipaActive.Value)
        {
			input.x = 0;
			if (Mathf.Abs(joyInput.x) > 0.3f)
            {
                input.x = joyInput.x;
            }

            input.y = joyInput.y;
            targetVelocityX = input.x * moveSpeed.Value;
            if (levouDogada.Value == false)
            {
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationGround.Value : accelerationAir.Value);
            }
            else
            {
                velocity.x = 0;
            }


            if (!triggerController.collisions.slowTime)
            {
                slowFall = false;
                velocity.y += gravity * Time.deltaTime;
            }

            else
            {
                slowFall = true;
                velocity.y = -5f;
            }

            controller.Move(velocity * Time.deltaTime, input);
            triggerController.MoveDirection(velocity * Time.deltaTime);

            animations.ChangeMoveAnim(velocity, oldPosition, input, levouDogada.Value, ganhou);


            if (controller.collisions.above || controller.collisions.below)
            {
                if (stopJump)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Queda", transform.position);
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

            if (carroActive.Value)
            {
                if (jump && controller.collisions.below)
                {
                    carroVelocity.y = maxJumpHeight.Value;
                }

                float targetVelocityX = input.x * carroMoveSpeed;
                carroVelocity.x = Mathf.SmoothDamp(carroVelocity.x, targetVelocityX, ref carroVelocityXSmoothing, (controller.collisions.below) ? carroAccelerationTimeGrounded : carroAccelerationTimeAirborne);
                carroVelocity.y += gravity * Time.deltaTime;
                controller.Move(carroVelocity * Time.deltaTime, input);
                triggerController.MoveDirection(carroVelocity);

                if (controller.collisions.above || controller.collisions.below)
                {
                    carroVelocity.y = 0;
                    stopJump = false;
                }

                if (/*controller.collisions.below &&*/ triggerController.collisions.caixaDagua)
                {
                    if (triggerController.collisions.direction.x != 0)
                    {
                        velocity.x = maxJumpHeight.Value * 2f * triggerController.collisions.direction.x;
                    }

                    else
                    {
                        velocity.y = maxJumpHeight.Value * 1.5f * triggerController.collisions.direction.y;
                    }

                }

            }
            if (pipaActive.Value)
            {
                float targetVelocityX = input.x * pipaMoveSpeed;
                pipaVelocity.x = Mathf.SmoothDamp(pipaVelocity.x, targetVelocityX, ref pipaVelocityXSmoothing, pipaAccelerationTimeAirborne);
                if (/*controller.collisions.below &&*/ triggerController.collisions.caixaDagua)
                {
                    if (triggerController.collisions.direction.x != 0)
                    {
                        Debug.Log(triggerController.collisions.direction.x);
                        velocity.x = maxJumpHeight.Value * 2f * triggerController.collisions.direction.x;
                    }

                    else
                    {
                        Debug.Log(triggerController.collisions.direction.y);
                        velocity.y = maxJumpHeight.Value * 1.5f * triggerController.collisions.direction.y;
                    }

                }
                else
                {
                    if (input.y >= 0)
                    {
                        pipaVelocity.y += pipaGravity * Time.deltaTime;
                    }
                    else
                    {
                        pipaVelocity.y -= pipaGravity * Time.deltaTime;
                    }
                }

                triggerController.MoveDirection(pipaVelocity);
                controller.Move(pipaVelocity * Time.deltaTime, input);
            }
        }
    }
    private void LateUpdate()
    {
        oldPosition = velocity;
		joyInput = new Vector2(0, 0);
    }

    public void Jump()
    {
        jump = true;

        if (controller.collisions.below && jump && levouDogada.Value == false)
        {
            velocity.y = maxJumpHeight.Value;
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Pulo", transform.position);
        }
    }

    public void StopJump()
    {
        stopJump = true;

        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
        jump = false;
    }

    public void CallFootsteps()
    {
        if ((!carroActive.Value && !pipaActive.Value) && (velocity.x > 0.3f || velocity.x < -0.3f) && (controller.collisions.below && jump == false) && (textoAtivo.Value == false))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Passos", transform.position);
        }
    }
}