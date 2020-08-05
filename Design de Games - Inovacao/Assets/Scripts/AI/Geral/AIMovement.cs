using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
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

        public bool isJumping;

        float jumpTimes;


        private StateController controller;
        //public TriggerCollisionInfo collisions;
        public AIController2D aiController2D;
        public AITriggerController triggerController;
        public AnimationsAI animAI;
        public Rigidbody2D rbBola;
        public GameObject ai;
        private BotStates botStates;

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

        public enum State { Coleta, Corrida, Futebol, Volei, Moto, Stop }
        public State state;
        bool actionIsOn;

        // Start is called before the first frame update
        public void Start()
        {
            animAI = GetComponent<AnimationsAI>();
            controller = GetComponent<StateController>();
            aiController2D = GetComponent<AIController2D>();
            triggerController = GetComponent<AITriggerController>();
            botStates = GetComponent<BotStates>();

            //target = controller.wayPointList[0];

            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

            aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
            playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");

            //aiGanhou.Value[indexDaFase] = false;

        }

        private void Update()
        {
            botStates.BotWork();
            triggerController.RayTriggerDirection();


            if (levouDogada == false)
            {
                targetVelocityX = input.x * speed;
            }

            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (aiController2D.collisions.below) ? 0.1f : 0.2f);

            velocity.y += gravity * Time.deltaTime;

            if (aiController2D.collisions.below == true || aiController2D.collisions.climbingSlope || aiController2D.collisions.descendingSlope)
            {
                isJumping = false;
                velocity.y = 0;
            }
            if (triggerController.triggerCollision.caixaDagua)
            {
                velocity.y = maxJumpHeight * 1.8f;
            }
            aiController2D.Move(velocity * Time.deltaTime, input);
            //animAI.ChangeAnimAI(velocity * Time.deltaTime, oldPosition, input, isJumping);
        }

        public void Move(BotStates.State horizontalState, BotStates.State verticalState,bool actionOn)
        {
            actionIsOn = actionOn;
            if (!horizontalState.Equals(BotStates.State.Null))
            {
                switch (horizontalState)
                {
                    case BotStates.State.Right:
                        GoRight();
                        break;

                    case BotStates.State.Left:
                        GoLeft();
                        break;
                }
            }

            if (!verticalState.Equals(BotStates.State.Null))
            {
                switch (verticalState)
                {
                    case BotStates.State.Up:
                        AIJump();
                        break;

                    case BotStates.State.Down:
                        GoDown();
                        break;
                }
            }
        }



        private void LateUpdate()
        {
            oldPosition = velocity;
        }

        public void Stop()
        {
            if (!GameManager.Instance.fase.Equals(GameManager.Fase.Moto) && !actionIsOn)
            {
                animAI.AnimState("Idle");
            }
            jumpTimes = 0;
            input.x = 0;
        }

        public void GoRight()
        {
            if (!GameManager.Instance.fase.Equals(GameManager.Fase.Moto) && !actionIsOn)
            {
                animAI.AnimState("Walking");
            }
            // jumpTimes = 0;
            input.x = 1;
            dirDir = true;
            Quaternion direction = Quaternion.Euler(0, 0, 0);
            transform.rotation = direction;
        }

        public void GoLeft()
        {
            if (!GameManager.Instance.fase.Equals(GameManager.Fase.Moto) && !actionIsOn)
            {
                animAI.AnimState("Walking");
            }
            //jumpTimes = 0;
            input.x = 1;
            dirDir = false;
            Quaternion direction = Quaternion.Euler(0, 180, 0);
            transform.rotation = direction;
        }

        public void AIJump()
        {
            if (isJumping == false)
            {
                jumpTimes++;
                input.y = 1;
                isJumping = true;
                if (!GameManager.Instance.fase.Equals(GameManager.Fase.Moto) && !actionIsOn)
                {
                    animAI.AnimState("NoArUp");
                }
                if (jumpTimes > 2)
                {
                    velocity.y = maxJumpHeight + (jumpTimes * 1.5f);
                    jumpTimes = 0;
                }
                else
                {
                    velocity.y = maxJumpHeight;
                }
            }
        }

        public void GoDown()
        {
            input.y = -1;
        }

        public IEnumerator LevouDogada()
        {
            levouDogada = true;
            yield return new WaitForSeconds(3f);
            levouDogada = false;
        }


    }
}
