using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

namespace AI
{
    [RequireComponent(typeof(BotFSM))]
    public class CollectFSM : MonoBehaviour
    {
        #region Variables
        public bool startBot;

        private BotFSM botFSM;
        private MovementAI movementAI;
        public AISpawner aiSpawner;

        public List<GameObject> coletaveis;
        public GameObject target;

        public Vector2 distTarget;

        [SerializeField]
        Vector2 input;
        float inputX;
        float inputY;

        #endregion

        #region Unity Function

        void Awake()
        {
            movementAI = GetComponent<MovementAI>(); 
            botFSM = GetComponent<BotFSM>();
            botFSM.SetBotFSM(movementAI);
            coletaveis = aiSpawner.wayPointsForAI;
        }

        void Update()
        {
            if (GameManager.pausaJogo == true) { return; }
            if (startBot)
            {
                if(target == null)
                {
                    CheckTarget();
                }
                else
                {
                    CalculateDistance();
                    if ((distTarget.x <= 0.5f && distTarget.x >= -0.5f)&& (distTarget.y <= 0.5f && distTarget.y >= -0.5f)) { botFSM.Idle(movementAI); }
                    else { SetDirection(); }
                }
            }
            else
            {
                botFSM.Idle(movementAI);
            }
        }

        #endregion

        #region Public Functions

        #endregion

        #region Private Functions


        void CheckTarget()
        {
            #region Usar Depois
            for (int i = 0; i < coletaveis.Count; i++)
            {
                if (coletaveis[i] == null)
                {
                    coletaveis.Remove(coletaveis[i]);
                    break;
                }
            }
            #endregion

            //Achar o coletavel ativo da lista
            for (int i = 0; i < coletaveis.Count; i++)
            {
                if (coletaveis[i].activeInHierarchy)
                {
                    target = coletaveis[i];
                    break;
                }
            }
        }


        void CalculateDistance()
        {
            distTarget = target.transform.position - transform.position;
        }

        void SetDirection()
        {
            SetVerticalDirection();
            SetHorizontalDirection();
        }

        void SetHorizontalDirection()
        {
            if (distTarget.x > 0.5) { inputX = 1; }
            else if (distTarget.x < -0.5) { inputX = -1; }

            botFSM.SetHorizontalMovement(inputX);
        }

        void SetVerticalDirection()
        {
            if (distTarget.y > 0.9)
            {
                movementAI.aiController2D.VerticalCollisions(ref distTarget);
                if (movementAI.aiController2D.collisions.canJump)
                {
                    botFSM.SetJump();
                    Debug.Log("[CollectFSM] SetJump");
                }
            }
            else if(distTarget.y < -1)
            {
                botFSM.SetFall();
                Debug.Log("[CollectFSM] SetFall");
            }
            else
            {
                botFSM.SetNone();
            }
        }
        #endregion
    }
}
