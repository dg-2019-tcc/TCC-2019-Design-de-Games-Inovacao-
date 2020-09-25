using System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Complete;

public class PlayerAnimationsDB : MonoBehaviour
{
    [HideInInspector]
    public PhotonView photonView;

    public GameObject frente;
    public GameObject lado;

    public UnityArmatureComponent playerLado;
    public UnityArmatureComponent playerFrente;

    public DragonBones.AnimationState idleState = null;
    public DragonBones.AnimationState stunState = null;
    public DragonBones.AnimationState winState = null;
    public DragonBones.AnimationState loseState = null;

    public DragonBones.AnimationState jumpState = null;
    public DragonBones.AnimationState fallState = null;
    public DragonBones.AnimationState landState = null;
    public DragonBones.AnimationState walkState = null;

    public DragonBones.AnimationState chuteState = null;
    public DragonBones.AnimationState tiroState = null;

    public DragonBones.AnimationState carroWalkState = null;
    public DragonBones.AnimationState carroUpState = null;
    public DragonBones.AnimationState carroDownState = null;
    public DragonBones.AnimationState pipaState = null;

    public enum State { Idle, Walking, Rising, Falling, Aterrisando, Chutando, Arremessando, Inativo, Pipa, CarroWalk, CarroUp, CarroDown, Stun, Ganhou, Perdeu, Null }

    public State state = State.Idle;



    public AnimStateFrente stateFrente;
    public AnimStatePowerUp statePowerUp;
    public AnimStateAction stateAction;
    public AnimStateMovement stateMovement;

    public GameObject[] pipaMesh;
    public GameObject[] carroMesh;

    public Landing landParticles;

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
            switch (stateFrente)
            {
                case AnimStateFrente.Idle:
                    if (!GameManager.inRoom) IdleAnim(true);
                    else photonView.RPC("IdleAnim", RpcTarget.All, true);
                    break;

                case AnimStateFrente.Stun:
                    if (!GameManager.inRoom) StunAnim(true);
                    else photonView.RPC("StunAnim", RpcTarget.All, true);
                    break;

                case AnimStateFrente.Perdeu:
                    if (!GameManager.inRoom) PerdeuAnim(true);
                    else photonView.RPC("PerdeuAnim", RpcTarget.All, true);
                    break;

                case AnimStateFrente.Ganhou:
                    if (!GameManager.inRoom) GanhouAnim(true);
                    else photonView.RPC("GanhouAnim", RpcTarget.All, true);
                    break;
            }

            if(stateFrente != AnimStateFrente.Idle)
            {
                if (!GameManager.inRoom) IdleAnim(false);
                else photonView.RPC("IdleAnim", RpcTarget.All, false);
            }

            if (stateFrente != AnimStateFrente.Ganhou && winState !=null)
            {
                if (!GameManager.inRoom) GanhouAnim(false);
                else photonView.RPC("GanhouAnim", RpcTarget.All, false);
            }

            if (stateFrente != AnimStateFrente.Perdeu && loseState !=null)
            {
                if (!GameManager.inRoom) PerdeuAnim(false);
                else photonView.RPC("PerdeuAnim", RpcTarget.All, false);
            }

