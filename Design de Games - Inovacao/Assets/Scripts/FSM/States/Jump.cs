using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Jump : StateAI
    {
        private MovementAI movementAI;
        private BotFSM stateMachine;

        public Jump(MovementAI _moveAI, BotFSM _stateMachine)
        {
            movementAI = _moveAI;
            stateMachine = _stateMachine;
        }

        public override void EntryAction()
        {
            Debug.Log("[Jump] EntryAction");
        }

        public override void ExitAction()
        {
            //stateMachine.StopMovement(movementAI);
            Debug.Log("[Jump] ExitAction");
        }

        public override void UpdateAction()
        {
            movementAI.Jump();
        }
    }
}
