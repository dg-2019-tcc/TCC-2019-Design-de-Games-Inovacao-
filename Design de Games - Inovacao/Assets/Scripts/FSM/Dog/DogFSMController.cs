using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    namespace Dog
    {
        [RequireComponent(typeof(BotFSM))]
        public class DogFSMController : MonoBehaviour
        {
            #region Variables

            public bool startBot;   

            private BotFSM botFSM;
            private MovementAI movementAI;

            public GameObject target;
            public Vector2 distTarget;

            Vector2 input;
            float inputX;
            float inputY;

            #endregion

            #region Unity Function

            private void Awake()
            {
                movementAI = GetComponent<MovementAI>();
                botFSM = GetComponent<BotFSM>();
            }

            private void Update()
            {
                if (GameManager.pausaJogo == true) { return; }
                if (startBot)
                {
                    CalculateDistance();
                    if ((distTarget.x <= 0.5f && distTarget.x >= -0.5f)  && (distTarget.y <= 0.5f && distTarget.y >= -0.5f)) { botFSM.Idle(movementAI); }
                    else { SetDirection(); }
                }
                else
                {
                    botFSM.Idle(movementAI);
                    botFSM.SetNone(2);
                    botFSM.SetNone(3);
                }
            }

            #endregion

            #region Public Functions

            #endregion

            #region Private Functions

            void CalculateDistance()
            {
                distTarget = target.transform.position - transform.position;
            }

            void SetDirection()
            {
                SetHorizontalDirection();
                SetVerticalDirection();
            }

            void SetHorizontalDirection()
            {
                if (distTarget.x > 1) { inputX = 1; }
                else if (distTarget.x < -1) { inputX = -1; }
                else { inputX = 0; }

                botFSM.SetHorizontalMovement(inputX);
            }

            void SetVerticalDirection()
            {
                if (distTarget.y > 1)
                {
                    movementAI.aiController2D.collisions.canJump = true;
                    botFSM.SetJump();
                }
                else
                {
                    botFSM.SetNone(2);
                }
            }

            #endregion
        }
    }
}
