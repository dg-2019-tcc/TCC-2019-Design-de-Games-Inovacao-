using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class MoveLeft : StateAI
    {
        private MovementAI movementAI;
        private BotFSM stateMachine;

        public MoveLeft(MovementAI _moveAI, BotFSM _stateMachine)
        {
            movementAI = _moveAI;
            stateMachine = _stateMachine;
        }


        public override void EntryAction()
        {
        }

        public override void ExitAction()
        {
            movementAI.SetMovement(0);
        }

        public override void UpdateAction()
        {
            movementAI.SetMovement(-1);
        }
    }
}
