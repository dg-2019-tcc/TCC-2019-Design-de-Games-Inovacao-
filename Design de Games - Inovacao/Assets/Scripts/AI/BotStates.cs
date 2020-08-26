using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Scene;

namespace Complete {
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

        private void Start()
        {
            aiMovement = GetComponent<AIMovement>();
            aiSpawner = FindObjectOfType<AISpawner>();
            triggerController = GetComponent<AITriggerController>();

            //GameManager.Instance.ChecaFase();

            coletaveis = aiSpawner.wayPointsForAI;
        }


        public void BotWork()
        {
            if(GameManager.pausaJogo == true || active == State.Off) { return; }
            //if (GameManager.Instance.fase.Equals(GameManager.Fase.Coleta) || GameManager.Instance.fase.Equals(GameManager.Fase.Futebol) || GameManager.Instance.fase.Equals(GameManager.Fase.Volei))
            if(GameManager.Instance.sceneAtual == SceneType.Coleta || GameManager.Instance.sceneAtual == SceneType.Futebol || GameManager.Instance.sceneAtual == SceneType.Volei)
            {
                if (target == null)
                {
                    CheckTarget();
                }
            }
            SetDirection();
            aiMovement.Move(horizontalState, verticalState, actionOn);
        }


        public void CheckTarget()
        {
            //if (GameManager.Instance.fase.Equals(GameManager.Fase.Coleta))
            if (GameManager.Instance.sceneAtual == SceneType.Coleta)
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

            //if (GameManager.Instance.fase.Equals(GameManager.Fase.Futebol))
            if (GameManager.Instance.sceneAtual == SceneType.Futebol)
            {
                if(target == null)
                {
                    target = aiSpawner.bola;
                }
            }
        }

        public void SetDirection()
        {
            botInfo.Reset();
            SetState(State.Null, true);
            //if (GameManager.Instance.fase.Equals(GameManager.Fase.Coleta))
            if (GameManager.Instance.sceneAtual == SceneType.Coleta)
            {
                if (transform.position.x - target.transform.position.x > 0.5)
                {
                    botInfo.isLeft = true;
                    SetState(State.Left, false);
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

                if (transform.position.y - target.transform.position.y < -0.8)
                {
                    botInfo.isUp = true;
                    SetState(State.Up, false);
                }
                else if (transform.position.y - target.transform.position.y > 0.5)
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

            //else if (GameManager.Instance.fase.Equals(GameManager.Fase.Futebol))
            else if (GameManager.Instance.sceneAtual == SceneType.Futebol)
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

            //else if (GameManager.Instance.fase.Equals(GameManager.Fase.Moto))
            else if (GameManager.Instance.sceneAtual == SceneType.Moto)
            {
                SetState(State.Right, false);

                if (triggerController.triggerCollision.needJump)
                {
                    SetState(State.Up, false);
                }

                aiMovement.speed += 0.2f * Time.deltaTime;
            }

            //else if (GameManager.Instance.fase.Equals(GameManager.Fase.Corrida))
             else if (GameManager.Instance.sceneAtual == SceneType.Corrida)
            {
                SetState(State.Right, false);

                if (triggerController.triggerCollision.needJump)
                {
                    SetState(State.Up, false);
                }
            }
        }

        public void SetActionState(State nextActionState)
        {
            if(actionState == State.Null)
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

        public void SetState(State nextState, bool reset)
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
