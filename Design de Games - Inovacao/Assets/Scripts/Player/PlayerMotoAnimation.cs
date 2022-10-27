using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Complete;

public class PlayerMotoAnimation : MonoBehaviour
{
    public GameObject frente;
    public GameObject lado;

    public string motoMotoWalkAnim = "8_Moto(Andando)";
    public string motoDownAnim = "8_Moto(DescendoNoAr)";
    public string motoUpAnim = "8_Moto(SubindoNoAr)";
    public string motoGrauAnim = "8_Moto(Empinando)";
    public string motoCrashAnim = "8_Moto(Batendo)";
    public string motoLandAnim = "8_Moto(Aterrisando)";

    private DragonBones.AnimationState trickState = null;
    private DragonBones.AnimationState crashState = null;
    private DragonBones.AnimationState motoLandState = null;
    private DragonBones.AnimationState motoJumpState = null;
    private DragonBones.AnimationState motoFallState = null;
    private DragonBones.AnimationState motoWalkState = null;

    public enum State{ MotoWalk, MotoUp, MotoDown, MotoGrau, MotoCrash, MotoLand}

    public State state = State.MotoWalk;

    private Controller2D controller;
    [SerializeField]
    private UnityArmatureComponent playerLado;
    [SerializeField]
    private UnityArmatureComponent playerFrente;

    [HideInInspector]
    public PhotonView photonView;

    public TriggerCollisionsController triggerCollisions;

    public EmpinaMoto empina;
    public BoolVariable levouDogada;

    private bool isOnline;

    #region Unity Function

    void Start()
    {
        frente.SetActive(false);
        photonView = gameObject.GetComponent<PhotonView>();
        controller = GetComponent<Controller2D>();
        triggerCollisions = GetComponent<TriggerCollisionsController>();

        if (PhotonNetwork.InRoom)
        {
            isOnline = true;
        }
        else
        {
            isOnline = false;
        }
    }

    #endregion

    #region Public Functions

    public void ChangeMotoAnim(Vector3 moveAmount, Vector2 oldPos, bool stun)
    {
        if (stun == false)
        {
            if (empina.isEmpinando || empina.isManobrandoNoAr)
            {
                if (empina.isManobrandoNoAr && controller.collisions.below)
                {
                    if (!GameManager.inRoom) CrashAnim(true);
                    else photonView.RPC("CrashAnim", RpcTarget.All, true);
                    return;
                }
                if (state == State.MotoGrau) return;
                if (!GameManager.inRoom) ManobraAnim(true);
                else photonView.RPC("ManobraAnim", RpcTarget.All, true);
            }


            else if (moveAmount.y > 0 && controller.collisions.below == false)
            {
                if (state == State.MotoUp) return;
                if (!GameManager.inRoom) MotoUpAnim(true);
                else photonView.RPC("MotoUpAnim", RpcTarget.All, true);
            }

            else if (moveAmount.y <= -1 && controller.collisions.below == false)
            {
                if (state == State.MotoDown) return;
                if (!GameManager.inRoom) MotoFallAnim(true);
                else photonView.RPC("MotoFallAnim", RpcTarget.All, true);
            }

            else if (moveAmount.y == 0)
            {
                if (state == State.MotoWalk) return;
                if (!GameManager.inRoom) MotoWalkAnim(true);
                else photonView.RPC("MotoWalkAnim", RpcTarget.All, true);
            }
        }

        else
        {
            if (state == State.MotoCrash) return;
            if (!GameManager.inRoom) CrashAnim(true);
            else photonView.RPC("CrashAnim", RpcTarget.All, true);
        }

        if (!GameManager.inRoom) AnimStateUpdate();
        else photonView.RPC("AnimStateUpdate", RpcTarget.All);
    }

    #endregion

    #region Private Functions

    [PunRPC]
    private void CrashAnim(bool play)
    {
        if (play)
        {
            if (crashState == null)
            {
                crashState = playerLado.animation.FadeIn("8_Moto(Batendo)", 0.1f, -1, 10, null, AnimationFadeOutMode.Single);
            }
            else
            {
                crashState.weight = 1;
                crashState.Play();
            }
            Debug.Log("Crash");
            state = State.MotoCrash;
        }
        else
        {
            if (crashState == null) return;
            else
            {
                crashState.weight = 0;
                crashState.Stop();
            }
        }
    }

