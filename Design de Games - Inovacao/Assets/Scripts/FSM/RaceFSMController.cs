using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

namespace AI
{
    [RequireComponent(typeof(BotFSM))]
    public class RaceFSMController : MonoBehaviour
    {
        #region Variables

        public bool startBot;

        private BotFSM botFSM;
        private MovementAI movementAI;
        private ActionsAI actionsAI;
        private AITriggerController triggerController;

        #endregion

        #region Unity Function

        void Awake()
        {
            movementAI = GetComponent<MovementAI>();
            botFSM = GetComponent<BotFSM>();
            actionsAI = GetComponent<ActionsAI>();
            triggerController = GetComponent<AITriggerController>();
        }

        void Update()
        {
            //if (GameManager.pausaJogo == true) { return; }
            if (startBot)
            {
                CallHorizontalMovement();
                CheckObstacles();
            }
            else
            {
                botFSM.Idle(movementAI);
            }
        }

        #endregion

        #region Public Functions

        #endregion

        #region Private Functions
        void CallHorizontalMovement()
        {
            botFSM.SetHorizontalMovement(1);
        }

        void CheckObstacles()
        {
            if(movementAI.aiController2D.collisions.right && movementAI.aiController2D.collisions.below == false)
            {
                botFSM.Idle(movementAI);
            }

            if (triggerController.triggerCollision.needJump && movementAI.aiController2D.collisions.climbingSlope == false)
            {
                movementAI.aiController2D.collisions.canJump = true;
                Debug.Log("Jump");
                botFSM.SetJump();
            }
        }
        #endregion
    }
}
