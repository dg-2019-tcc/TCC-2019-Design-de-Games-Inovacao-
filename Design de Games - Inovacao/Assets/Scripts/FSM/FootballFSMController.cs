using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

namespace AI
{
    [RequireComponent(typeof(BotFSM))]
    public class FootballFSMController : MonoBehaviour
    {
        #region Variables
        public bool startBot;

        private BotFSM botFSM;
        private MovementAI movementAI;
        private ActionsAI actionsAI;

        public AISpawner spawner;
        public GameObject target;

        public Vector2 distTarget;

        [SerializeField]
        Vector2 input;
        float inputX;
        float inputY;

        public float kickCooldown = 1f;
        bool canKick = true;

        public float stunnedTime = 1f;
        #endregion

        #region Unity Function

        void Awake()
        {
            spawner = FindObjectOfType<AISpawner>();
            movementAI = GetComponent<MovementAI>();
            botFSM = GetComponent<BotFSM>();
            actionsAI = GetComponent<ActionsAI>();

            target = spawner.bola;
        }

        void Update()
        {
            if (GameManager.pausaJogo == true) { return; }
            if (startBot)
            {
                CalculateDistance();
                if ((distTarget.x <= 0.5f && distTarget.x >= -0.5f) && (distTarget.y <= 0.5f && distTarget.y >= -0.5f)) { botFSM.Idle(movementAI); }
                else { SetDirection(); }
            }
            else
            {
                botFSM.Idle(movementAI);
                botFSM.SetNone(2);
                botFSM.SetNone(3);
            }
        }

        #endregion

        #region Public Functions
        public void CallKick(Rigidbody2D _rb, int _type)
        {
            if (!canKick) return;
            botFSM.SetKick(_rb, _type);
            StartCoroutine("KickCooldown");
        }

        public void CallKickPlayer(NewPlayerMovent _playerMovement)
        {
            if (!canKick) return;
            botFSM.SetKickPlayer(_playerMovement);
            StartCoroutine("KickCooldown");
        }

        public void StunnedByPlayer()
        {
            StartCoroutine("Stunned");
        }
        #endregion

        #region Private Functions

        IEnumerator KickCooldown()
        {
            canKick = false;
            yield return new WaitForSeconds(kickCooldown);
            canKick = true;
        }

        IEnumerator Stunned()
        {
            Debug.Log("[FootballFSMController] Stunned");
            startBot = false;
            yield return new WaitForSeconds(stunnedTime);
            startBot = true;
        }

        void CalculateDistance()
        {
            distTarget = target.transform.position - transform.position;
        }

        void SetDirection()
        {
            SetHorizontalDirection();
            if (inputX == 0) { SetVerticalDirection(); }
        }

        void SetHorizontalDirection()
        {
            if (distTarget.x > 0.5)
            {
                inputX = 1;
                if(distTarget.y < 1)
                {
                    movementAI.aiController2D.collisions.canJump = true;
                    botFSM.SetJump();
                }
            }
            else if (distTarget.x < -1) { inputX = -1; }
            else { inputX = 0; }

            botFSM.SetHorizontalMovement(inputX);
        }

        void SetVerticalDirection()
        {
            if (distTarget.y > 1)
            {
                movementAI.aiController2D.collisions.canJump = true;
                botFSM.SetJump();
            }
            else
            {
                botFSM.SetNone(2);
            }
        }
        #endregion
    }
}
