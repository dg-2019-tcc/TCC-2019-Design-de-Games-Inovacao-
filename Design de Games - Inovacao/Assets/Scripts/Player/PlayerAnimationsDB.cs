using System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Complete;
using Kintal;

public class PlayerAnimationsDB : MonoBehaviour
{
    [HideInInspector]
    public PhotonView photonView;

    public GameObject frente;
    public GameObject lado;

    public UnityArmatureComponent playerLado;
    public UnityArmatureComponent playerFrente;

    private DragonBones.AnimationState idleState = null;
    private DragonBones.AnimationState stunState = null;
    private DragonBones.AnimationState winState = null;
    private DragonBones.AnimationState loseState = null;

    private DragonBones.AnimationState jumpState = null;
    private DragonBones.AnimationState fallState = null;
    private DragonBones.AnimationState landState = null;
    private DragonBones.AnimationState walkState = null;

    private DragonBones.AnimationState chuteState = null;
    private DragonBones.AnimationState tiroState = null;

    private DragonBones.AnimationState carroWalkState = null;
    private DragonBones.AnimationState carroUpState = null;
    private DragonBones.AnimationState carroDownState = null;
    private DragonBones.AnimationState pipaState = null;

    public AnimStateFrente stateFrente;
    public AnimStatePowerUp statePowerUp;
    public AnimStateAction stateAction;
    public AnimStateMovement stateMovement;

