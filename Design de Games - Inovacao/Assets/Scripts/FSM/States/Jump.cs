using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

namespace AI
{
    public class Jump : StateAI
    {
        private MovementAI movementAI;
        private BotFSM stateMachine;
        private AnimationsAI animationsAI;

        public Jump(MovementAI _moveAI, BotFSM _stateMachine, AnimationsAI _animationsAI)
        {
            movementAI = _moveAI;
            stateMachine = _stateMachine;
            animationsAI = _animationsAI;
        }

        public override void EntryAction()
        {
            movementAI.isJumping = true;
            animationsAI.CallAnim(AnimationsAI.State.Jumping);
        }

        public override void ExitAction()
        {
            movementAI.isJumping = false;
        }

        public override void UpdateAction()
        {
            movementAI.Jump();
        }
    }
}
