using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent (typeof (Controller2D))]
public class NewPlayerMovent : MonoBehaviour
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

    float pipaMoveSpeed = 5;
    float pipaVelocityXSmoothing;
    float pipaAccelerationTimeAirborne = 0.6f;
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

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2* Mathf.Abs(gravity)*minJumpHeight);

		pv = GetComponent<PhotonView>();
        joyStick = FindObjectOfType<Joystick>();
    }

    void Update()
    {
		if (!pv.IsMine && PhotonNetwork.InRoom) return;
		if (levouDogada.Value) return;
		joyInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);

        if (carroActive.Value == false && pipaActive.Value == false)
        {
            if(joyInput.x > 0.3f || joyInput.x < -0.3f)
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
            if (controller.collisions.above ||controller.collisions.below)
            {
                velocity.y = 0;
                jump = false;
                stopJump = false;
            }


        }

        else
        {

            input = joyInput;

            if (carroActive.Value == true)
            {
                if (jump == true && controller.collisions.below)
                {
                    Debug.Log("Jump");
                    carroVelocity.y = maxJumpHeight;
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
            velocity.y = maxJumpHeight;

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
}
