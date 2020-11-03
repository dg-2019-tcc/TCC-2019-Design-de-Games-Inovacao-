﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Fall : StateAI
    {
        private MovementAI movementAI;
        private BotFSM stateMachine;

        public Fall(MovementAI _moveAI, BotFSM _stateMachine)
        {
            movementAI = _moveAI;
            stateMachine = _stateMachine;
        }

        public override void EntryAction()
        {
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
