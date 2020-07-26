using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

namespace Complete
{
    public class AnimationsAI : MonoBehaviour
    {
        public GameObject frente;
        public GameObject lado;

        public bool isKlay;

        public string idlePose = "0_Idle";
        public string walkAnimation = "0_Corrida_V2";
        public string startJumpAnimation = "1_Pulo";
        public string subindoJumpAnimation = "1_NoAr(1_Subindo)";
        public string transitionJumpAnimation = "1_NoAr(2_Transicao)";
        public string descendoJumpAnimation = "1_NoAr(3_Descendo)";
        public string aterrisandoAnimation = "1_Aterrisando";
        public string chuteAnimation = "3_Bicuda(SemPreparacao)";
        public string abaixarAnimation = "5_Abaixar(2_IdlePose)";
        public string arremessoAnimation = "6_Arremessar(3_Arremesso)";
        public string inativoAnimation = "1_Inatividade(2_IdlePose)";

        public enum State { Idle, Walking, Jumping, Rising, Falling, TransitionAir, Aterrisando, Chutando, Abaixando, Arremessando, Inativo }

        public State state = State.Idle;

        private AIController2D controller;
        private AITriggerController triggerController;



        private UnityArmatureComponent playerSide;
        private UnityArmatureComponent playerFrente;

        [SerializeField]
        private bool jaAterrisou;

        public bool isFut;

        private void Start()
        {
            controller = GetComponent<AIController2D>();
            triggerController = GetComponent<AITriggerController>();


            if (isKlay == false)
            {
                playerSide = lado.GetComponent<UnityArmatureComponent>();
                playerFrente = frente.GetComponent<UnityArmatureComponent>();

                frente.SetActive(true);
                lado.SetActive(false);
            }

            else
            {
                playerSide = lado.GetComponent<UnityArmatureComponent>();
            }
        }

        public void ChangeAnimAI(Vector3 moveAmount, Vector2 oldPos, Vector2 input, bool Jump)
        {
            if (triggerController.triggerCollision.ativaAnimChute == false)
            {

                if (moveAmount.y < -2 && jaAterrisou && state != State.Aterrisando)
                {
                    jaAterrisou = false;
                }

                if (oldPos.y < moveAmount.y && controller.collisions.below == false)
                {
                    jaAterrisou = false;
                    AnimState("NoArUp");
                }

                else if (moveAmount.y < 15 && moveAmount.y > 0 && controller.collisions.below == false)
                {
                    AnimState("TransitionAir");
                }

                else if (moveAmount.y <= 0 && controller.collisions.below == false && jaAterrisou == false)
                {
                    AnimState("Fall");
                }

                else if (moveAmount.y < -5f && input.x == 0 && input.y >= 0 && controller.collisions.below == true && jaAterrisou == false)
                {
                    AnimState("Aterrisando");
                }

                else if (controller.collisions.below && input.x == 0 && input.y < 0)
                {
                    AnimState("Abaixar");
                }

                else if (input.x != 0 && controller.collisions.below)
                {
                    AnimState("Walking");
                }
                else
                {
                    AnimState("Idle");
                }
            }

            else
            {
                AnimState("Chute");
            }
        }

        public void AnimState(string anim)
        {

            if (isKlay == false)
            {
                if (state == State.Idle || state == State.Inativo)
                {
                    if (anim == "Walking" || anim == "NoArUp" || anim == "Fall" || anim == "Aterrisando" || anim == "Chute" || anim == "Arremesso" || anim == "Abaixar" || anim == "TransitionAir")
                    {
                        playerFrente.animation.Play(idlePose);
                        lado.SetActive(true);
                        frente.SetActive(false);
                    }
                }
            }
            switch (anim)
            {
                case "Idle":
                    if (state != State.Idle)
                    {
                        if (!isKlay)
                        {
                            frente.SetActive(true);
                            lado.SetActive(false);
                            playerFrente.animation.timeScale = 1;
                            playerFrente.animation.Play(idlePose);
                        }

                        else
                        {
                            playerSide.animation.Play(idlePose);
                        }

                        state = State.Idle;
                    }
                    break;


                case "Walking":
                    if (state != State.Walking)
                    {
                        playerSide.animation.Play(walkAnimation);
                        state = State.Walking;
                    }
                    break;

                case "StartPulo":
                    if (state != State.Jumping)
                    {
                        playerSide.animation.FadeIn(startJumpAnimation, 0f, 1);
                        playerSide.animation.timeScale = 1;
                        state = State.Jumping;
                    }
                    break;
                case "NoArUp":
                    if (state != State.Rising)
                    {
                        playerSide.animation.Play(subindoJumpAnimation);
                        state = State.Rising;
                    }
                    break;
                case "Fall":
                    if (state != State.Falling)
                    {
                        playerSide.animation.Play(descendoJumpAnimation);
                        state = State.Falling;
                    }
                    break;
                case "Aterrisando":
                    if (state != State.Aterrisando)
                    {
                        playerSide.animation.Play(aterrisandoAnimation);
                        state = State.Aterrisando;
                        jaAterrisou = true;
                    }
                    break;
                case "Chute":
                    if (state != State.Chutando)
                    {
                        //player.animation.timeScale = 1f;
                        playerSide.animation.Play(chuteAnimation);
                        state = State.Chutando;
                    }
                    break;
                case "Arremesso":
                    if (state != State.Arremessando)
                    {
                        //player.animation.timeScale = 1;
                        playerSide.animation.Play(arremessoAnimation);
                        state = State.Arremessando;
                    }
                    break;
                case "Abaixar":
                    if (state != State.Abaixando)
                    {
                        //player.animation.timeScale = 1;
                        playerSide.animation.Play(abaixarAnimation);
                        state = State.Abaixando;
                    }
                    break;
                case "TransitionAir":
                    if (state != State.TransitionAir)
                    {
                        // player.animation.timeScale = 1.5f;
                        playerSide.animation.Play(transitionJumpAnimation);
                        state = State.TransitionAir;
                    }
                    break;
            }
        }
    }
}
