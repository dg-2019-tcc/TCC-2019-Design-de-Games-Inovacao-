using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class BotFSM : StateMachineAI
    {
        #region Variables
        private MovementAI movementAI;
        private ActionsAI actionsAI;

        // States do FSM
        public None noneState;
        public Stop stopState;
        public Jump jumpState;
        public Fall fallState;
        public MoveLeft moveLeft;
        public MoveRight moveRight;
        public Kick kickState;
        public TouchBall touchBallState;
        public KickPlayer kickPlayerState;

        public enum States { Idle, Right, Left, Down, Up, Kick, None}
        public States state = States.None;

        #endregion

        #region Unity Function

        void Awake()
        {
            movementAI = GetComponent<MovementAI>();
            actionsAI = GetComponent<ActionsAI>();
        }

        #endregion

        #region Public Functions

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
            //if (state == States.Up) return;
            if(jumpState == null) { jumpState = new Jump(movementAI, this); }
            SetState02(jumpState);
            state = States.Up;
        }

        public void SetFall()
        {
            if (state == States.Down) return;
            if(fallState == null) { fallState = new Fall(movementAI, this); }
            SetState02(fallState);
            state = States.Down;
        }

        public void SetKick(Rigidbody2D _rb, int _type)
        {
            switch (_type)
            {
                case 0:
                    if(touchBallState == null) { touchBallState = new TouchBall(actionsAI, this, _rb); }
                    SetState03(touchBallState);
                    break;

                case 1:
                    if(kickState == null) { kickState = new Kick(actionsAI,this,_rb); }
                    SetState03(kickState);
                    break;

            }
        }

        public void SetKickPlayer(NewPlayerMovent _playerMovement)
        {
            if(kickPlayerState == null) { kickPlayerState = new KickPlayer(actionsAI, this, _playerMovement); }
            SetState03(kickPlayerState);
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