            if (stateFrente != AnimStateFrente.Stun && stunState != null)
            {
                if (!GameManager.inRoom) StunAnim(false);
                else photonView.RPC("StunAnim", RpcTarget.All, false);
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
                switch (statePowerUp)
                {
                    case AnimStatePowerUp.Pipa:
                        if (state == State.Pipa) return;
                        if (!GameManager.inRoom) PipaAnim(true);
                        else photonView.RPC("PipaAnim", RpcTarget.All, true);
                        break;

                    case AnimStatePowerUp.CarroWalk:
                        if (state == State.CarroWalk) return;
                        if (!GameManager.inRoom) CarroWalkAnim(true);
                        else photonView.RPC("CarroWalkAnim", RpcTarget.All, true);
                        break;

                    case AnimStatePowerUp.CarroUp:
                        if (state == State.CarroUp) return;
                        if (!GameManager.inRoom) CarroUpAnim(true);
                        else photonView.RPC("CarroUpAnim", RpcTarget.All, true);
                        break;

                    case AnimStatePowerUp.CarroDown:
                        if (state == State.CarroDown) return;
                        if (!GameManager.inRoom) CarroDownAnim(true);
                        else photonView.RPC("CarroDownAnim", RpcTarget.All, true);
                        break;
                }

                if (state != State.Pipa && pipaState != null)
                {
                    if (!GameManager.inRoom) PipaAnim(false);
                    else photonView.RPC("PipaAnim", RpcTarget.All, false);
                }

                if (state != State.CarroDown && carroDownState != null)
                {
                    if (!GameManager.inRoom) CarroDownAnim(false);
                    else photonView.RPC("CarroDownAnim", RpcTarget.All, false);
                }

                if (state != State.CarroUp && carroUpState != null)
                {
                    if (!GameManager.inRoom) CarroUpAnim(false);
                    else photonView.RPC("CarroUpAnim", RpcTarget.All, false);
                }

                if (state != State.CarroWalk && carroWalkState != null)
                {
                    if (!GameManager.inRoom) CarroWalkAnim(false);
                    else photonView.RPC("CarroWalkAnim", RpcTarget.All, false);
                }
            }
        }
        else
        {
            if (pipaState != null && pipaState.weight !=0) if (GameManager.inRoom) { photonView.RPC("PipaAnim", RpcTarget.All, false); } else { PipaAnim(false); }
            if (carroDownState != null && carroDownState.weight != 0) if (GameManager.inRoom) { photonView.RPC("CarroDownAnim", RpcTarget.All, false); } else { CarroDownAnim(false); }
            if (carroUpState != null && carroUpState.weight != 0) if (GameManager.inRoom) { photonView.RPC("CarroUpAnim", RpcTarget.All, false); } else { CarroUpAnim(false); }
            if (carroWalkState != null && carroWalkState.weight != 0) if (GameManager.inRoom) { photonView.RPC("CarroWalkAnim", RpcTarget.All, false); } else { CarroWalkAnim(false); }
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
                        if (state == State.Chutando) return;
                        if (GameManager.inRoom == false) {  ChutandoAnim(true); }
                        else { photonView.RPC("ChutandoAnim", RpcTarget.All, true); }
                        break;

                    case AnimStateAction.Arremesando:
                        if (state == State.Arremessando) return;
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
                if(walkState !=null && walkState.weight !=0) WalkAnim(false);
                if (jumpState != null && jumpState.weight != 0) JumpAnim(false);
                if (fallState != null && fallState.weight != 0) FallingAnim(false);
                if (landState != null && landState.weight != 0) AterrisandoAnim(false);
            }
            else
            {
                if (walkState != null && walkState.weight != 0) photonView.RPC("WalkAnim", RpcTarget.All, false);
                if (jumpState != null && jumpState.weight != 0) photonView.RPC("JumpAnim", RpcTarget.All, false);
                if (fallState != null && fallState.weight != 0) photonView.RPC("FallingAnim", RpcTarget.All, false);
                if (landState != null && landState.weight != 0) photonView.RPC("AterrisandoAnim", RpcTarget.All, false);
            }
        }
        else
        {
            ActivateCarro(false);
            ActivatePipa(false);
            updateMove = true;

            if (lado.activeInHierarchy == false)
            {
                lado.SetActive(true);
                frente.SetActive(false);
            }

            if (stateMovement != AnimStateMovement.None)
            {
                switch (stateMovement)
                {
                    case AnimStateMovement.Walk:
                        if (state == State.Walking) return;
                        if (!GameManager.inRoom) WalkAnim(true);
                        else photonView.RPC("WalkAnim", RpcTarget.All, true);
                        break;
                    case AnimStateMovement.Rising:
                        if (state == State.Rising) return;
                        if (!GameManager.inRoom) JumpAnim(true);
                        else photonView.RPC("JumpAnim", RpcTarget.All, true);
                        break;
                    case AnimStateMovement.Falling:
                        if (state == State.Falling) return;
                        if (!GameManager.inRoom) FallingAnim(true);
                        else photonView.RPC("FallingAnim", RpcTarget.All, true);
                        break;
                    case AnimStateMovement.Aterrisando:
                        if (state == State.Aterrisando) return;
                        if (!GameManager.inRoom) AterrisandoAnim(true);
                        else photonView.RPC("AterrisandoAnim", RpcTarget.All, true);
                        break;

                }
            }

            if(state != State.Walking)
            {
                if (!GameManager.inRoom) WalkAnim(false);
                else photonView.RPC("WalkAnim", RpcTarget.All, false);
            }

            if (state != State.Rising)
            {
                if (!GameManager.inRoom) JumpAnim(false);
                else photonView.RPC("JumpAnim", RpcTarget.All, false);
            }

            if (state != State.Falling)
            {
                if (!GameManager.inRoom) FallingAnim(false);
                else photonView.RPC("FallingAnim", RpcTarget.All, false);
            }

            if (state != State.Aterrisando)
            {
                if (!GameManager.inRoom) AterrisandoAnim(false);
                else photonView.RPC("AterrisandoAnim", RpcTarget.All, false);
            }
        }

    }


    //Animações do StateFrente
    [PunRPC]
    public void IdleAnim(bool play)
    {
        if (play)
        {
            landParticles.particleLoop.Stop();
            playerFrente.animation.lastAnimationState.weight = 0;
            if (idleState == null)
            {
                idleState = playerFrente.animation.FadeIn("0_Idle", 0.1f, -1, 15, null, AnimationFadeOutMode.None);
                idleState.displayControl = true;
            }
            else
            {
                idleState.displayControl = true;
                idleState.weight = 1;
                idleState.Play();
            }

            state = State.Idle;
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
            playerFrente.animation.lastAnimationState.weight = 0;
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

            state = State.Ganhou;
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
            playerFrente.animation.lastAnimationState.weight = 0;
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

            state = State.Perdeu;
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
            playerFrente.animation.lastAnimationState.weight = 0;
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

            state = State.Stun;
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
            if (carroDownState != null || carroUpState != null || carroWalkState != null) carroDownState = null; carroUpState = null; carroWalkState = null;
            if (tiroState != null) tiroState = null;
            playerLado.animation.lastAnimationState.weight = 0;
            if (pipaState == null)
            {
                pipaState = playerLado.animation.FadeIn("7_Pipa", 0.1f, -1, 9, null, AnimationFadeOutMode.None);
                pipaState.displayControl = true;
                Debug.Log("PipaState == NULL");
            }
            else
            {
                ActivatePipa(true);

                pipaState.displayControl = true;
                pipaState.weight = 1;
                pipaState.Play();
            }
            state = State.Pipa;
        }
        else
        {
            if (pipaState == null) return;
            else
            {
                pipaState.displayControl = false;
                pipaState.weight = 0;
                ActivatePipa(false);
                //pipaState = null;
            }
        }
    }

    private void ActivatePipa(bool isOn)
    {
        if (pipaMesh[0].activeInHierarchy == isOn) return;
        for (int i = 0; i < pipaMesh.Length; i++)
        {
            pipaMesh[i].SetActive(isOn);
        }
        Debug.Log("ActivatePipa2 = " + isOn);
    }

    [PunRPC]
    public void CarroWalkAnim(bool play)
    {
        if (play)
        {
            landParticles.CarPaticles();
            if (pipaState != null) pipaState = null;
            if (tiroState != null) tiroState = null;

            playerLado.animation.lastAnimationState.weight = 0;

            if (carroWalkState == null)
            {
                carroWalkState = playerLado.animation.FadeIn("6_Rolima(Andando)", 0.1f, -1, 6, null, AnimationFadeOutMode.None);
                carroWalkState.displayControl = true;
            }
            else
            {
                ActivateCarro(true);

                carroWalkState.displayControl = true;
                carroWalkState.weight = 1;
                carroWalkState.Play();

                Debug.Log("CArroWalk = " + carroWalkState.name);
            }
            state = State.CarroWalk;
        }
        else
        {
            if (carroWalkState == null) return;
            else
            {
                landParticles.particleLoop.Stop();

                carroWalkState.displayControl = false;
                carroWalkState.weight = 0;
                carroWalkState.Stop();

                Debug.Log("CArroWalk == FALSE");
            }
        }
    }
    [PunRPC]
    public void CarroUpAnim(bool play)
    {
        if (play)
        {
            if (pipaState != null) pipaState = null;
            if (tiroState != null) tiroState = null;

            playerLado.animation.lastAnimationState.weight = 0;

            if (carroUpState == null)
            {
                carroUpState = playerLado.animation.FadeIn("6_Rolima(SubindoNoAr)", 0.1f, -1, 7, null, AnimationFadeOutMode.None);
                carroUpState.displayControl = true;
            }
            else
            {
                ActivateCarro(true);

                carroUpState.displayControl = true;
                carroUpState.weight = 1;
                carroUpState.Play();
                Debug.Log("CArroUp");
            }
            state = State.CarroUp;
        }
        else
        {
            if (carroUpState == null) return;
            else
            {
                carroUpState.displayControl = false;
                carroUpState.weight = 0;
                carroUpState.Stop();
                Debug.Log("CArroUp == FALSE");
            }
        }
    }
    [PunRPC]
    public void CarroDownAnim(bool play)
    {
        if (play)
        {
            if (pipaState != null) pipaState = null;
            if (tiroState != null) tiroState = null;

            playerLado.animation.lastAnimationState.weight = 0;

            if (carroDownState == null)
            {
                carroDownState = playerLado.animation.FadeIn("6_Rolima(DescendoNoAr)", 0.1f, -1, 8, null, AnimationFadeOutMode.None);
                carroDownState.displayControl = true;
            }
            else
            {
                ActivateCarro(true);

                carroDownState.displayControl = true;
                carroDownState.weight = 1;
                carroDownState.Play();

                Debug.Log("CArroDown");
            }
            state = State.CarroDown;
        }
        else
        {
            if (carroDownState == null) return;
            else
            {
                carroDownState.displayControl = false;
                carroDownState.weight = 0;
                carroDownState.Stop();
                Debug.Log("CArroDown == FALSE");
            }
        }
    }


    private void ActivateCarro(bool isOn)
    {
        if (carroMesh[0].activeInHierarchy == isOn) return;

        for (int i = 0; i < carroMesh.Length; i++)
        {
            carroMesh[i].SetActive(isOn);
        }

        Debug.Log("ActivateCarro2 = " + isOn);
    }

    // Animações do stateAction
    [PunRPC]
    public void ArremessandoAnim(bool play)
    {

        if (play)
        {
            if (carroDownState != null || carroUpState != null || carroWalkState != null) carroDownState = null; carroUpState = null; carroWalkState = null;
            if (pipaState != null) pipaState = null;

            playerLado.animation.lastAnimationState.weight = 0;

            if (tiroState == null)
            {
                tiroState = playerLado.animation.FadeIn("5_Arremessar", 0.1f, -1, 5, null, AnimationFadeOutMode.None);
                tiroState.resetToPose = true;
                tiroState.displayControl = true;
            }
            else
            {
                tiroState.displayControl = true;
                tiroState.weight = 1;
                tiroState.Play();
            }

            state = State.Arremessando;
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
            playerLado.animation.lastAnimationState.weight = 0;

            if (chuteState == null)
            {
                chuteState = playerLado.animation.FadeIn("3_Bicuda", 0.1f, -1, 4, null, AnimationFadeOutMode.None);
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
    [PunRPC]
    public void AterrisandoAnim(bool play)
    {
        if (play)
        {
            playerLado.animation.lastAnimationState.weight = 0;
            landParticles.PlayLand();

            if (landState == null)
            {
                landState = playerLado.animation.FadeIn("1_Aterrisando", 0.1f, -1, 3, null, AnimationFadeOutMode.Single);
                landState.displayControl = false;
                landState.resetToPose = true;
            }
            else
            {
                //landState.displayControl = true;
                landState.weight = 1;
                landState.Play();
            }

            state = State.Aterrisando;
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
            playerLado.animation.lastAnimationState.weight = 0;

            if (fallState == null)
            {
                fallState = playerLado.animation.FadeIn("1_NoAr(2_Descendo)", 0.1f, -1, 20, null, AnimationFadeOutMode.Single);
                fallState.displayControl = false;
            }
            else
            {
                //fallState.displayControl = true;
                fallState.weight = 1;
                fallState.Play();
            }
            state = State.Falling;
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
            playerLado.animation.lastAnimationState.weight = 0;
            landParticles.WalkParticles();
            if (walkState == null)
            {
                walkState = playerLado.animation.FadeIn("0_Corrida_V2", -1, -1, 21, null, AnimationFadeOutMode.Single);
                walkState.displayControl = false;
            }
            else
            {
                //walkState.displayControl = true;
                walkState.weight = 1f;
                walkState.Play();
            }
            state = State.Walking;
        }

        else
        {
            landParticles.particleLoop.Stop();
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
            playerLado.animation.lastAnimationState.weight = 0;
            landParticles.PlayLand();

            if (jumpState == null)
            {
                jumpState = playerLado.animation.FadeIn("1_NoAr(1_Subindo)", -1, -1, 22, null, AnimationFadeOutMode.Single);
                jumpState.displayControl = false;
            }
            else
            {
                //jumpState.displayControl = true;
                jumpState.weight = 1;
                jumpState.Play();
            }
            state = State.Rising;
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
