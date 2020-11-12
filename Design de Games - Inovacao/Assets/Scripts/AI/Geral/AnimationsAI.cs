using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using AI;

namespace Complete
{
    public class AnimationsAI : MonoBehaviour
    {
        public GameObject frente;
        public GameObject lado;

        public bool isKlay;
        public bool doubleArmature;

        public string idleAnim = "0_Idle";
        public string walkAnim = "0_Corrida_V2";
        public string jumpAnim = "1_NoAr(1_Subindo)";
        public string fallAnim = "1_NoAr(2_Descendo)";
        public string landAnim = "1_Aterrisando";
        public string wonAnim = "0_Corrida_V2";
        public string lostAnim = "0_Corrida_V2";
        public string kickAnim = "3_Bicuda";
        public string stunAnim = "0_Corrida_V2";

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

        public enum State { Idle, Walking, Jumping, Rising, Falling, TransitionAir, Landing, Kicking, Abaixando, Arremessando, Inativo, Ganhou, Perdeu, Stun }

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
        public void CallAnim(State _state)
        {
            switch (_state)
            {
                case State.Idle:
                    IdleAnim(true);
                    JumpAnim(false);
                    FallingAnim(false);
                    WalkAnim(false);
                    ChutandoAnim(false);
                    break;

                case State.Walking:
                    WalkAnim(true);
                    JumpAnim(false);
                    FallingAnim(false);
                    IdleAnim(false);
                    ChutandoAnim(false);
                    return;

                case State.Jumping:
                    jaAterrisou = false;
                    JumpAnim(true);
                    FallingAnim(false);
                    WalkAnim(false);
                    IdleAnim(false);
                    ChutandoAnim(false);
                    break;

                case State.Falling:
                    FallingAnim(true);
                    JumpAnim(false);
                    WalkAnim(false);
                    IdleAnim(false);
                    ChutandoAnim(false);
                    break;

                case State.Kicking:
                    ChutandoAnim(true);
                    JumpAnim(false);
                    FallingAnim(false);
                    WalkAnim(false);
                    IdleAnim(false);
                    break;

            }
        }

