using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Complete;

namespace AI
{
    public class BotFSM : StateMachineAI
    {
        #region Variables
        private MovementAI movementAI;
        private ActionsAI actionsAI;

        // States do FSM
        public None noneState;
        public Stun stunState;
        public Stop stopState;
        public Jump jumpState;
        public Fall fallState;
        public MoveLeft moveLeft;
        public MoveRight moveRight;
        public Kick kickState;
        public TouchBall touchBallState;
        public KickPlayer kickPlayerState;

        public enum States { Idle, Right, Left, Down, Up, Kick, None}
        public States stateHorizontal = States.None;
        public States stateVertical = States.None;
        public States stateAction = States.None;

        private AnimationsAI animationsAI;

        public GameObject botObj;
        Quaternion direction;
        #endregion

        #region Unity Function

        void Awake()
        {
            movementAI = GetComponent<MovementAI>();
            actionsAI = GetComponent<ActionsAI>();
            animationsAI = GetComponent<AnimationsAI>();
        }

        #endregion

        #region Public Functions

        public void Idle(MovementAI _movementAI)
        {
            if (stopState == null) { stopState = new Stop(movementAI, this, animationsAI); }
            if (stateHorizontal != States.Idle)
            {
                SetState01(stopState);
                stateHorizontal = States.Idle;
            }
        }

        public void SetHorizontalMovement(float _inputX)
        {
            if (_inputX > 0)
            {
                if (stateHorizontal == States.Right) return;
                if (moveRight == null) { moveRight = new MoveRight(movementAI, this, animationsAI); }
                direction = Quaternion.Euler(0, 0, 0);
                botObj.transform.rotation = direction;
                SetState01(moveRight);
                stateHorizontal = States.Right;
            }
            else if (_inputX < 0)
            {
                if (stateHorizontal == States.Left) return;
                if (moveLeft == null) { moveLeft = new MoveLeft(movementAI, this, animationsAI); }
                direction = Quaternion.Euler(0, 180, 0);
                botObj.transform.rotation = direction;
                SetState01(moveLeft);
                stateHorizontal = States.Left;
            }
        }

        public void SetJump()
        {
            //if (state == States.Up) return;
            if(jumpState == null) { jumpState = new Jump(movementAI, this, animationsAI); }
            SetState02(jumpState);
            stateVertical = States.Up;
        }

        public void SetFall()
        {
            if (stateVertical == States.Down) return;
            if(fallState == null) { fallState = new Fall(movementAI, this, animationsAI); }
            SetState02(fallState);
            stateVertical = States.Down;
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
                    if(kickState == null) { kickState = new Kick(actionsAI,this,_rb, animationsAI); }
                    SetState03(kickState);
                    break;

            }
        }

        public void SetKickPlayer(NewPlayerMovent _playerMovement)
        {
            if(kickPlayerState == null) { kickPlayerState = new KickPlayer(actionsAI, this, _playerMovement); }
            SetState03(kickPlayerState);
        }

        public void SetStun()
        {
            if(stunState == null) { stunState = new Stun(this); }
            Idle(movementAI);
            SetNone(2);
            SetNone(3);
        }

        public void SetNone(int _state)
        {
            if (noneState == null) { noneState = new None(movementAI, this); }

            if (_state == 2){ SetState02(noneState); stateVertical = States.None; }
            else if(_state == 3){ SetState03(noneState);}
        }
        #endregion

        #region Private Functions

        #endregion
    }
}
