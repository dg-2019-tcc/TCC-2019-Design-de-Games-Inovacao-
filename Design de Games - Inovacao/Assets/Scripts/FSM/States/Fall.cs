using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

namespace AI
{
    public class Fall : StateAI
    {
        private MovementAI movementAI;
        private BotFSM stateMachine;
        private AnimationsAI animationsAI;

        public Fall(MovementAI _moveAI, BotFSM _stateMachine, AnimationsAI _animationsAI)
        {
            movementAI = _moveAI;
            stateMachine = _stateMachine;
            animationsAI = _animationsAI;
        }

        public override void EntryAction()
        {
            animationsAI.CallAnim(AnimationsAI.State.Falling);
            movementAI.Fall(-1);
        }

        public override void ExitAction()
        {
            movementAI.Fall(0);
        }

        public override void UpdateAction()
        {
            movementAI.Fall(-1);
        }
    }
}
