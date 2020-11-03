using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    public class Stop : StateAI
    {
        private MovementAI movementAI;
        private BotFSM stateMachine;

        public Stop(MovementAI _moveAI, BotFSM _stateMachine)
        {
            movementAI = _moveAI;
            stateMachine = _stateMachine;
        }

        public override void EntryAction()
        {
        }

        public override void ExitAction()
        {
        }

        public override void UpdateAction()
        {
            movementAI.SetMovement(0);
        }
    }
}
