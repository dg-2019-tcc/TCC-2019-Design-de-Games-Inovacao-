using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class BotFSM : StateMachineAI
    {
        #region Variables
        private MovementAI movementAI;

        // States do FSM
        public None noneState;
        public Stop stopState;
        public Jump jumpState;
        public MoveLeft moveLeft;
        public MoveRight moveRight;

        public enum States { Idle, Right, Left, Down, Up, None}
        public States state = States.None;

        #endregion

        #region Unity Function

        void Awake()
        {
            //StopMovement();
        }

        #endregion

        #region Public Functions

        public void SetBotFSM(MovementAI _movementAI)
        {
            movementAI = _movementAI;
        }

        public void Idle(MovementAI _movementAI)
        {
            if (stopState == null) { stopState = new Stop(movementAI, this); }
            if (state != States.Idle)
            {
                SetState01(stopState);
                state = States.Idle;
            }
        }

        public void SetHorizontalMovement(float _inputX)
        {
            if (_inputX > 0)
            {
                if (state == States.Right) return;
                if (moveRight == null) { moveRight = new MoveRight(movementAI, this); }
                SetState01(moveRight);
                state = States.Right;
            }
            else if (_inputX < 0)
            {
                if (state == States.Left) return;
                if (moveLeft == null) { moveLeft = new MoveLeft(movementAI, this); }
                SetState01(moveLeft);
                state = States.Left;
            }
        }

        public void SetJump()
        {
            if(jumpState == null) { jumpState = new Jump(movementAI, this); }
            SetState02(jumpState);
        }

        public void SetNone()
        {
            if(noneState == null) { noneState = new None(movementAI, this); }
            SetState02(noneState);
        }
        #endregion

        #region Private Functions

        #endregion
    }
}
