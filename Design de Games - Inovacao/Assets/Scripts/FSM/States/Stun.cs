using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Stun : StateAI
    {
        private BotFSM stateMachine;

        public Stun( BotFSM _stateMachine)
        {
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
        }
    }
}
