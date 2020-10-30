using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Scene;
using AI;

namespace Complete {
    public class AIMovement : MonoBehaviour
    {
        public float speed = 3f;
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

        public AIController2D aiController2D;
        public AITriggerController triggerController;
        public AnimationsAI animAI;
        private BotStates botStates;

        public bool isColteta;
        public bool isFut;
        public bool isCorrida;
        public bool isMoto;
        public bool isVolei;
        public bool dirDir;
        public bool levouDogada;
        public Vector2 animDir;

        public enum State { Coleta, Corrida, Futebol, Volei, Moto, Stop }
        public State state;
        bool actionIsOn;

        #region Unity Function
        public void Start()
        {
            animAI = GetComponent<AnimationsAI>();
            aiController2D = GetComponent<AIController2D>();
            triggerController = GetComponent<AITriggerController>();
            botStates = GetComponent<BotStates>();


            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        }

        private void Update()
        {
            if (GameManager.pausaJogo) return;
            botStates.BotWork();
            triggerController.RayTriggerDirection();
            UpdateMove();
        }
        #endregion

        #region Public Functions

        public void Move(BotStates.State horizontalState, BotStates.State verticalState, bool actionOn)
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

        #endregion

        #region Private Functions

        void UpdateMove()
        {
            if (levouDogada == false) { targetVelocityX = input.x * speed;}

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
        }

        private void Stop()
        {
            jumpTimes = 0;
            input.x = 0;
        }

        private void GoRight()
        {
            animDir = new Vector2(1, 0);
            if (GameManager.sceneAtual != SceneType.Moto && !actionIsOn)
            {
                animAI.ChangeAnimAI(animDir);
            }
            input.x = 1;
            dirDir = true;
            Quaternion direction = Quaternion.Euler(0, 0, 0);
            transform.rotation = direction;
        }

        private void GoLeft()
        {
            animDir = new Vector2(1, 0);
            animAI.ChangeAnimAI(animDir);
            input.x = 1;
            dirDir = false;
            Quaternion direction = Quaternion.Euler(0, 180, 0);
            transform.rotation = direction;
        }

        private void AIJump()
        {
            if (isJumping == false)
            {
                animDir = new Vector2(0, 1);

                if (GameManager.sceneAtual != SceneType.Moto && !actionIsOn)
                {
                    animAI.ChangeAnimAI(animDir);
                }

                jumpTimes++;
                input.y = 1;
                isJumping = true;

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

        private void GoDown()
        {
            animDir = new Vector2(0, -1);
            animAI.ChangeAnimAI(animDir);
            input.y = -1;
        }

        private IEnumerator LevouDogada()
        {
            levouDogada = true;
            yield return new WaitForSeconds(3f);
            levouDogada = false;
        }

        #endregion
    }
}
