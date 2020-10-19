using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Complete;

[RequireComponent(typeof(Controller2D))]
public class NewPlayerMovent : MonoBehaviour
{
    // Movimentação Normal
    public FloatVariable moveSpeed;
    float velocityXSmoothing;
    public FloatVariable accelerationAir;
    public FloatVariable accelerationGround;
    float targetVelocityX;
    float maxJumpVelocity;
    float minJumpVelocity;
    float gravity;
    float slowGravity;
    public FloatVariable maxJumpHeight;
    public FloatVariable minJumpHeight;
    public FloatVariable timeToJumpApex;

    //Move Pipa
    float pipaMoveSpeed = 6;
    float pipaVelocityXSmoothing;
    float pipaAccelerationTimeAirborne = 0.3f;
    public float pipaGravity;

    //Move Carro
    float carroMoveSpeed = 12;
    float carroVelocityXSmoothing;
    float carroAccelerationTimeAirborne = 0.35f;
    float carroAccelerationTimeGrounded = 0.175f;

    //Jump
    [HideInInspector]
    public bool jump;
    bool stopJump;

    public BoolVariable carroActive;
    public BoolVariable pipaActive;
    public BoolVariable levouDogada;


    [HideInInspector] public Vector2 velocity;
    [HideInInspector] public Vector3 carroVelocity;
    Vector3 pipaVelocity;

    [HideInInspector]
    public Vector2 input;
    [HideInInspector]
    public Vector2 joyInput;

    //Controllers
    Controller2D controller;
    public Controller2D dogController;
    TriggerCollisionsController triggerController;
    Player2DAnimations animations;
    public InputController inputController;


    private PhotonView pv;

    public BoolVariable playerGanhou;
    public BoolVariable textoAtivo;

    public bool slowFall;
    public PlayerAnimInfo playerAnimInfo;

    #region Unity Function
    void Start()
    {
        inputController = GetComponent<InputController>();
        controller = GetComponent<Controller2D>();
        triggerController = GetComponent<TriggerCollisionsController>();
        animations = GetComponent<Player2DAnimations>();
        playerAnimInfo = GetComponent<PlayerAnimInfo>();

        gravity = -(2 * maxJumpHeight.Value) / Mathf.Pow(timeToJumpApex.Value, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex.Value);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight.Value);

        slowGravity = gravity * 0.5f;

        pv = GetComponent<PhotonView>();

        //Utilizado para fazer os sons dos passos tocarem
        InvokeRepeating("CallFootsteps", 0, 0.3f);

        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");
        textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");
        playerGanhou.Value = false;

        if (moveSpeed.Value != 6)
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

        joyInput = inputController.joyInput;



        if (!carroActive.Value && !pipaActive.Value)
        {
            NormalMovement();

        }

        else
        {

            input = joyInput;

            if (carroActive.Value)
            {
                CarroMovement();
            }
            if (pipaActive.Value)
            {
                PipaMovement();
            }
        }
    }

    private void LateUpdate()
    {
        joyInput = new Vector2(0, 0);
    }
    #endregion

    #region Public Functions
    public void Jump()
    {
        jump = true;

        if (controller.collisions.below && jump && levouDogada.Value == false)
        {
            if (carroActive.Value)
            {
                carroVelocity.y = maxJumpVelocity;
            }

            else
            {
                velocity.y = maxJumpVelocity;
            }
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/PuloGr", transform.position);
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/PuloPq", transform.position);
    }

    public void CallFootsteps()
    {
        if ((!carroActive.Value && !pipaActive.Value) && (velocity.x > 0.3f || velocity.x < -0.3f) && (controller.collisions.below && jump == false) && (textoAtivo.Value == false))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Passos", transform.position);
        }
    }
    #endregion

    #region Private Functions
    private void NormalMovement()
    {
        if (triggerController.collisions.caixaDagua)
        {
            velocity.y = maxJumpVelocity * 2f;
        }

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

        controller.Move(velocity * Time.deltaTime);
        triggerController.MoveDirection(velocity * Time.deltaTime);



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
        #region PC Pulo
        //Para PC
        if (inputController.releaseX == true)
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

        if (inputController.pressX == true && controller.collisions.below)
        {
            //animDB.CallAnimState04(AnimState04.Rising);
            jump = true;
            velocity.y = maxJumpVelocity;
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/PuloGr", transform.position);
        }
        #endregion

        playerAnimInfo.UpdateInfo04(velocity, input, controller.collisions.below, jump);
    }

    private void CarroMovement()
    {
        if (inputController.pressX == true && controller.collisions.below)
        {
            jump = true;
            carroVelocity.y = maxJumpVelocity;
        }


        targetVelocityX = input.x * carroMoveSpeed;
        carroVelocity.x = Mathf.SmoothDamp(carroVelocity.x, targetVelocityX, ref carroVelocityXSmoothing, (controller.collisions.below) ? carroAccelerationTimeGrounded : carroAccelerationTimeAirborne);
        carroVelocity.y += gravity * Time.deltaTime;
        controller.Move(carroVelocity * Time.deltaTime);
        triggerController.MoveDirection(carroVelocity);

        if (controller.collisions.above || controller.collisions.below)
        {
            carroVelocity.y = 0;
            jump = false;
            stopJump = false;
        }

        if (triggerController.collisions.caixaDagua)
        {
            jump = true;
            carroVelocity.y = maxJumpVelocity * 1.8f;
        }
    }

    private void PipaMovement()
    {
        targetVelocityX = input.x * pipaMoveSpeed;
        pipaVelocity.x = Mathf.SmoothDamp(pipaVelocity.x, targetVelocityX, ref pipaVelocityXSmoothing, pipaAccelerationTimeAirborne);

        if (input.y >= 0)
        {
            if (pipaVelocity.y < 5)
            {
                pipaVelocity.y += pipaGravity * Time.deltaTime;
            }
            else
            {
                pipaVelocity.y = 5;
            }
        }
        else
        {
            if (pipaVelocity.y > -5)
            {
                pipaVelocity.y -= pipaGravity * Time.deltaTime;
            }
            else
            {
                pipaVelocity.y = -5;
            }
        }


        triggerController.MoveDirection(pipaVelocity);
        controller.Move(pipaVelocity * Time.deltaTime);
    }
    #endregion
}
