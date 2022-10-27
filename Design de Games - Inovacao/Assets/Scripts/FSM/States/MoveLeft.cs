using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

namespace AI
{
    public class MoveLeft : StateAI
    {
        private MovementAI movementAI;
        private BotFSM stateMachine;
        private AnimationsAI animationsAI;

        public MoveLeft(MovementAI _moveAI, BotFSM _stateMachine, AnimationsAI _animationsAI)
        {
            movementAI = _moveAI;
            stateMachine = _stateMachine;
            animationsAI = _animationsAI;
        }


        public override void EntryAction()
        {
            if (stateMachine.stateVertical == BotFSM.States.None)
            {
                animationsAI.CallAnim(AnimationsAI.State.Walking);
            }
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
