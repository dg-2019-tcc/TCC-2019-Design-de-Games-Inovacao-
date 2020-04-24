using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class NewMotoPlayerMovement : MonoBehaviour
{
	public FloatVariable moveSpeed;
	float velocityXSmoothing;
	float accelerationTimeAirborne = 0.1f;
	float accelerationTimeGrounded = 0.05f;

	float maxJumpVelocity;
	float minJumpVelocity;
	float gravity;
	public float maxJumpHeight = 4;
	public float minJumpHeight = 2;
	public float timeToJumpApex = 0.4f;
	
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
	Vector3 motoVelocity;

	public Controller2D controller;

	TriggerCollisionsController triggerController;

	[SerializeField]
	public Joystick joyStick;

	void Start()
	{
		controller = GetComponent<Controller2D>();
		triggerController = GetComponent<TriggerCollisionsController>();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

		joyStick = FindObjectOfType<Joystick>();
	}

	void Update()
	{

		Vector2 input = new Vector2(joyStick.Horizontal, joyStick.Vertical);

			if (jump == true && controller.collisions.below)
			{
				velocity.y = maxJumpHeight;
			}

			float targetVelocityX = (Mathf.Clamp(input.x, -1, 0)+1) * (moveSpeed.Value + motoMoveSpeed);
			velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
			velocity.y += gravity * Time.deltaTime;
			controller.Move(velocity * Time.deltaTime, input);
			triggerController.MoveDirection(velocity);
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
