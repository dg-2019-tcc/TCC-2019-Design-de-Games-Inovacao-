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
            movementAI.Jump();
        }

        public override void ExitAction()
        {
        }

        public override void UpdateAction()
        {
        }
    }
}
