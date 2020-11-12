using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

namespace AI
{
    public class Stop : StateAI
    {
        private MovementAI movementAI;
        private BotFSM stateMachine;
        private AnimationsAI animationsAI;

        public Stop(MovementAI _moveAI, BotFSM _stateMachine, AnimationsAI _animationsAI)
        {
            movementAI = _moveAI;
            stateMachine = _stateMachine;
            animationsAI = _animationsAI;
        }

        public override void EntryAction()
        {
            animationsAI.CallAnim(AnimationsAI.State.Idle);
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
