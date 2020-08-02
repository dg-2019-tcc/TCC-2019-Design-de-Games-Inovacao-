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
	float motoVelocityXSmoothing;
	float motoAccelerationTimeAirborne = 0f;
	float motoAccelerationTimeGrounded = 0.05f;

	float motoMaxJumpVelocity;
	float motoMinJumpVelocity;
	float motoGravity;
	public float motoMaxJumpHeight = 4;
	public float motoMinJumpHeight = 2;
	public float motoTimeToJumpApex = 0.4f;

	bool jump;
	bool stopJump;
    bool subindo;

	public Vector2 oldPosition;

    [HideInInspector]
	public Vector3 velocity;
	Vector3 motoVelocity;

	public Controller2D controller;
    private InputController inputController;
    Vector2 input;


    TriggerCollisionsController triggerController;
	PlayerMotoAnimation animations;

	[SerializeField]
	public Joystick joyStick;

    public BoolVariable levouDogada;
    public BoolVariable playerGanhou;

    private PhotonView pv;


    void Start()
	{
		controller = GetComponent<Controller2D>();
        inputController = GetComponent<InputController>();
		triggerController = GetComponent<TriggerCollisionsController>();
		animations = GetComponent<PlayerMotoAnimation>();

        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");
        playerGanhou.Value = false;

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

		pv = GetComponent<PhotonView>();
		joyStick = FindObjectOfType<Joystick>();
	}

	void Update()
	{
        if (GameManager.pausaJogo == true) { return; }
        if (!pv.IsMine && PhotonNetwork.InRoom) return;
        //if (levouDogada.Value) return;
        if (playerGanhou.Value) return;

        input = inputController.joyInput;

	    float targetVelocityX = (motoSpeedChange.Value + motoMoveSpeed);
        //float targetVelocityX = (Mathf.Clamp(input.x, -1, 0)+1) * (moveSpeed.Value + motoMoveSpeed);
        if (levouDogada.Value == false)
        {
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        }

        else
        {
            velocity.x = 0;
        }

		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
		triggerController.MoveDirection(velocity);
		animations.ChangeMotoAnim(velocity, oldPosition, levouDogada.Value);

		if (controller.collisions.above || controller.collisions.below)
		{
			velocity.y = 0;
		}

        if (inputController.pressX == true && controller.collisions.below)
        {
            if (controller.collisions.climbingSlope)
            {
                //Debug.Log("Pulo");
                velocity.y = maxJumpHeight + 10f;
            }
            else
            {
                velocity.y = maxJumpHeight;
            }
        }


    }
	private void LateUpdate()
	{
		oldPosition = velocity; 
	}

	public void Jump()
	{
		if (controller.collisions.below && levouDogada.Value == false)
		{
            if (controller.collisions.climbingSlope)
            {
                //Debug.Log("Pulo");
                velocity.y = maxJumpHeight + 10f;
            }
            else
            {
                velocity.y = maxJumpHeight;
            }
        }
	}

	public void StopJump()
	{
		if (velocity.y > minJumpVelocity)
		{
			velocity.y = minJumpVelocity;
		}
		jump = false;
	}

    IEnumerator CaiuMoto()
    {
        yield return new WaitForSeconds(1.5f);

    }
}