    [PunRPC]
    private void ManobraAnim(bool play)
    {
        if (play)
        {
            if (trickState == null)
            {
                trickState = playerLado.animation.FadeIn("8_Moto(Empinando)", 0.1f, -1, 4, null, AnimationFadeOutMode.Single);
                trickState.resetToPose = true;
            }
            else
            {
                trickState.weight = 1;
                trickState.Play();
            }
            state = State.MotoGrau;
        }
        else
        {
            if (trickState == null) return;
            else
            {
                trickState.Stop();
                trickState.weight = 0;
            }
        }
    }

    [PunRPC]
    private void MotoLandAnim(bool play)
    {
        if (play)
        {
            if (motoLandState == null)
            {
                motoLandState = playerLado.animation.FadeIn("8_Moto(Aterrisando)", 0.1f, -1, 3, null, AnimationFadeOutMode.Single);
                motoLandState.resetToPose = true;
            }
            else
            {
                motoLandState.weight = 1;
                motoLandState.Play();
            }

            state = State.MotoLand;
        }
        else
        {
            if (motoLandState == null) return;
            else
            {
                motoLandState.Stop();
                motoLandState.weight = 0;
            }
        }

    }

    [PunRPC]
    private void MotoFallAnim(bool play)
    {
        if (play)
        {
            if (motoFallState == null)
            {
                motoFallState = playerLado.animation.FadeIn("8_Moto(DescendoNoAr)", 0.1f, -1, 20, null, AnimationFadeOutMode.Single);
            }
            else
            {
                motoFallState.weight = 1;
                motoFallState.Play();
            }
            state = State.MotoDown;
        }
        else
        {
            if (motoFallState == null) { return; }
            else
            {
                motoFallState.Stop();
                motoFallState.weight = 0;
            }
        }

    }

    [PunRPC]
    private void MotoWalkAnim(bool play)
    {
        if (play)
        {
            if (motoWalkState == null)
            {
                motoWalkState = playerLado.animation.FadeIn("8_Moto(Andando)", -1, -1, 21, null, AnimationFadeOutMode.Single);
            }
            else
            {
                motoWalkState.weight = 1f;
                motoWalkState.Play();
            }
            state = State.MotoWalk;
        }

        else
        {
            if (motoWalkState == null) return;
            else
            {
                motoWalkState.Stop();
                motoWalkState.weight = 0;
            }
        }



    }

    [PunRPC]
    private void MotoUpAnim(bool play)
    {
        if (play)
        {
            if (motoJumpState == null)
            {
                motoJumpState = playerLado.animation.FadeIn("8_Moto(SubindoNoAr)", -1, -1, 22, null, AnimationFadeOutMode.Single);
            }
            else
            {
                motoJumpState.weight = 1;
                motoJumpState.Play();
            }
            state = State.MotoUp;
        }
        else
        {
            if (motoJumpState == null) return;
            else
            {
                motoJumpState.Stop();
                motoJumpState.weight = 0;
            }
        }
    }

    [PunRPC]
    private void AnimStateUpdate()
    {
        if (state != State.MotoWalk && motoWalkState != null)
        {
            if (!GameManager.inRoom) MotoWalkAnim(false);
            else photonView.RPC("MotoWalkAnim", RpcTarget.All, false);
        }

        if (state != State.MotoUp && motoJumpState != null)
        {
            if (!GameManager.inRoom) MotoUpAnim(false);
            else photonView.RPC("MotoUpAnim", RpcTarget.All, false);
        }

        if (state != State.MotoDown && motoFallState != null)
        {
            if (!GameManager.inRoom) MotoFallAnim(false);
            else photonView.RPC("MotoFallAnim", RpcTarget.All, false);
        }

        if (state != State.MotoLand && motoLandState != null)
        {
            if (!GameManager.inRoom) MotoLandAnim(false);
            else photonView.RPC("MotoLandAnim", RpcTarget.All, false);
        }

        if (state != State.MotoGrau && trickState != null)
        {
            if (!GameManager.inRoom) ManobraAnim(false);
            else photonView.RPC("ManobraAnim", RpcTarget.All, false);
        }

        if (state != State.MotoCrash && crashState != null)
        {
            if (!GameManager.inRoom) CrashAnim(false);
            else photonView.RPC("CrashAnim", RpcTarget.All, false);
        }
    }

    #endregion

}
