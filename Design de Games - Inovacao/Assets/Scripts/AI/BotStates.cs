using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Scene;

namespace Complete
{
    public class BotStates : MonoBehaviour
    {
        public BotInfo botInfo;
        private AIMovement aiMovement;
        public AISpawner aiSpawner;
        public AITriggerController triggerController;
        public HeadAI futebolScript;

        public enum State { Search, Right, Left, Up, Down, Idle, Manobra, Null, On, Off, Chuta}
        public State active = State.On;
        public State horizontalState = State.Null;
        public State verticalState = State.Null;
        public State actionState = State.Null;
        public bool actionOn;

        private float searchTime;
        private int randomState;

        public List<GameObject> coletaveis;
        public GameObject target;

        public Vector2 distTarget;

        #region Unity Function

        private void Start()
        {
            aiMovement = GetComponent<AIMovement>();
            aiSpawner = FindObjectOfType<AISpawner>();
            triggerController = GetComponent<AITriggerController>();

            coletaveis = aiSpawner.wayPointsForAI;
        }

        #endregion

        #region Public Functions
        // Chamado Pelo AIMovement
        public void BotWork()
        {
            if (GameManager.pausaJogo == true || active == State.Off) { return; }
            if (GameManager.sceneAtual == SceneType.Coleta || GameManager.sceneAtual == SceneType.Futebol || GameManager.sceneAtual == SceneType.Volei)
            {
                if (target == null)
                {
                    CheckTarget();
                }
            }
            SetDirection();
            aiMovement.Move(horizontalState, verticalState, actionOn);
        }

        #endregion

        #region Private Functions

        private void CheckTarget()
        {
            if (GameManager.sceneAtual == SceneType.Coleta)
            {
                if (target == null)
                {
                    for (int i = 0; i < coletaveis.Count; i++)
                    {
                        if (coletaveis[i] == null)
                        {
                            coletaveis.Remove(coletaveis[i]);
                            break;
                        }
                    }
                }
                for (int i = 0; i < coletaveis.Count; i++)
                {
                    if (coletaveis[i].activeInHierarchy)
                    {
                        target = coletaveis[i];
                        break;
                    }
                }
            }

            if (GameManager.sceneAtual == SceneType.Futebol)
            {
                if (target == null)
                {
                    target = aiSpawner.bola;
                }
            }
        }

        private void SetDirection()
        {
            botInfo.Reset();
            SetState(State.Null, true);

            switch (GameManager.sceneAtual)
            {
                case SceneType.Coleta:
                    ColetaDirection();
                    break;

                case SceneType.Futebol:
                    FutebolDirection();
                    break;

                case SceneType.Moto:
                    MotoDirection();
                    break;

                case SceneType.Corrida:
                    CorridaDirection();
                    break;
            }
        }

        void ColetaDirection()
        {
            distTarget = transform.position - target.transform.position;

            if (distTarget.x > 0.5)
            {
                botInfo.isLeft = true;
                SetState(State.Left, false);
            }
            else if (distTarget.x < -0.5)
            {
                botInfo.isRight = true;
                SetState(State.Right, false);
            }
            else
            {
                botInfo.isRight = false;
                botInfo.isLeft = false;
                SetState(State.Idle, false);
            }

            if (distTarget.y < -0.8)
            {
                botInfo.isUp = true;
                SetState(State.Up, false);
            }
            else if (distTarget.y > 0.5)
            {
                botInfo.isDown = true;
                SetState(State.Down, false);
            }
            else
            {
                botInfo.isUp = false;
                botInfo.isDown = false;
                SetState(State.Idle, false);
            }
        }

        void FutebolDirection()
        {
            if (transform.position.x - target.transform.position.x > 0.1)
            {
                botInfo.isLeft = true;
                SetState(State.Left, false);
                if (transform.position.x - target.transform.position.x < 0.5)
                {
                    SetActionState(State.Chuta);
                }
                else
                {
                    SetActionState(State.Null);
                }
            }
            else if (transform.position.x - target.transform.position.x < -0.5)
            {
                botInfo.isRight = true;
                SetState(State.Right, false);
            }
            else
            {
                botInfo.isRight = false;
                botInfo.isLeft = false;
                SetState(State.Idle, false);
            }

            if (transform.position.x - target.transform.position.x < 0.5f && transform.position.x - target.transform.position.x > -0.5f)
            {
                if (transform.position.y - target.transform.position.y < -2)
                {
                    botInfo.isUp = true;
                    SetState(State.Up, false);
                }
            }
            else
            {
                SetState(State.Idle, false);
            }
        }

        void MotoDirection()
        {
            SetState(State.Right, false);
            if (triggerController.triggerCollision.needJump) { SetState(State.Up, false);}
            aiMovement.speed += 0.2f * Time.deltaTime;
        }

        void CorridaDirection()
        {
            SetState(State.Right, false);
            if (triggerController.triggerCollision.needJump) { SetState(State.Up, false);}
        }


        private void SetActionState(State nextActionState)
        {
            if (actionState == State.Null)
            {
                if (nextActionState != State.Null)
                {
                    actionState = nextActionState;
                    actionOn = true;
                }
                else
                {
                    actionOn = false;
                }
            }
        }

        private void SetState(State nextState, bool reset)
        {
            if (!reset)
            {
                if (nextState == horizontalState || nextState == verticalState) { return; }
                if (horizontalState == State.Idle || horizontalState == State.Null)
                {
                    horizontalState = nextState;
                }

                else
                {
                    verticalState = nextState;
                }
            }
            else
            {
                horizontalState = State.Null;
                verticalState = State.Null;
            }
        }

        #endregion

        public struct BotInfo
        {
            public bool isLeft, isRight, isDown, isUp;

            public void Reset()
            {
                isDown = isLeft = isRight = isUp = false;
            }
        }
    }
}
