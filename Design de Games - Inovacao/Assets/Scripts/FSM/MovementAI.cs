using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

namespace AI
{
    public class MovementAI : MonoBehaviour
    {
        #region Variables

        public float speed = 3f;
        public float maxJumpHeight;
        public float minJumpHeight;
        public float timeToJumpApex;
        public bool stun;

        float targetVelocityX;
        float velocityXSmoothing;
        float maxJumpVelocity;
        float minJumpVelocity;
        float gravity;
        Vector2 input;
        Vector3 velocity;

        public AIController2D aiController2D;
        AITriggerController triggerController;

        #endregion

        #region Unity Function

        void Start()
        {
            aiController2D = GetComponent<AIController2D>();
            triggerController = GetComponent<AITriggerController>();

            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        }

        void Update()
        {
            if (GameManager.pausaJogo) return;
            triggerController.RayTriggerDirection();
        }

        #endregion

        #region Public Functions

        public void SetMovement(float _inputX)
        {
            input.x = _inputX;
            if (stun == false) { targetVelocityX = input.x * speed; }

            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (aiController2D.collisions.below) ? 0.1f : 0.2f);
            velocity.y += gravity * Time.deltaTime;

            if ((aiController2D.collisions.below == true || aiController2D.collisions.climbingSlope || aiController2D.collisions.descendingSlope) && aiController2D.collisions.canJump == false)
            {
                velocity.y = 0;
            }
            /*if (triggerController.triggerCollision.caixaDagua)
            {
                velocity.y = maxJumpHeight * 1.8f;
            }*/

            aiController2D.Move(velocity * Time.deltaTime, input);
        }

        public void Jump()
        {
            if (aiController2D.collisions.below == true)
            {
                velocity.y = maxJumpVelocity;
            }
        }

        public void Fall(int _input)
        {
            input.y = _input;
        }

        #endregion

        #region Private Functions

        #endregion

    }
}
