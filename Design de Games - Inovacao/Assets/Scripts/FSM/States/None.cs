using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class None : StateAI
    {
        private MovementAI movementAI;
        private BotFSM stateMachine;

        public None(MovementAI _moveAI, BotFSM _stateMachine)
        {
            movementAI = _moveAI;
            stateMachine = _stateMachine;
        }

        public override void EntryAction()
        {
            Debug.Log("[None] EntryAction");
        }

        public override void ExitAction()
        {
            //stateMachine.StopMovement(movementAI);
            Debug.Log("[None] ExitAction");
        }

        public override void UpdateAction()
        {
        }
    }
}
