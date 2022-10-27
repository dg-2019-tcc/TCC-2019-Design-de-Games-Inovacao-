using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class KickPlayer : StateAI
    {
        private ActionsAI actionsAI;
        private BotFSM stateMachine;
        private NewPlayerMovent playerMovement;

        public KickPlayer(ActionsAI _actionsAI, BotFSM _stateMachine, NewPlayerMovent _playerMovement)
        {
            actionsAI = _actionsAI;
            stateMachine = _stateMachine;
            playerMovement = _playerMovement;
        }

        public override void EntryAction()
        {
            actionsAI.KickPlayer(playerMovement);
        }

        public override void ExitAction()
        {
        }

        public override void UpdateAction()
        {
        }
    }
}