        public void ChangeAnimAI(Vector2 dir)
        {
            if (triggerController.triggerCollision.ativaAnimChute == false)
            {

                if (dir.y > 1 && controller.collisions.below == false)
                {
                    jaAterrisou = false;
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
                    JumpAnim(false);
                    WalkAnim(false);
                    IdleAnim(false);
                    ChutandoAnim(false);
                    return;
                }

                else if (dir.x != 0 && controller.collisions.below)
                {
                    WalkAnim(true);
                    JumpAnim(false);
                    FallingAnim(false);
                    IdleAnim(false);
                    ChutandoAnim(false);
                    return;
                }
                else if (dir.x == 0 && dir.y == 0)
                {
                    IdleAnim(true);
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
                if (doubleArmature) { ChangeArmature(false); }
                if (idleBotState == null)
                {
                    idleBotState = playerFrente.animation.FadeIn(idleAnim, 0.1f, -1, 12, null, AnimationFadeOutMode.Single);

                    /*if (!isKlay)
                    {
                        ChangeArmature(false);
                        idleBotState = playerFrente.animation.FadeIn(idleAnim, 0.1f, -1, 12, null, AnimationFadeOutMode.Single);
                    }
                    else { idleBotState = playerSide.animation.FadeIn(idleAnim, 0.1f, -1, 12, null, AnimationFadeOutMode.Single); }*/

                    idleBotState.displayControl = true;
                    Debug.Log("IdleAnim == NULL");
                }
                else
                {
                    /*if (!isKlay)
                    {
                        ChangeArmature(false);
                    }*/

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
                if (doubleArmature) { ChangeArmature(false); }
                if (winBotState == null)
                {
                    winBotState = playerFrente.animation.FadeIn(wonAnim, 0.1f, -1, 12, null, AnimationFadeOutMode.Single);
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
                if (doubleArmature) { ChangeArmature(false); }
                if (loseBotState == null)
                {
                    loseBotState = playerFrente.animation.FadeIn(lostAnim, 0.1f, -1, 11, null, AnimationFadeOutMode.Single);
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
                if (doubleArmature) { ChangeArmature(false); }
                if (stunBotState == null)
                {
                    stunBotState = playerFrente.animation.FadeIn(stunAnim, 0.1f, -1, 10, null, AnimationFadeOutMode.Single);
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
                if (doubleArmature) { ChangeArmature(true); }
                if (chuteState == null)
                {
                    chuteState = playerSide.animation.FadeIn(kickAnim, 0.1f, -1, 4, null, AnimationFadeOutMode.None);
                    chuteState.displayControl = true;
                    chuteState.resetToPose = true;
                }
                else
                {
                    chuteState.displayControl = true;
                    chuteState.weight = 1;
                    chuteState.Play();
                }
                state = State.Kicking;
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
                if (doubleArmature) { ChangeArmature(true); }
                if (landBotState == null)
                {
                    landBotState = playerSide.animation.FadeIn(landAnim, 0.1f, -1, 3, null, AnimationFadeOutMode.Single);
                    landBotState.displayControl = true;
                    landBotState.resetToPose = true;
                }
                else
                {
                    landBotState.displayControl = true;
                    landBotState.weight = 1;
                    landBotState.Play();
                }

                state = State.Landing;
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
                if (doubleArmature) { ChangeArmature(true); }
                if (fallBotState == null)
                {
                    fallBotState = playerSide.animation.FadeIn(fallAnim, 0.1f, -1, 20, null, AnimationFadeOutMode.Single);
                    Debug.Log("FallAnim == NULL");
                    /*if (!isKlay)
                    {
                        fallBotState = playerSide.animation.FadeIn("1_NoAr(2_Descendo)", 0.1f, -1, 20, null, AnimationFadeOutMode.Single);
                        Debug.Log("FallAnim == NULL");
                    }
                    else
                    {
                        fallBotState = playerSide.animation.FadeIn("3_Descendo(NoAr)", 0.1f, -1, 20, null, AnimationFadeOutMode.Single);
                    }*/
                    //fallBotState.displayControl = true;
                }
                else
                {
                    /*if (!isKlay)
                    {
                        ChangeArmature(true);
                    }*/

                    fallBotState.displayControl = true;
                    fallBotState.weight = 1;
                    fallBotState.Play();
                    Debug.Log("FallAnim != NULL");
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
                if (doubleArmature) { ChangeArmature(true); }
                if (walkBotState == null)
                {
                    walkBotState = playerSide.animation.FadeIn(walkAnim, -1, -1, 21, null, AnimationFadeOutMode.Single);
                    /*if (!isKlay)
                    {
                        ChangeArmature(true);
                        walkBotState = playerSide.animation.FadeIn("0_Corrida_V2", -1, -1, 21, null, AnimationFadeOutMode.Single);
                        Debug.Log("WalkAnim == NULL");
                    }
                    else { walkBotState = playerSide.animation.FadeIn("1_Run", -1, -1, 21, null, AnimationFadeOutMode.Single); }*/
                    //walkBotState.displayControl = true;
                    Debug.Log("WalkAnim == NULL");
                }
                else
                {
                    /*if (!isKlay)
                    {
                        ChangeArmature(true);
                    }*/

                    walkBotState.displayControl = true;
                    walkBotState.weight = 1f;
                    walkBotState.Play();
                    Debug.Log("WalkAnim != NULL");
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
                if (doubleArmature) { ChangeArmature(true); }
                if (jumpBotState == null)
                {
                    jumpBotState = playerSide.animation.FadeIn(jumpAnim, -1, -1, 22, null, AnimationFadeOutMode.Single);
                    Debug.Log("JumpAnim == NULL");
                    /*if (!isKlay)
                    {
                        ChangeArmature(true);
                        jumpBotState = playerSide.animation.FadeIn("1_NoAr(1_Subindo)", -1, -1, 22, null, AnimationFadeOutMode.Single);
                        Debug.Log("JumpAnim = NULL");
                    }
                    else { jumpBotState = playerSide.animation.FadeIn("3_Subindo(NoAr)", -1, -1, 22, null, AnimationFadeOutMode.Single); }*/
                    //                    jumpBotState.displayControl = true;
                }
                else
                {
                    /*if (!isKlay)
                    {
                        ChangeArmature(true);
                    }*/
                    Debug.Log("JumpAnim != NULL");

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
            if(isSide == lado.activeInHierarchy) { return; }
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
