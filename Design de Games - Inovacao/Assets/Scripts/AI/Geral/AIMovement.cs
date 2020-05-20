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


	public bool isBallGame;
	public BoxCollider2D boxCollider;

	public float hitLenght = 5f;

    private ColetavelGerador coletavelGerador;
    private Vector2 coletavelPos;

	//Vector2 botInput;
    public PlatformEffector2D[] effectors;

    public bool isColteta;
    public bool isFut;
    float newVel = 0.01f;
    private bool found;
    public float maxSpeed = 0.5f;

    public bool dirDir;
   
    Transform target;

    public float jumpIndex;

    public bool levouDogada;

    // Start is called before the first frame update
    public void Start()
	{
		controller = GetComponent<StateController>();
        aiController2D = GetComponent<AIController2D>();
        target = controller.wayPointList[0];

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    public void FixedUpdate()
    {
        if (levouDogada)
        {
            return;
        }

        if (isFut)
        {
           
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
                StartCoroutine("Pulo");
            }
            aiController2D.Move(velocity * Time.deltaTime,input);
            triggerController.RayTriggerDirection();
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

            velocity.y += gravity * Time.deltaTime;

            if (found)
            {
                if (aiController2D.collisions.below == true || aiController2D.collisions.climbingSlope || aiController2D.collisions.descendingSlope)
                {
                    isJumping = false;
                }

                if (triggerController.triggerCollision.isRight)
                {
                    GoRight();
                }

                else if (triggerController.triggerCollision.isLeft)
                {
                    GoLeft();
                }

                else if (triggerController.triggerCollision.isUp && isJumping == false)
                {
                    Debug.Log("AtivaPulo");
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
        Debug.Log("Jump");
        input.x = 0;
        jumpTimes++;
        input.y = 1;
        isJumping = true;

        if (jumpTimes > 3)
        {
            velocity.y = maxJumpHeight + (jumpTimes * 2f);
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
