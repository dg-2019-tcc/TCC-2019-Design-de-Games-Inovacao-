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

        public readonly string idlePose = "0_Idle";
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

        private DragonBones.AnimationState idleBotState = null;
        private DragonBones.AnimationState stunBotState = null;
        private DragonBones.AnimationState winBotState = null;
        private DragonBones.AnimationState loseBotState = null;

        private DragonBones.AnimationState jumpBotState = null;
        private DragonBones.AnimationState fallBotState = null;
        private DragonBones.AnimationState landBotState = null;
        private DragonBones.AnimationState walkBotState = null;

        private DragonBones.AnimationState chuteState = null;
        private DragonBones.AnimationState tiroState = null;

        public enum State { Idle, Walking, Jumping, Rising, Falling, TransitionAir, Aterrisando, Chutando, Abaixando, Arremessando, Inativo, Ganhou, Perdeu, Stun }

        public State state = State.Idle;

        private AIController2D controller;
        private AITriggerController triggerController;



        private UnityArmatureComponent playerSide;
        private UnityArmatureComponent playerFrente;

        [SerializeField]
        private bool jaAterrisou;

        public bool isFut;

        #region Unity Function
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
        #endregion

        #region Public Functions
        public void ChangeAnimAI(Vector2 dir)
        {
            if (triggerController.triggerCollision.ativaAnimChute == false)
            {

                if (dir.y > 1 && controller.collisions.below == false)
                {
                    jaAterrisou = false;
                    //AnimState("NoArUp");
                    JumpAnim(true);

                    FallingAnim(false);
                    WalkAnim(false);
                    IdleAnim(false);
                    ChutandoAnim(false);
                    return;
                }

                else if (dir.y < 0 && controller.collisions.below == false)
                {
                    FallingAnim(true);
                    //AnimState("Fall");

                    JumpAnim(false);
                    WalkAnim(false);
                    IdleAnim(false);
                    ChutandoAnim(false);
                    return;
                }

                else if (dir.x != 0 && controller.collisions.below)
                {
                    WalkAnim(true);
                    //AnimState("Walking");

                    JumpAnim(false);
                    FallingAnim(false);
                    IdleAnim(false);
                    ChutandoAnim(false);
                    return;
                }
                else if (dir.x == 0 && dir.y == 0)
                {
                    IdleAnim(true);
                    //AnimState("Idle");

                    JumpAnim(false);
                    FallingAnim(false);
                    WalkAnim(false);
                    ChutandoAnim(false);
                    return;
                }
            }

            else
            {
                ChutandoAnim(true);
                //AnimState("Chute");
                JumpAnim(false);
                FallingAnim(false);
                WalkAnim(false);
                IdleAnim(false);
                return;
            }
        }
        #endregion

        #region Private Functions
        private void IdleAnim(bool play)
        {
            if (play)
            {
                if (state == State.Idle) return;

                if (idleBotState == null)
                {
                    if (!isKlay)
                    {
                        ChangeArmature(false);
                        idleBotState = playerFrente.animation.FadeIn("0_Idle", 0.1f, -1, 12, null, AnimationFadeOutMode.Single);
                    }
                    else { idleBotState = playerSide.animation.FadeIn("0_Idle", 0.1f, -1, 12, null, AnimationFadeOutMode.Single); }

                    idleBotState.displayControl = true;
                    Debug.Log("IdleAnim == NULL");
                }
                else
                {
                    if (!isKlay)
                    {
                        ChangeArmature(false);
                    }
                    idleBotState.displayControl = true;
                    idleBotState.weight = 1;
                    idleBotState.Play();
                    Debug.Log("IdleAnim != NULL");
                }

                state = State.Idle;
            }
            else
            {
                if (idleBotState == null) return;
                else
                {
                    idleBotState.displayControl = fallBotState;
                    idleBotState.weight = 0;
                    idleBotState.Stop();
                }
            }

        }

        private void GanhouAnim(bool play)
        {
            if (play)
            {
                if (winBotState == null)
                {
                    winBotState = playerFrente.animation.FadeIn("2_Vencer", 0.1f, -1, 12, null, AnimationFadeOutMode.Single);
                    winBotState.displayControl = true;
                }
                else
                {
                    winBotState.displayControl = true;
                    winBotState.weight = 1;
                    winBotState.Play();
                }

                state = State.Ganhou;
            }
            else
            {
                if (winBotState == null) return;
                else
                {
                    winBotState.displayControl = false;
                    winBotState.weight = 0;
                    winBotState.Stop();
                }
            }
        }

        private void PerdeuAnim(bool play)
        {
            if (play)
            {
                if (loseBotState == null)
                {
                    loseBotState = playerFrente.animation.FadeIn("2_Perder", 0.1f, -1, 11, null, AnimationFadeOutMode.Single);
                    loseBotState.displayControl = true;
                }
                else
                {
                    loseBotState.displayControl = true;
                    loseBotState.weight = 1;
                    loseBotState.Play();
                }

                state = State.Perdeu;
            }
            else
            {
                if (loseBotState == null) return;
                else
                {
                    loseBotState.displayControl = false;
                    loseBotState.weight = 0;
                    loseBotState.Stop();
                }
            }
        }


        private void StunAnim(bool play)
        {
            if (play)
            {
                if (stunBotState == null)
                {
                    stunBotState = playerFrente.animation.FadeIn("3_Atordoado", 0.1f, -1, 10, null, AnimationFadeOutMode.Single);
                    stunBotState.displayControl = true;
                }
                else
                {
                    stunBotState.displayControl = true;
                    stunBotState.weight = 1;
                    stunBotState.Play();
                }

                state = State.Stun;
            }
            else
            {
                if (stunBotState == null) return;
                else
                {
                    stunBotState.displayControl = false;
                    stunBotState.weight = 0;
                    stunBotState.Stop();
                }
            }
        }

        private void ChutandoAnim(bool play)
        {
            if (play)
            {
                if (chuteState == null)
                {
                    chuteState = playerSide.animation.FadeIn("3_Bicuda", 0.1f, -1, 4, null, AnimationFadeOutMode.None);
                    chuteState.displayControl = true;
                    chuteState.resetToPose = true;
                }
                else
                {
                    chuteState.displayControl = true;
                    chuteState.weight = 1;
                    chuteState.Play();
                }
                state = State.Chutando;
            }
            else
            {
                if (chuteState == null) return;
                else
                {
                    chuteState.displayControl = false;
                    chuteState.Stop();
                    chuteState.weight = 0;
                }
            }
        }

        // Animações do stateMovement
        private void AterrisandoAnim(bool play)
        {
            if (play)
            {
                WalkAnim(false);
                JumpAnim(false);
                FallingAnim(false);

                if (landBotState == null)
                {
                    landBotState = playerSide.animation.FadeIn("1_Aterrisando", 0.1f, -1, 3, null, AnimationFadeOutMode.Single);
                    landBotState.displayControl = true;
                    landBotState.resetToPose = true;
                }
                else
                {
                    landBotState.displayControl = true;
                    landBotState.weight = 1;
                    landBotState.Play();
                }

                state = State.Aterrisando;
            }
            else
            {
                if (landBotState == null) return;
                else
                {
                    landBotState.displayControl = false;
                    landBotState.Stop();
                    landBotState.weight = 0;
                }
            }

        }


        private void FallingAnim(bool play)
        {
            if (play)
            {
                WalkAnim(false);
                JumpAnim(false);
                AterrisandoAnim(false);

                if (fallBotState == null)
                {
                    if (!isKlay)
                    {
                        ChangeArmature(true);
                        fallBotState = playerSide.animation.FadeIn("1_NoAr(2_Descendo)", 0.1f, -1, 20, null, AnimationFadeOutMode.Single);
                        Debug.Log("FallAnim == NULL");
                    }
                    else
                    {
                        fallBotState = playerSide.animation.FadeIn("3_Descendo(NoAr)", 0.1f, -1, 20, null, AnimationFadeOutMode.Single);
                    }
                    //fallBotState.displayControl = true;
                }
                else
                {
                    if (!isKlay)
                    {
                        ChangeArmature(true);
                    }

                    fallBotState.displayControl = true;
                    fallBotState.weight = 1;
                    fallBotState.Play();
                }
                state = State.Falling;
            }
            else
            {
                if (fallBotState == null) { return; }
                else
                {
                    fallBotState.displayControl = false;
                    fallBotState.Stop();
                    fallBotState.weight = 0;
                }
            }

        }
        private void WalkAnim(bool play)
        {
            if (play)
            {
                JumpAnim(false);
                FallingAnim(false);
                AterrisandoAnim(false);

                if (walkBotState == null)
                {
                    if (!isKlay)
                    {
                        ChangeArmature(true);
                        walkBotState = playerSide.animation.FadeIn("0_Corrida_V2", -1, -1, 21, null, AnimationFadeOutMode.Single);
                        Debug.Log("WalkAnim == NULL");
                    }
                    else { walkBotState = playerSide.animation.FadeIn("1_Run", -1, -1, 21, null, AnimationFadeOutMode.Single); }
                    //walkBotState.displayControl = true;
                    Debug.Log("WalkAnim == NULL");
                }
                else
                {
                    if (!isKlay)
                    {
                        ChangeArmature(true);
                    }

                    walkBotState.displayControl = true;
                    walkBotState.weight = 1f;
                    walkBotState.Play();
                }
                state = State.Walking;
            }

            else
            {
                if (walkBotState == null) return;
                else
                {
                    walkBotState.displayControl = false;
                    walkBotState.Stop();
                    walkBotState.weight = 0;
                }
            }



        }

        private void JumpAnim(bool play)
        {
            if (play)
            {
                WalkAnim(false);
                FallingAnim(false);
                AterrisandoAnim(false);

                if (jumpBotState == null)
                {
                    if (!isKlay)
                    {
                        ChangeArmature(true);
                        jumpBotState = playerSide.animation.FadeIn("1_NoAr(1_Subindo)", -1, -1, 22, null, AnimationFadeOutMode.Single);
                        Debug.Log("JumpAnim = NULL");
                    }
                    else { jumpBotState = playerSide.animation.FadeIn("3_Subindo(NoAr)", -1, -1, 22, null, AnimationFadeOutMode.Single); }
                    //                    jumpBotState.displayControl = true;
                }
                else
                {
                    if (!isKlay)
                    {
                        ChangeArmature(true);
                    }

                    jumpBotState.displayControl = true;
                    jumpBotState.weight = 1;
                    jumpBotState.Play();
                }
                state = State.Rising;
            }
            else
            {
                if (jumpBotState == null) return;
                else
                {
                    jumpBotState.displayControl = false;
                    jumpBotState.Stop();
                    jumpBotState.weight = 0;
                }
            }
        }

        private void ChangeArmature(bool isSide)
        {
            if (isSide)
            {
                lado.SetActive(true);
                frente.SetActive(false);
            }
            else
            {
                lado.SetActive(false);
                frente.SetActive(true);
            }
        }
        #endregion
    }
}
