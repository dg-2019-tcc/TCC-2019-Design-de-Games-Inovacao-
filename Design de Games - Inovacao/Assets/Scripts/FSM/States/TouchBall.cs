using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class TouchBall : StateAI
    {
        private ActionsAI actionsAI;
        private BotFSM stateMachine;
        private Rigidbody2D rb;

        public TouchBall(ActionsAI _actionsAI, BotFSM _stateMachine, Rigidbody2D _rb)
        {
            actionsAI = _actionsAI;
            stateMachine = _stateMachine;
            rb = _rb;
        }

        public override void EntryAction()
        {
            actionsAI.KickAction(rb, 0);
        }

        public override void ExitAction()
        {
        }

        public override void UpdateAction()
        {
        }
    }
}


