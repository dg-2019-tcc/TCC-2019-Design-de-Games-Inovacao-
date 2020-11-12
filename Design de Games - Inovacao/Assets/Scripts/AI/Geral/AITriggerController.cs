using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace Complete
{
    public class AITriggerController : RaycastController
    {
        public TriggerCollisionInfo triggerCollision;
        public FloatVariable rayLenghtAI;
        public Rigidbody2D rbBola;
        public FootballFSMController footballFSMController;
        public CollectFSM collectFSM;
        private MovementAI movementAI;
        [HideInInspector] public NewPlayerMovent playerMovement;


        public FloatVariable botScore;

        public bool isRace;

        #region Unity Function

        public override void Start()
        {
            base.Start();
            movementAI = GetComponent<MovementAI>();
        }

        #endregion

        #region Public Functions

        public void RayTriggerDirection()
        {
            UpdateRaycastOrigins();
            triggerCollision.Reset();

            RightCollisions();
            LeftCollisions();
            UpCollisions();
            DownCollisions();

            if (isRace)
            {
                RightCollisions();
                DownCollisions();
            }

            else
            {
                RightCollisions();
                LeftCollisions();
                UpCollisions();
                DownCollisions();
            }
        }

        #endregion

        #region Private Functions

        void RightCollisions()
        {
            float directionX = 1;
            float rayLenght;

            rayLenght = rayLenghtAI.Value + skinWidth;


            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

                //Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

                if (hit)
                {
                    if (hit.collider.CompareTag("LinhaDeChegada") && hit.distance == 0)
                    {
                        Debug.Log("AIGanhou");
                        triggerCollision.ganhou = true;
                        LinhaDeChegada linhaDeChegada = hit.collider.GetComponent<LinhaDeChegada>();
                        linhaDeChegada.AIGanhou();
                    }


                    if (hit.collider.CompareTag("Coletavel"))
                    {
                        triggerCollision.isRight = true;

                        if (hit.distance == 0)
                        {
                            triggerCollision.isRight = true;
                            DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                            coletavel2D.PegouColetavel(true);
                            botScore.Value++;
                        }
                    }

                    if (hit.collider.CompareTag("Futebol"))
                    {

                        if (rbBola == null)
                        {
                            rbBola = hit.collider.GetComponent<Rigidbody2D>();
                        }
                        triggerCollision.isRight = true;
                        if (hit.distance <= 0.5f)
                        {
                            triggerCollision.touchBall = true;
                        }
                    }

                    if (hit.collider.CompareTag("Volei"))
                    {
                        if (rbBola == null)
                        {
                            rbBola = hit.collider.GetComponent<Rigidbody2D>();
                        }
                        triggerCollision.isUp = true;

                        if (hit.distance <= 0.5f)
                        {
                            footballFSMController.CallKick(rbBola, 0);
                            triggerCollision.touchBall = true;
                        }
                    }

                    if (hit.collider.CompareTag("Plataforma"))
                    {
                        if (hit.distance <= 2f)
                        {
                            triggerCollision.needJump = true;
                        }
                    }

                    if (hit.collider.CompareTag("Barreira"))
                    {
                        triggerCollision.needJump = true;
                    }

                    if (hit.collider.CompareTag("AITrigger"))
                    {
                        triggerCollision.needJump = true;
                    }

                    if (hit.collider.CompareTag("BotArea"))
                    {
                        if (hit.distance == 0)
                        {
                            triggerCollision.botArea = true;
                        }
                    }

                    if (hit.collider.CompareTag("Player"))
                    {
                        if (hit.distance <= 0.5f)
                        {
                            if (movementAI.input.x > 0)
                            {
                                playerMovement = hit.collider.GetComponent<NewPlayerMovent>();
                                footballFSMController.CallKickPlayer(playerMovement);
                            }
                        }
                    }

                    /*if (hit.collider.tag == "Area")
                    {
                        triggerCollision.naArea = true;
                    }*/
                }
            }
        }

        void LeftCollisions()
        {
            float directionX = -1;

            var rayLenght = rayLenghtAI.Value + skinWidth;


            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

                //Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

                if (hit)
                {
                    if (hit.collider.CompareTag("Coletavel"))
                    {
                        triggerCollision.isLeft = true;
                        if (hit.distance == 0)
                        {
                            triggerCollision.isLeft = true;
                            DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                            coletavel2D.PegouColetavel(true);
                            botScore.Value++;
                        }
                    }

                    if (hit.collider.CompareTag("Player"))
                    {
                        if (hit.distance <= 0.5f)
                        {
                            if (movementAI.input.x < 0)
                            {
                                playerMovement = hit.collider.GetComponent<NewPlayerMovent>();
                                footballFSMController.CallKickPlayer(playerMovement);
                            }
                        }
                    }

                    if (hit.collider.CompareTag("Futebol"))
                    {
                        if (rbBola == null)
                        {
                            rbBola = hit.collider.GetComponent<Rigidbody2D>();
                        }
                        triggerCollision.isLeft = true;
                        if (hit.distance <= 0.5f)
                        {
                            if (movementAI.input.x > 0)
                            {
                                footballFSMController.CallKick(rbBola, 0);
                                triggerCollision.touchBall = true;
                            }
                            else
                            {
                                footballFSMController.CallKick(rbBola,1);
                                triggerCollision.chutouBall = true;
                                triggerCollision.ativaAnimChute = true;
                            }
                        }
                    }

                    if (hit.collider.CompareTag("Volei"))
                    {
                        if (rbBola == null)
                        {
                            rbBola = hit.collider.GetComponent<Rigidbody2D>();
                        }
                        triggerCollision.isUp = true;

                        if (hit.distance <= 0.5f)
                        {
                            triggerCollision.touchBall = true;
                        }
                    }

                    if (hit.collider.CompareTag("Area"))
                    {
                        triggerCollision.naArea = true;
                    }

                    if (hit.collider.CompareTag("BotArea"))
                    {
                        if (hit.distance == 0)
                        {
                            triggerCollision.botArea = true;
                        }
                    }
                }
            }
        }

        void UpCollisions()
        {
            float directionY = 1;
            float rayLenght = rayLenghtAI.Value + skinWidth;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

                if (hit)
                {
                    if (hit.collider.CompareTag("Through"))
                    {
                        if (movementAI.needToJump)
                        {
                            if (hit.distance <= movementAI.maxJumpHeight)
                            {
                                triggerCollision.canJump = true;
                            }
                        }
                    }

                    if (hit.collider.CompareTag("Coletavel"))
                    {
                        triggerCollision.isUp = true;
                        if (hit.distance == 0)
                        {
                            triggerCollision.isUp = true;
                            DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                            coletavel2D.PegouColetavel(true);
                            botScore.Value++;
                        }
                    }


                    if (hit.collider.CompareTag("Futebol"))
                    {

                        if (rbBola == null)
                        {
                            rbBola = hit.collider.GetComponent<Rigidbody2D>();
                        }

                        if (hit.distance <= 0.5f)
                        {
                            footballFSMController.CallKick(rbBola, 0);
                            triggerCollision.isUp = true;
                            triggerCollision.touchBall = true;
                        }
                    }

                    if (hit.collider.CompareTag("Volei"))
                    {
                        if (rbBola == null)
                        {
                            rbBola = hit.collider.GetComponent<Rigidbody2D>();
                        }

                        triggerCollision.isUp = true;

                        if (hit.distance <= 0.5f)
                        {
                            triggerCollision.touchBall = true;
                        }
                    }

                    if (hit.collider.CompareTag("Area"))
                    {
                        triggerCollision.naArea = true;
                    }

                    if (hit.collider.CompareTag("BotArea"))
                    {
                        if (hit.distance == 0)
                        {
                            triggerCollision.botArea = true;
                        }
                    }
                }
            }
        }

        void DownCollisions()
        {
            float directionY = -1;
            float rayLenght = rayLenghtAI.Value + skinWidth;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

                if (hit)
                {
                    if (hit.collider.CompareTag("Coletavel"))
                    {
                        triggerCollision.isDown = true;

                        if (hit.distance == 0)
                        {
                            DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                            coletavel2D.PegouColetavel(true);
                            botScore.Value++;
                        }
                    }


                    if (hit.collider.CompareTag("AITrigger"))
                    {
                        triggerCollision.needJump = true;
                    }

                    if (hit.collider.CompareTag("Area"))
                    {
                        triggerCollision.naArea = true;
                    }


                    if (hit.collider.CompareTag("CaixaDagua"))
                    {
                        triggerCollision.caixaDagua = true;
                    }

                    if (hit.collider.CompareTag("BotArea"))
                    {
                        if (hit.distance == 0)
                        {
                            triggerCollision.botArea = true;
                        }
                    }
                }
            }
        }

        #endregion

        public struct TriggerCollisionInfo
        {
            public bool isLeft, isRight;
            public bool isUp, isDown;
            public bool touchBall, chutouBall;
            public bool needJump;
            public bool naArea;
            public bool ganhou;
            public bool caixaDagua;
            public bool ativaAnimChute;
            public bool botArea;
            public bool canJump;

            public void Reset()
            {
                isLeft = isRight = false;
                isUp = isDown = false;
                touchBall = chutouBall = false;
                needJump = naArea = false;
                ganhou = false;
                caixaDagua = false;
                ativaAnimChute = false;
                botArea = false;
                canJump = false;
            }
        }
    }
}
    
