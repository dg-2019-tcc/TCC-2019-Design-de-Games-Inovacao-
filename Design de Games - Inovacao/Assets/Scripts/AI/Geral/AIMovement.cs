using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float speed = 3f;
    public float jumpVelocity = 1f;
    public bool isDown;

    float targetVelocityX;
    float velocityXSmoothing;

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
    public AnimationsAI animAI;
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

    public BoolVariableArray aiGanhou;
	public int indexDaFase;
    public BoolVariable playerGanhou;

    public Vector2 oldPosition;

    private float targetDist;

    // Start is called before the first frame update
    public void Start()
	{
        animAI = GetComponent<AnimationsAI>();
		controller = GetComponent<StateController>();
        aiController2D = GetComponent<AIController2D>();
        triggerController = GetComponent<AITriggerController>();

        target = controller.wayPointList[0];

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");
		
		aiGanhou.Value[indexDaFase] = false;
		
        
    }

    public void FixedUpdate()
    {
        if (aiGanhou.Value[indexDaFase]  == true|| playerGanhou.Value == true)
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

        triggerController.RayTriggerDirection();

        if (aiController2D.collisions.below == true || aiController2D.collisions.climbingSlope || aiController2D.collisions.descendingSlope)
        {
            isJumping = false;
            velocity.y = 0;
        }

        if (triggerController.triggerCollision.caixaDagua)
        {
            velocity.y = maxJumpHeight * 1.8f;
        }

        if (isVolei)
        {
            Volei();
        }

        if (isFut)
        {
            Futebol();
        }

        if (isCorrida || isMoto)
        {
            MotoCorrida();

        }

        if (isColteta)
        {
            Coleta();
        }

        targetVelocityX = input.x * speed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (aiController2D.collisions.below) ? 0.1f : 0.2f);

        velocity.y += gravity * Time.deltaTime;
        aiController2D.Move(velocity * Time.deltaTime, input);
        if (!isMoto)
        {
            animAI.ChangeAnimAI(velocity, oldPosition, input, isJumping);
        }

    }

    private void Coleta()
    {
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
    }

    private void Futebol()
    {
        if (triggerController.triggerCollision.botArea == true)
        {
            transform.position = target.transform.position;
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
    }

    private void Volei()
    {
        if (triggerController.triggerCollision.botArea == true)
        {
            target = controller.wayPointList[1];
            transform.position = target.transform.position;
        }

        targetDist = transform.position.x - target.transform.position.x;

        if (targetDist > 1.5f)
        {
            Stop();
        }

        else
        {
            if (targetDist > 0.5f)
            {
                GoLeft();
            }

            else if (targetDist < -0.5f)
            {
                GoRight();
            }

            else if (triggerController.triggerCollision.isUp && isJumping == false)
            {
                AIJump();
            }
        }
    }

    private void MotoCorrida()
    {
        if (aiController2D.collisions.acabouCorrida) return;
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
    }

    private void LateUpdate()
    {
        oldPosition = velocity;
    }

    public void Stop()
    {
        Debug.Log("Stop");
        jumpTimes = 0;
        input.x = 0;
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
