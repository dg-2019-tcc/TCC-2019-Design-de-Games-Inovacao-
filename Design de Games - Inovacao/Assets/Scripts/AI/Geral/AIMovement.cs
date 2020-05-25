using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float speed = 3f;
    public float jumpVelocity = 1f;
    public bool isDown;

    Vector2 input;

    Vector3 velocity;
    float maxJumpVelocity;
    float minJumpVelocity;
    float gravity;
    public float maxJumpHeight;
    public float minJumpHeight;
    public float timeToJumpApex;

    bool isJumping;

    float jumpTimes;


    private StateController controller;
	//public TriggerCollisionInfo collisions;
	public AIController2D aiController2D;
    public AITriggerController triggerController;
    public Rigidbody2D rbBola;
	public GameObject ai;

    public float turnCooldown;


	public BoxCollider2D boxCollider;

	public float hitLenght = 5f;

    private ColetavelGerador coletavelGerador;
    private Vector2 coletavelPos;


    public bool isColteta;
    public bool isFut;
    public bool isCorrida;
    public bool isMoto;
    public bool isVolei;
    float newVel = 0.01f;
    private bool found;
    public float maxSpeed = 0.5f;

    public bool dirDir;
   
    Transform target;

    public float jumpIndex;

    public bool levouDogada;

    public BoolVariable aiGanhou;

    // Start is called before the first frame update
    public void Start()
	{
		controller = GetComponent<StateController>();
        aiController2D = GetComponent<AIController2D>();
        triggerController = GetComponent<AITriggerController>();

        target = controller.wayPointList[0];

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        aiGanhou.Value = false;
    }

    public void FixedUpdate()
    {
        if (aiGanhou.Value)
        {
            return;
        }

        if (levouDogada)
        {
            return;
        }

        if (triggerController.triggerCollision.ganhou)
        {
            return;
        }

        if (isVolei)
        {
            triggerController.RayTriggerDirection();

            if (aiController2D.collisions.below == true)
            {
                isJumping = false;
            }

            if (transform.position.x - target.transform.position.x > 2)
            {
                return;
            }

            else
            {
                if (transform.position.x - target.transform.position.x > 0)
                {
                    GoLeft();
                }

                else if (transform.position.x - target.transform.position.x < 0)
                {
                    GoRight();
                }

                else if (triggerController.triggerCollision.isUp && isJumping == false)
                {
                    AIJump();
                }
            }

            velocity.x = speed * input.x;
            velocity.y += gravity * Time.deltaTime;
            aiController2D.Move(velocity * Time.deltaTime, input);
        }

        if (isFut)
        {
            triggerController.RayTriggerDirection();

            if (aiController2D.collisions.below == true)
            {
                isJumping = false;
            }

            if (transform.position.x - target.transform.position.x > 1)
            {
                GoLeft();
            }

            else if (transform.position.x - target.transform.position.x < -1)
            {
                GoRight();
            }

            else if (triggerController.triggerCollision.isUp && isJumping == false)
            {
                //input.x = 0;
                AIJump();
            }
            velocity.x = speed * input.x;
            velocity.y += gravity * Time.deltaTime;
            aiController2D.Move(velocity * Time.deltaTime,input);
        }

        if (isCorrida || isMoto)
        {
            if (aiController2D.collisions.acabouCorrida) return;
            triggerController.RayTriggerDirection();
            GoRight();

            if (triggerController.triggerCollision.needJump && aiController2D.collisions.below)
            {
                input.x = 1;
                AIJump();
            }

            if (isMoto)
            {
                speed += 0.15f * Time.deltaTime;
            }

            velocity.x = speed;
            velocity.y += gravity * Time.deltaTime;
            aiController2D.Move(velocity * Time.deltaTime, input);
        }

        if (isColteta)
        {
            triggerController.RayTriggerDirection();

            if (triggerController.triggerCollision.isRight || triggerController.triggerCollision.isLeft || triggerController.triggerCollision.isUp || triggerController.triggerCollision.isDown)
            {
                found = true;
            }

            else
            {
                found = false;
            }

            if (found)
            {
                if (aiController2D.collisions.below == true || aiController2D.collisions.climbingSlope || aiController2D.collisions.descendingSlope)
                {
                    isJumping = false;
                    velocity.y = 0;
                }

                if (triggerController.triggerCollision.isRight)
                {
                    GoRight();
                }

                else if (triggerController.triggerCollision.isLeft)
                {
                    GoLeft();
                }

                else if (triggerController.triggerCollision.isUp && isJumping == false && aiController2D.collisions.below == true)
                {
                    input.x = 0;
                    AIJump();
                }

                else if (triggerController.triggerCollision.isDown)
                {
                    input.y = -1;
                    input.x = 0;
                }

            }

            else
            {
                if (aiController2D.collisions.below == true || aiController2D.collisions.climbingSlope || aiController2D.collisions.descendingSlope)
                {
                    velocity.y = 0;
                }

                if (transform.position.x - target.transform.position.x > 1)
                {
                    GoLeft();
                }

                else if (transform.position.x - target.transform.position.x < -1)
                {
                    GoRight();
                }

                else
                {
                    if (target == controller.wayPointList[0])
                    {
                        target = controller.wayPointList[1];
                    }

                    else
                    {
                        target = controller.wayPointList[0];
                    }
                }

            }

            velocity.y += gravity * Time.deltaTime;
            Debug.Log(velocity.y);
            velocity.x = speed * input.x;
            aiController2D.Move(velocity * Time.deltaTime,input);
        }
  
    }

    public void GoRight()
    {
        jumpTimes = 0;
        input.x = 1;
        dirDir = true;
        Quaternion direction = Quaternion.Euler(0, 0, 0);
        transform.rotation = direction;
    }

    public void GoLeft()
    {
        jumpTimes = 0;
        input.x = 1;
        dirDir = false;
        Quaternion direction = Quaternion.Euler(0, 180, 0);
        transform.rotation = direction;
    }

    public void AIJump()
    {
        jumpTimes++;
        input.y = 1;
        isJumping = true;

        if (jumpTimes > 2)
        {
            velocity.y = maxJumpHeight + (jumpTimes * 2f);
            jumpTimes = 0;
        }
        else
        {
            velocity.y = maxJumpHeight;
        }
    }

    public IEnumerator LevouDogada()
    {
        levouDogada = true;
        yield return new WaitForSeconds(3f);
        levouDogada = false;
    }


}