    private bool updateMove;
    [HideInInspector]
    public bool updateCar;
    private bool canFadeIn;

    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
    }

    public void StateFrenteUpdate()
    {
        if(frente.activeInHierarchy == false)
        {
            frente.SetActive(true);
            lado.SetActive(false);
        }
        if(stateFrente != AnimStateFrente.None)
        {
            if(GameManager.inRoom == false)
            {
                switch (stateFrente)
                {
                    case AnimStateFrente.Idle:
                        IdleAnim(true);

                        StunAnim(false);
                        PerdeuAnim(false);
                        GanhouAnim(false);
                        break;

                    case AnimStateFrente.Stun:
                        StunAnim(true);

                        IdleAnim(false);
                        PerdeuAnim(false);
                        GanhouAnim(false);
                        break;

                    case AnimStateFrente.Perdeu:
                        PerdeuAnim(true);

                        IdleAnim(false);
                        StunAnim(false);
                        GanhouAnim(false);
                        break;

                    case AnimStateFrente.Ganhou:
                        GanhouAnim(true);

                        IdleAnim(false);
                        StunAnim(false);
                        PerdeuAnim(false);
                        break;
                }
            }
            else
            {
                switch (stateFrente)
                {
                    case AnimStateFrente.Idle:
                        photonView.RPC("IdleAnim", RpcTarget.All, true);

                        photonView.RPC("StunAnim", RpcTarget.All, false);
                        photonView.RPC("PerdeuAnim", RpcTarget.All, false);
                        photonView.RPC("GanhouAnim", RpcTarget.All, false);
                        break;

                    case AnimStateFrente.Stun:
                        photonView.RPC("StunAnim", RpcTarget.All, true);

                        photonView.RPC("IdleAnim", RpcTarget.All, false);
                        photonView.RPC("PerdeuAnim", RpcTarget.All, false);
                        photonView.RPC("GanhouAnim", RpcTarget.All, false);
                        break;

                    case AnimStateFrente.Perdeu:
                        photonView.RPC("PerdeuAnim", RpcTarget.All, true);

                        photonView.RPC("IdleAnim", RpcTarget.All, false);
                        photonView.RPC("StunAnim", RpcTarget.All, false);
                        photonView.RPC("GanhouAnim", RpcTarget.All, false);
                        break;

                    case AnimStateFrente.Ganhou:
                        photonView.RPC("GanhouAnim", RpcTarget.All, true);

                        photonView.RPC("IdleAnim", RpcTarget.All, false);
                        photonView.RPC("StunAnim", RpcTarget.All, false);
                        photonView.RPC("PerdeuAnim", RpcTarget.All, false);
                        break;
                }
            }
        }
    }

    public void StatePowerUpUpdate(bool play)
    {
        if (play)
        {
            if (lado.activeInHierarchy == false)
            {
                lado.SetActive(true);
                frente.SetActive(false);
            }
            StateMoveUpdate(false);
            if (statePowerUp != AnimStatePowerUp.None)
            {
                if (GameManager.inRoom == false)
                {
                    switch (statePowerUp)
                    {
                        case AnimStatePowerUp.Pipa:
                            PipaAnim(true);
                            break;

                        case AnimStatePowerUp.CarroWalk:
                            CarroWalkAnim(true);

                            CarroUpAnim(false);
                            CarroDownAnim(false);
                            break;

                        case AnimStatePowerUp.CarroUp:
                            CarroUpAnim(true);

                            CarroWalkAnim(false);
                            CarroDownAnim(false);
                            break;

                        case AnimStatePowerUp.CarroDown:
                            CarroDownAnim(true);

                            CarroWalkAnim(false);
                            CarroUpAnim(false);
                            break;
                    }
                }
                else
                {
                    switch (statePowerUp)
                    {
                        case AnimStatePowerUp.Pipa:
                            photonView.RPC("PipaAnim", RpcTarget.All, true);
                            break;

                        case AnimStatePowerUp.CarroWalk:
                            photonView.RPC("CarroWalkAnim", RpcTarget.All, true);

                            photonView.RPC("CarroUpAnim", RpcTarget.All, false);
                            photonView.RPC("CarroDownAnim", RpcTarget.All, false);
                            break;

                        case AnimStatePowerUp.CarroUp:
                            photonView.RPC("CarroUpAnim", RpcTarget.All, true);

                            photonView.RPC("CarroWalkAnim", RpcTarget.All, false);
                            photonView.RPC("CarroDownAnim", RpcTarget.All, false);
                            break;

                        case AnimStatePowerUp.CarroDown:
                            photonView.RPC("CarroDownAnim", RpcTarget.All, true);

                            photonView.RPC("CarroWalkAnim", RpcTarget.All, false);
                            photonView.RPC("CarroUpAnim", RpcTarget.All, false);
                            break;
                    }
                }
            }
        }
        else
        {
            if (pipaState != null) if (GameManager.inRoom) { photonView.RPC("PipaAnim", RpcTarget.All, false); } else { PipaAnim(false); }
            if (carroDownState != null) if (GameManager.inRoom) { photonView.RPC("CarroDownAnim", RpcTarget.All, false); } else { CarroDownAnim(false); }
            if (carroUpState != null) if (GameManager.inRoom) { photonView.RPC("CarroUpAnim", RpcTarget.All, false); } else { CarroUpAnim(false); }
            if (carroWalkState != null) if (GameManager.inRoom) { photonView.RPC("CarroWalkAnim", RpcTarget.All, false); } else { CarroWalkAnim(false); }
        }
    }


    public void StateActionUpdate(bool play)
    {
        if (play)
        {
            if (lado.activeInHierarchy == false)
            {
                lado.SetActive(true);
                frente.SetActive(false);
            }
            StateMoveUpdate(false);

            if (stateAction != AnimStateAction.None)
            {
                switch (stateAction)
                {
                    case AnimStateAction.Chute:
                        if (GameManager.inRoom == false) { ChutandoAnim(true); }
                        else { photonView.RPC("ChutandoAnim", RpcTarget.All, true); }
                        break;

                    case AnimStateAction.Arremesando:
                        if (GameManager.inRoom == false) { ArremessandoAnim(true); }
                        else { photonView.RPC("ArremessandoAnim", RpcTarget.All, true); }
                        break;
                }
            }
        }
        else
        {
            if(chuteState != null)
            {
                if (GameManager.inRoom == false) { ChutandoAnim(false); }
                else { photonView.RPC("ChutandoAnim", RpcTarget.All, false); }
            }

            if(tiroState != null)
            {
                if (GameManager.inRoom == false) { ArremessandoAnim(false); }
                else { photonView.RPC("ArremessandoAnim", RpcTarget.All, false); }
            }
        }
    }

    public void StateMoveUpdate(bool play)
    {
        if (!play)
        {
            updateMove = false;
            if (GameManager.inRoom == false)
            {
                WalkAnim(false);
                JumpAnim(false);
                FallingAnim(false);
                AterrisandoAnim(false);
            }
            else
            {
                photonView.RPC("WalkAnim", RpcTarget.All, false);
                photonView.RPC("JumpAnim", RpcTarget.All, false);
                photonView.RPC("FallingAnim", RpcTarget.All, false);
                photonView.RPC("AterrisandoAnim", RpcTarget.All, false);
            }
        }
        else
        {
            updateMove = true;
            if (lado.activeInHierarchy == false)
            {
                lado.SetActive(true);
                frente.SetActive(false);
            }
            if (stateMovement != AnimStateMovement.None)
            {
                if (GameManager.inRoom == false)
                {
                    switch (stateMovement)
                    {
                        case AnimStateMovement.Walk:
                            WalkAnim(true);

                            JumpAnim(false);
                            FallingAnim(false);
                            AterrisandoAnim(false);
                            break;

                        case AnimStateMovement.Rising:
                            JumpAnim(true);

                            FallingAnim(false);
                            AterrisandoAnim(false);
                            WalkAnim(false);
                            break;

                        case AnimStateMovement.Falling:
                            FallingAnim(true);

                            AterrisandoAnim(false);
                            WalkAnim(false);
                            JumpAnim(false);
                            break;

                        case AnimStateMovement.Aterrisando:
                            AterrisandoAnim(true);

                            FallingAnim(false);
                            WalkAnim(false);
                            JumpAnim(false);
                            break;

                    }
                }
                else
                {
                    switch (stateMovement)
                    {
                        case AnimStateMovement.Walk:
                            photonView.RPC("WalkAnim", RpcTarget.All, true);

                            photonView.RPC("JumpAnim", RpcTarget.All, false);
                            photonView.RPC("FallingAnim", RpcTarget.All, false);
                            photonView.RPC("AterrisandoAnim", RpcTarget.All, false);
                            break;

                        case AnimStateMovement.Rising:
                            photonView.RPC("JumpAnim", RpcTarget.All, true);

                            photonView.RPC("WalkAnim", RpcTarget.All, false);
                            photonView.RPC("FallingAnim", RpcTarget.All, false);
                            photonView.RPC("AterrisandoAnim", RpcTarget.All, false);
                            break;

                        case AnimStateMovement.Falling:
                            photonView.RPC("FallingAnim", RpcTarget.All, true);

                            photonView.RPC("WalkAnim", RpcTarget.All, false);
                            photonView.RPC("JumpAnim", RpcTarget.All, false);
                            photonView.RPC("AterrisandoAnim", RpcTarget.All, false);
                            break;

                        case AnimStateMovement.Aterrisando:
                            photonView.RPC("AterrisandoAnim", RpcTarget.All, true);

                            photonView.RPC("WalkAnim", RpcTarget.All, false);
                            photonView.RPC("JumpAnim", RpcTarget.All, false);
                            photonView.RPC("FallingAnim", RpcTarget.All, false);
                            break;

                    }
                }
            }
        }

    }


    //Animações do StateFrente
    [PunRPC]
    public void IdleAnim(bool play)
    {
        if (play)
        {
            if (idleState == null)
            {
                idleState = playerFrente.animation.Play("0_Idle");
                idleState.displayControl = true;
            }
            else
            {
                idleState.displayControl = true;
                idleState.weight = 1;
                idleState.Play();
            }
        }
        else
        {
            if (idleState == null) return;
            else
            {
                idleState.displayControl = fallState;
                idleState.weight = 0;
                idleState.Stop();
            }
        }

    }

    [PunRPC]
    public void GanhouAnim(bool play)
    {
        if (play)
        {
            if (winState == null)
            {
                winState = playerFrente.animation.FadeIn("2_Vencer", 0.1f, -1, 12, null, AnimationFadeOutMode.Single);
                winState.displayControl = true;
            }
            else
            {
                winState.displayControl = true;
                winState.weight = 1;
                winState.Play();
            }
        }
        else
        {
            if (winState == null) return;
            else
            {
                winState.displayControl = false;
                winState.weight = 0;
                winState.Stop();
            }
        }
    }

    [PunRPC]
    public void PerdeuAnim(bool play)
    {
        if (play)
        {
            if (loseState == null)
            {
                loseState = playerFrente.animation.FadeIn("2_Perder", 0.1f, -1, 11, null, AnimationFadeOutMode.Single);
                loseState.displayControl = true;
            }
            else
            {
                loseState.displayControl = true;
                loseState.weight = 1;
                loseState.Play();
            }
        }
        else
        {
            if (loseState == null) return;
            else
            {
                loseState.displayControl = false;
                loseState.weight = 0;
                loseState.Stop();
            }
        }
    }


    [PunRPC] public void StunAnim(bool play)
    {
        if (play)
        {
            if (stunState == null)
            {
                stunState = playerFrente.animation.FadeIn("3_Atordoado", 0.1f, -1, 10, null, AnimationFadeOutMode.Single);
                stunState.displayControl = true;
            }
            else
            {
                stunState.displayControl = true;
                stunState.weight = 1;
                stunState.Play();
            }
        }
        else
        {
            if (stunState == null) return;
            else
            {
                stunState.displayControl = false;
                stunState.weight = 0;
                stunState.Stop();
            }
        }
    }

    //Animações do statePowerUp
    [PunRPC]
    public void PipaAnim(bool play)
    {
        if (play)
        {
            if (pipaState == null)
            {
                pipaState = playerLado.animation.FadeIn("7_Pipa", 0.1f, -1, 9, null, AnimationFadeOutMode.Single);
                pipaState.displayControl = true;
            }
            else
            {
                pipaState.displayControl = true;
                pipaState.weight = 1;
                pipaState.Play();
            }
        }
        else
        {
            if (pipaState == null) return;
            else
            {
                var pipaCarretel = playerLado.armature.GetSlot("Pipa_CArretel");
                var pipaStrokeNariz = playerLado.armature.GetSlot("Stroke_Pipanariz");
                var pipaStrokeBase = playerLado.armature.GetSlot("Stroke_Pipabase");
                var pipaRabo = playerLado.armature.GetSlot("PipaRabo");
                var pipaOrelhaBaixo = playerLado.armature.GetSlot("Pipa_Orelha_Baixo");
                var pipaOrelhaCima = playerLado.armature.GetSlot("Pipa_Orelha_Cima");
                var pipaBase = playerLado.armature.GetSlot("Pipa_Base");
                var pipaNariz = playerLado.armature.GetSlot("Pipa_Nariz");

                pipaCarretel.display = null;
                pipaStrokeNariz.display = null;
                pipaStrokeBase.display = null;
                pipaRabo.display = null;
                pipaOrelhaBaixo.display = null;
                pipaOrelhaCima.display = null;
                pipaBase.display = null;
                pipaNariz.display = null;

                pipaState.displayControl = false;
                pipaState.weight = 0;
                pipaState.Stop();
            }
        }
    }

    [PunRPC]
    public void CarroWalkAnim(bool play)
    {
        if (play)
        {
            if (carroWalkState == null)
            {
                carroWalkState = playerLado.animation.FadeIn("6_Rolima(Andando)", 0.1f, -1, 8, null, AnimationFadeOutMode.None);
                carroWalkState.displayControl = true;
            }
            else
            {
                carroWalkState.displayControl = true;
                carroWalkState.weight = 1;
                carroWalkState.Play();
            }
        }
        else
        {
            if (carroWalkState == null) return;
            else
            {
                if (!updateCar)
                {
                    var rolimaStrokw = playerLado.armature.GetSlot("Rolima_Strokw");
                    var rolimaRabo = playerLado.armature.GetSlot("Rolima_Rabo");
                    var rolimaBase = playerLado.armature.GetSlot("Rolima_Base");
                    var rolimaRoda1 = playerLado.armature.GetSlot("Rolima_Roda1");
                    var rolimaRoda2 = playerLado.armature.GetSlot("Rolima_Roda2");
                    var rolimaSombras = playerLado.armature.GetSlot("Rolima_Sombras");

                    rolimaStrokw.display = null;
                    rolimaRabo.display = null;
                    rolimaBase.display = null;
                    rolimaRoda1.display = null;
                    rolimaRoda2.display = null;
                    rolimaSombras.display = null;
                }

                carroWalkState.displayControl = false;
                carroWalkState.weight = 0;
                carroWalkState.Stop();
            }
        }
    }
    [PunRPC]
    public void CarroUpAnim(bool play)
    {
        if (play)
        {
            if (carroUpState == null)
            {
                carroUpState = playerLado.animation.FadeIn("6_Rolima(SubindoNoAr)", 0.1f, -1, 7, null, AnimationFadeOutMode.None);
                carroUpState.displayControl = true;
            }
            else
            {
                carroUpState.displayControl = true;
                carroUpState.weight = 1;
                carroUpState.Play();
            }
        }
        else
        {
            if (carroUpState == null) return;
            else
            {
                if (!updateCar)
                {
                    var rolimaStrokw = playerLado.armature.GetSlot("Rolima_Strokw");
                    var rolimaRabo = playerLado.armature.GetSlot("Rolima_Rabo");
                    var rolimaBase = playerLado.armature.GetSlot("Rolima_Base");
                    var rolimaRoda1 = playerLado.armature.GetSlot("Rolima_Roda1");
                    var rolimaRoda2 = playerLado.armature.GetSlot("Rolima_Roda2");
                    var rolimaSombras = playerLado.armature.GetSlot("Rolima_Sombras");

                    rolimaStrokw.display = null;
                    rolimaRabo.display = null;
                    rolimaBase.display = null;
                    rolimaRoda1.display = null;
                    rolimaRoda2.display = null;
                    rolimaSombras.display = null;
                }

                carroUpState.displayControl = false;
                carroUpState.weight = 0;
                carroUpState.Stop();
            }
        }
    }
    [PunRPC]
    public void CarroDownAnim(bool play)
    {
        if (play)
        {
            if(carroDownState == null)
            {
                carroDownState = playerLado.animation.FadeIn("6_Rolima(DescendoNoAr)", 0.1f, -1, 6, null, AnimationFadeOutMode.None);
                carroDownState.displayControl = true;
            }
            else
            {
                carroDownState.displayControl = true;
                carroDownState.weight = 1;
                carroDownState.Play();
            }
        }
        else
        {
            if (carroDownState == null) return;
            else
            {
                if (!updateCar)
                {
                    var rolimaStrokw = playerLado.armature.GetSlot("Rolima_Strokw");
                    var rolimaRabo = playerLado.armature.GetSlot("Rolima_Rabo");
                    var rolimaBase = playerLado.armature.GetSlot("Rolima_Base");
                    var rolimaRoda1 = playerLado.armature.GetSlot("Rolima_Roda1");
                    var rolimaRoda2 = playerLado.armature.GetSlot("Rolima_Roda2");
                    var rolimaSombras = playerLado.armature.GetSlot("Rolima_Sombras");

                    rolimaStrokw.display = null;
                    rolimaRabo.display = null;
                    rolimaBase.display = null;
                    rolimaRoda1.display = null;
                    rolimaRoda2.display = null;
                    rolimaSombras.display = null;
                }

                carroDownState.displayControl = false;
                carroDownState.weight = 0;
                carroDownState.Stop();
            }
        }
    }

    // Animações do stateAction
    [PunRPC]
    public void ArremessandoAnim(bool play)
    {

        if (play)
        {
            if (tiroState == null)
            {
                tiroState = playerLado.animation.FadeIn("5_Arremessar", 0.1f, -1, 5, null, AnimationFadeOutMode.Single);
                tiroState.resetToPose = true;
                tiroState.displayControl = true;
            }
            else
            {
                tiroState.displayControl = true;
                tiroState.weight = 1;
                tiroState.Play();
            }
        }
        else
        {
            if (tiroState == null) return;
            else
            {
                tiroState.displayControl = false;
                tiroState.Stop();
                tiroState.weight = 0;
            }
        }


    }
    [PunRPC]
    public void ChutandoAnim(bool play)
    {
        if (play)
        {
            if (chuteState == null)
            {
                chuteState = playerLado.animation.FadeIn("3_Bicuda", 0.1f, -1, 4, null, AnimationFadeOutMode.Single);
                chuteState.displayControl = true;
                chuteState.resetToPose = true;
            }
            else
            {
                chuteState.displayControl = true;
                chuteState.weight = 1;
                chuteState.Play();
            }
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
    [PunRPC]
    public void AterrisandoAnim(bool play)
    {
        if (play)
        {
            if (landState == null)
            {
                landState = playerLado.animation.FadeIn("1_Aterrisando", 0.1f, -1, 3, null, AnimationFadeOutMode.Single);
                landState.displayControl = true;
                landState.resetToPose = true;
            }
            else
            {
                landState.displayControl = true;
                landState.weight = 1;
                landState.Play();
            }
        }
        else
        {
            if (landState == null) return;
            else
            {
                landState.displayControl = false;
                landState.Stop();
                landState.weight = 0;
            }
        }

    }


    [PunRPC]
    public void FallingAnim(bool play)
    {
        if (play)
        {
            if (fallState == null)
            {
                fallState = playerLado.animation.FadeIn("1_NoAr(2_Descendo)", 0.1f, -1, 20, null, AnimationFadeOutMode.Single);
                fallState.displayControl = true;
            }
            else
            {
                fallState.displayControl = true;
                fallState.weight = 1;
                fallState.Play();
            }
        }
        else
        {
            if (fallState == null) { return; }
            else
            {
                fallState.displayControl = false;
                fallState.Stop();
                fallState.weight = 0;
            }
        }

    }
    [PunRPC]
    public void WalkAnim(bool play)
    {
        if (play)
        {
            if (walkState == null)
            {
                walkState = playerLado.animation.FadeIn("0_Corrida_V2", -1, -1, 21, null, AnimationFadeOutMode.SameLayer);
                walkState.displayControl = true;
            }
            else
            {
                walkState.displayControl = true;
                walkState.weight = 1f;
                walkState.Play();
            }
        }

        else
        {
            if (walkState == null) return;
            else
            {
                walkState.displayControl = false;
                walkState.Stop();
                walkState.weight = 0;
            }
        }



    }

    [PunRPC]
    public void JumpAnim(bool play)
    {
        if (play)
        {
            if (jumpState == null)
            {
                jumpState = playerLado.animation.FadeIn("1_NoAr(1_Subindo)", -1, -1, 22, null, AnimationFadeOutMode.Single);
                jumpState.displayControl = true;
            }
            else
            {
                jumpState.displayControl = true;
                jumpState.weight = 1;
                jumpState.Play();
            }
        }
        else
        {
            if (jumpState == null) return;
            else
            {
                jumpState.displayControl = false;
                jumpState.Stop();
                jumpState.weight = 0;
            }
        }
    }
}
