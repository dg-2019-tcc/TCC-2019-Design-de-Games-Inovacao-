using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class BotStates : MonoBehaviour
    {
        public BotInfo botInfo;
        private AIMovement aiMovement;
        public AISpawner aiSpawner;

        public enum State { Search, Right, Left, Up, Down, Idle, Manobra, Null }
        public State move01State = State.Idle;
        public State move02State = State.Null;
        public State move03State = State.Null;
        public State actionState = State.Null;

        private float searchTime;
        private int randomState;

        public List<GameObject> coletaveis;
        public GameObject target;

        private void Start()
        {
            aiMovement = GetComponent<AIMovement>();
            aiSpawner = FindObjectOfType<AISpawner>();

            coletaveis = aiSpawner.wayPointsForAI;
            //FindAllTargets();
            //InvokeRepeating("CheckTarget", 5f, 5f);
        }


        public void BotWork()
        {
            if(target == null) { CheckTarget();}
            SetDirection();
            aiMovement.Move(move01State, move02State, move03State);
        }


        public void CheckTarget()
        {
            if (GameManager.Instance.fase.Equals(GameManager.Fase.Coleta))
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
        }

        public void SetDirection()
        {
            botInfo.Reset();
            SetState(State.Null, true);
            if (transform.position.x - target.transform.position.x > 0.5)
            {
                botInfo.isLeft = true;
                SetState(State.Left,false);
            }
            else if (transform.position.x - target.transform.position.x < -0.5)
            {
                botInfo.isRight = true;
                SetState(State.Right,false);
            }
            else
            {
                botInfo.isRight = false;
                botInfo.isLeft = false;
                SetState(State.Idle,false);
            }

            if (transform.position.y - target.transform.position.y < -0.8)
            {
                botInfo.isUp = true;
                SetState(State.Up,false);
            }
            else if (transform.position.y - target.transform.position.y >0.5)
            {
                botInfo.isDown = true;
                SetState(State.Down,false);
            }
            else
            {
                botInfo.isUp = false;
                botInfo.isDown = false;
                SetState(State.Idle,false);
            }
        }

        public void SetState(State nextState, bool reset)
        {
            if (!reset)
            {
                if (nextState == move01State || nextState == move02State || nextState == move03State) { return; }
                if (move01State == State.Idle || move01State == State.Null)
                {
                    move01State = nextState;
                }

                else
                {
                    if (move02State == State.Idle || move02State == State.Null)
                    {
                        move02State = nextState;
                    }
                    else
                    {
                        move03State = nextState;
                    }
                }
            }
            else
            {
                move01State = State.Null;
                move02State = State.Null;
                move03State = State.Null;
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
