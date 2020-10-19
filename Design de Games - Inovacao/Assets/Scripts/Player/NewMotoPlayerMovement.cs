using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Complete;

[RequireComponent(typeof(Controller2D))]
public class NewMotoPlayerMovement : MonoBehaviour
{
	public FloatVariable moveSpeed;
	public FloatVariable motoSpeedChange;
	float velocityXSmoothing;
	float accelerationTimeAirborne = 0.2f;
	float accelerationTimeGrounded = 0.05f;

	float maxJumpVelocity;
	float minJumpVelocity;
	float gravity;
	public float maxJumpHeight = 4;
	public float minJumpHeight = 2;
	public float timeToJumpApex = 0.4f;
	
	float motoMoveSpeed = 6;

	bool jump;
	bool stopJump;

	public Vector2 oldPosition;

    [HideInInspector]
	public Vector3 velocity;

	Controller2D controller;
    InputController inputController;
    Vector2 input;
    TriggerCollisionsController triggerController;
	PlayerMotoAnimation animations;

	[SerializeField]
	public Joystick joyStick;

    public BoolVariable levouDogada;
    BoolVariable playerGanhou;

    private PhotonView pv;

    #region Unity Function
    void Start()
    {
        controller = GetComponent<Controller2D>();
        inputController = GetComponent<InputController>();
        triggerController = GetComponent<TriggerCollisionsController>();
        animations = GetComponent<PlayerMotoAnimation>();
        pv = GetComponent<PhotonView>();
        joyStick = FindObjectOfType<Joystick>();

        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");
        playerGanhou.Value = false;

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update()
    {
        ShouldUpdate();

        input = inputController.joyInput;

        float targetVelocityX = (motoSpeedChange.Value + motoMoveSpeed);

        if (levouDogada.Value == false)
        {
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        }

        else
        {
            velocity.x = 0;
        }

        velocity.y += gravity * Time.deltaTime;

        if (controller.collisions.above || controller.collisions.below)
        {
            if (!jump) velocity.y = 0;
        }

        PCJump();
        UpdateMove();
    }

    private void LateUpdate()
    {
        oldPosition = velocity;
    }
    #endregion

    #region Public Functions
    public void Jump()
    {
        if (controller.collisions.below && levouDogada.Value == false)
        {
            jump = true;
            if (controller.collisions.climbingSlope)
            {
                //Debug.Log("Pulo");
                velocity.y = maxJumpVelocity + 10f;
                Debug.Log("BIG Jump");
            }
            else
            {
                velocity.y = maxJumpVelocity;
                Debug.Log("Jump");
            }
        }

        Debug.Log("End Jump");
    }

    public void StopJump()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
        jump = false;
    }
    #endregion

    #region Private Functions

    void ShouldUpdate()
    {
        if (GameManager.pausaJogo == true) { return; }
        if (!pv.IsMine && PhotonNetwork.InRoom) return;
        if (playerGanhou.Value) return;
    }

    void PCJump()
    {
        if (inputController.pressX == true && controller.collisions.below)
        {
            if (controller.collisions.climbingSlope)
            {
                velocity.y = maxJumpVelocity + 10f;
            }
            else
            {
                velocity.y = maxJumpVelocity;
            }
        }
    }

    void UpdateMove()
    {
        controller.Move(velocity * Time.deltaTime);
        triggerController.MoveDirection(velocity);
        animations.ChangeMotoAnim(velocity, oldPosition, levouDogada.Value);
    }

    IEnumerator CaiuMoto()
    {
        yield return new WaitForSeconds(1.5f);
    }
    #endregion
}
