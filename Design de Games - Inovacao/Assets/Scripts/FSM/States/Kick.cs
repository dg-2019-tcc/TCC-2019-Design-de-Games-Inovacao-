using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

namespace AI
{
    public class Kick : StateAI
    {
        private ActionsAI actionsAI;
        private BotFSM stateMachine;
        private Rigidbody2D rb;
        private AnimationsAI animationsAI;

        public Kick(ActionsAI _actionsAI, BotFSM _stateMachine, Rigidbody2D _rb, AnimationsAI _animationsAI)
        {
            actionsAI = _actionsAI;
            stateMachine = _stateMachine;
            rb = _rb;
            animationsAI = _animationsAI;
        }

        public override void EntryAction()
        {
            actionsAI.KickAction(rb,1);
            animationsAI.CallAnim(AnimationsAI.State.Kicking);
        }

        public override void ExitAction()
        {
        }

        public override void UpdateAction()
        {
        }
    }
}