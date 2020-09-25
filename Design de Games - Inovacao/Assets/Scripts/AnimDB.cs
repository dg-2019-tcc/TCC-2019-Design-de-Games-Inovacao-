using System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Complete;

public class AnimDB : MonoBehaviour
{
    public bool debug = true;

    public GameObject frente;
    public GameObject lado;

    public UnityArmatureComponent playerLado;
    public UnityArmatureComponent playerFrente;
    //[HideInInspector]
    public UnityArmatureComponent playerAtivo;

    [SerializeField]
    private string nextAnim;

    public enum State { Idle, Walking, Rising, Falling, Aterrisando, Chutando, Arremessando, Inativo, Pipa, CarroWalk, CarroUp, CarroDown, Stun, Ganhou, Perdeu, Null }


    [SerializeField]
    public Armature arm01 = null;
    public Armature arm02 = null;

    private List<DragonBones.AnimationState> animations  = new List<DragonBones.AnimationState>();
    private DragonBones.AnimationState idleState = null;
    public DragonBones.AnimationState jumpState = null;
    private DragonBones.AnimationState fallState = null;
    private DragonBones.AnimationState landState = null;
    public DragonBones.AnimationState walkState = null;
    public DragonBones.AnimationState chuteState = null;
    public DragonBones.AnimationState tiroState = null;

    private const string WALK_ANIMATION_GROUP = "walk";
    private const string JUMP_ANIMATION_GROUP = "jump";
    private const string FALL_ANIMATION_GROUP = "fall";

    DragonBones.TimelineState timelineState;
    DragonBones.AnimationConfig animConfig;
    DragonBones.AnimationState walk = null;
    DragonBones.AnimationState idle = null;
    public DragonBones.AnimationState playState;

    float walkTime;
    float jumpTime;
    float fallTime;
    public const string stopJump = "stopJump";
    public const string startLand = "startLand";
  /*  public struct AnimStates
    {
        public AnimState01 animState01;
        public AnimState02 animState02;
        public AnimState03 animState03;
        public AnimState04 animState04;

        public AnimState01 oldState01;
        public AnimState02 oldState02;
        public AnimState03 oldState03;
        public AnimState04 oldState04;

        public AnimState04 walkState04;
        public AnimState04 jumpState04;
        public AnimState04 fallState04;

        public string animFuction;
        public bool changingArmature;
        public bool callLanding;
        public bool canFadeIn;


        public void Reset()
        {
        animState01 = AnimState01.None;
        animState02 = AnimState02.None;
        animState03 = AnimState03.None;
        animState04 = AnimState04.None;

        oldState01 = AnimState01.None;
        oldState02 = AnimState02.None;
        oldState03 = AnimState03.None;
        oldState04 = AnimState04.None;
        }
    }

    [HideInInspector]
    public PhotonView photonView;

    public static int armCache;
    public static int cache;
    public static bool sameCache;
    public static bool cleared;
    public static float currentTime;
    public static float cacheFrameRate;


    private void Start()
    {
        Debug.Log(playerLado.armature._cacheFrameIndex);
        Debug.Log(playerLado.armature.cacheFrameRate);
        photonView = gameObject.GetComponent<PhotonView>();
        playerLado.armature.cacheFrameRate = 24;
        //playerLado.AddEventListener(EventObject.LOOP_COMPLETE, _animationEventHandler);
        playerFrente.armature.cacheFrameRate = 12;

    }


    public void CallAnimState01(AnimState01 state01)
    {
        //if ((int)animState01 < (int)state01) { return; }
        animStates.animState01 = state01;

        PlayAnimState01();
    }

    public void CallAnimState02(AnimState02 state02)
    {
        if(state02 == animStates.animState02) { return; }
        if (animStates.animState01 != AnimState01.None) { return; }
        //if ((int)animState02 < (int)state02) { return; }
        animStates.animState02 = state02;
        animStates.oldState02 = state02;

        if(animStates.animState02 == AnimState02.None) { return; }

        PlayAnimState02();
    }

    public void CallAnimState03(AnimState03 state03)
    {
        if (state03 == animStates.animState03) { return; }
        if (animStates.animState01 != AnimState01.None || animStates.animState02 != AnimState02.None) { return; }
        //if ((int)animState03 < (int)state03) { return; }
        animStates.animState03 = state03;
        animStates.oldState03 = state03;

        if(animStates.animState03 == AnimState03.None) { return; }

        PlayAnimState03();
    }

    public void CallAnimState04(AnimState04 state04)
    {
        if (state04 == animStates.animState04) { return; }
        if (animStates.animState01 != AnimState01.None || animStates.animState02 != AnimState02.None || animStates.animState03 != AnimState03.None) { return; }

        animStates.animState04 = state04;
        animStates.oldState04 = state04;

        if (animStates.animState04 == AnimState04.None) { animStates.animState04 = AnimState04.Idle; }

        PlayAnimState04();
    }

    private void PlayAnimState01()
    {
        if (frente.activeInHierarchy == false) { ChangeArmature(0); }
        else
        {
            if (animStates.oldState02 == AnimState02.None && animStates.oldState03 == AnimState03.None && animStates.oldState04 == AnimState04.None)
            {
                animStates.canFadeIn = true;
            }
        }
        animStates.oldState01 = animStates.animState01;
        animStates.oldState02 = AnimState02.None;
        animStates.oldState03 = AnimState03.None;
        animStates.oldState04 = AnimState04.None;

        /*if (GameManager.inRoom)
        {
            switch (animStates.animState01)
            {
                case AnimState01.Ganhou:
                    animStates.animFuction = "GanhouAnim";
                    break;

                case AnimState01.Perdeu:
                    animStates.animFuction = "PerdeuAnim";
                    break;

                case AnimState01.Stun:
                    animStates.animFuction = "StunAnim";
                    break;
            }
        }*/
     /*   PlayAnim();
    }
    private void PlayAnimState02()
    {
        if (lado.activeInHierarchy == false) { ChangeArmature(1); }
        else
        {
            if (animStates.oldState01 == AnimState01.None && animStates.oldState03 == AnimState03.None && animStates.oldState04 == AnimState04.None)
            {
                animStates.canFadeIn = true;
            }
        }

        animStates.oldState01 = AnimState01.None;
        animStates.oldState02 = animStates.animState02;
        animStates.oldState03 = AnimState03.None;
        animStates.oldState04 = AnimState04.None;

        while (animStates.changingArmature) { return; }
        /*if (GameManager.inRoom)
        {
            switch (animStates.animState02)
            {
                case AnimState02.Pipa:
                    animStates.animFuction = "PipaAnim";
                    break;

                case AnimState02.CarroDown:
                    animStates.animFuction = "CarroDownAnim";
                    break;

                case AnimState02.CarroUp:
                    animStates.animFuction = "CarroUpAnim";
                    break;

                case AnimState02.CarroWalk:
                    animStates.animFuction = "CarroWalkAnim";
                    break;
            }
        }
        PlayAnim();
    }
    private void PlayAnimState03()
    {
        if (lado.activeInHierarchy == false) { ChangeArmature(1); }
        else
        {
            if (animStates.oldState01 == AnimState01.None && animStates.oldState02 == AnimState02.None && animStates.oldState04 == AnimState04.None)
            {
                animStates.canFadeIn = true;
            }
        }
        animStates.oldState01 = AnimState01.None;
        animStates.oldState02 = AnimState02.None;
        animStates.oldState03 = animStates.animState03;
        animStates.oldState04 = AnimState04.None;

        while (animStates.changingArmature) { return; }

        /*if (GameManager.inRoom)
        {
            switch (animStates.animState03)
            {
                case AnimState03.Arremesando:
                    animStates.animFuction = "ArremessandoAnim";
                    break;

                case AnimState03.Chute:
                    animStates.animFuction = "ChutandoAnim";
                    break;
            }
        }
        PlayAnim();
    }
    public void PlayAnimState04()
    {
        if (animStates.animState04 == AnimState04.Idle && frente.activeInHierarchy == false) { ChangeArmature(0); }
        else if (animStates.animState04 != AnimState04.Idle && lado.activeInHierarchy == false) { ChangeArmature(1); }
        else
        {

            if (animStates.oldState01 == AnimState01.None && animStates.oldState02 == AnimState02.None && animStates.oldState03 == AnimState03.None)
            {
                animStates.canFadeIn = true;
            }
        }
        animStates.oldState01 = AnimState01.None;
        animStates.oldState02 = AnimState02.None;
        animStates.oldState03 =AnimState03.None;


        while (animStates.changingArmature) { return; }
        /*if (GameManager.inRoom)
        {
            switch (animStates.animState04)
            {
                case AnimState04.Aterrisando:
                    animStates.animFuction = "AterrisandoAnim";
                    break;

                case AnimState04.Falling:
                    animStates.animFuction = "FallingAnim";
                    break;

                case AnimState04.Rising:
                    animStates.animFuction = "JumpAnim";
                    break;

                case AnimState04.Walk:
                    animStates.animFuction = "WalkAnim";
                    break;

                case AnimState04.Idle:
                    animStates.animFuction = "IdleAnim";
                    break;
            }
        }
        PlayAnim();
    }


    // int type é 0 para ativar a frente e 1 para a de lado
    public void ChangeArmature(int type)
    {
        animStates.canFadeIn = false;
        animStates.changingArmature = true;
        if (type == 0)
        {
            frente.SetActive(true);
                playerAtivo = playerFrente;
            lado.SetActive(false);
        }
        else
        {
            lado.SetActive(true);
                playerAtivo = playerLado;
            frente.SetActive(false);
        }
        animStates.changingArmature = false;
    }

    public void PlayAnim()
    {
        ChooseFunction();
        //if (GameManager.inRoom) { photonView.RPC(animStates.animFuction, RpcTarget.All); }
        //else { ChooseFunction(); } //{ Invoke(functionName, 0); }
    }
    
    public void StopLastState()
    {
        if (playerLado.animation.lastAnimationState != null && animStates.oldState04 != AnimState04.Idle)
        {
            /*if(playerLado.animation.lastAnimationState.name == "1_NoAr(2_Descendo)") { fallState = playerLado.animation.GotoAndStopByTime("1_NoAr(2_Descendo)", fallTime); }
            if (playerLado.animation.lastAnimationState.name == "0_Corrida_V2") { walkState = playerLado.animation.GotoAndStopByTime("0_Corrida_V2", walkTime); }
            if (playerLado.animation.lastAnimationState.name == "1_NoAr(1_Subindo)") { jumpState = playerLado.animation.GotoAndStopByTime("1_NoAr(1_Subindo)", jumpTime); }
            animations.Add(playerLado.animation.lastAnimationState);
            playerLado.animation.lastAnimationState.Stop();
            //playerLado.animation.lastAnimationState._fadeState = 0;
            //playerLado.animation.lastAnimationState._subFadeState = 0;
        }
    }
    
    public void ChooseFunction()
    {
        #region Anitga
        
        if (animStates.animState01 != AnimState01.None)
        {
            switch (animStates.animState01)
            {
                case AnimState01.Ganhou:
                    GanhouAnim();
                    return;

                case AnimState01.Perdeu:
                    PerdeuAnim();
                    return;

                case AnimState01.Stun:
                    StunAnim();
                    return;
            }
        }
        else
        {
            if(animStates.animState02 != AnimState02.None)
            {
                switch (animStates.animState02)
                {
                    case AnimState02.Pipa:
                        PipaAnim();
                        return;

                    case AnimState02.CarroDown:
                        CarroDownAnim();
                        return;

                    case AnimState02.CarroUp:
                        CarroUpAnim();
                        return;

                    case AnimState02.CarroWalk:
                        CarroWalkAnim();
                        return;
                }
            }
            else
            {
                if(animStates.animState03 != AnimState03.None)
                {
                    if (!GameManager.inRoom)
                    {
                        AterrisandoAnim(false);
                        FallingAnim(false);
                        JumpAnim(false);
                        WalkAnim(false);
                        IdleAnim(false);
                    }
                    else
                    {
                        photonView.RPC("AterrisandoAnim", RpcTarget.All,false);
                        photonView.RPC("FallingAnim", RpcTarget.All,false);
                        photonView.RPC("JumpAnim", RpcTarget.All,false);
                        photonView.RPC("WalkAnim", RpcTarget.All,false);
                        photonView.RPC("IdleAnim", RpcTarget.All,false);
                    }

                    switch (animStates.animState03)
                    {
                        case AnimState03.Arremesando:
                            if (!GameManager.inRoom)
                            {
                                ArremessandoAnim(true);
                                ChutandoAnim(false);
                            }
                            else
                            {
                                photonView.RPC("ArremessandoAnim", RpcTarget.All, true);
                                photonView.RPC("ChutandoAnim", RpcTarget.All, false);
                            }
                            return;

                        case AnimState03.Chute:
                            if (!GameManager.inRoom)
                            {
                                ChutandoAnim(true);
                                ArremessandoAnim(false);
                            }
                            else
                            {
                                photonView.RPC("ArremessandoAnim", RpcTarget.All, false);
                                photonView.RPC("ChutandoAnim", RpcTarget.All, true);
                            }
                            return;
                    }
                }

                else
                {
                    if(animStates.animState04 != AnimState04.None)
                    {
                        if (chuteState != null) photonView.RPC("ArremessandoAnim", RpcTarget.All, false);
                        if (tiroState != null) photonView.RPC("ChutandoAnim", RpcTarget.All, false);
                        switch (animStates.animState04)
                        {
                            case AnimState04.Aterrisando:
                                if (!GameManager.inRoom)
                                {
                                    AterrisandoAnim(true);
                                    if(fallState!=null) FallingAnim(false);
                                    if (jumpState != null) JumpAnim(false);
                                    if (walkState != null) WalkAnim(false);
                                    if (idleState != null) IdleAnim(false);
                                }
                                else
                                {
                                    photonView.RPC("AterrisandoAnim", RpcTarget.All, true);
                                    if (fallState != null) photonView.RPC("FallingAnim", RpcTarget.All, false);
                                    if (jumpState != null) photonView.RPC("JumpAnim", RpcTarget.All, false);
                                    if (walkState != null) photonView.RPC("WalkAnim", RpcTarget.All, false);
                                    if (idleState != null) photonView.RPC("IdleAnim", RpcTarget.All, false);
                                }
                                return;

                            case AnimState04.Falling:
                                if (!GameManager.inRoom)
                                {
                                    FallingAnim(true);
                                    if (jumpState != null) JumpAnim(false);
                                    if (walkState != null) WalkAnim(false);
                                    if (idleState != null) IdleAnim(false);
                                    if (landState != null) AterrisandoAnim(false);
                                }
                                else
                                {
                                    if (landState != null) photonView.RPC("AterrisandoAnim", RpcTarget.All, false);
                                    photonView.RPC("FallingAnim", RpcTarget.All, true);
                                    if (jumpState != null) photonView.RPC("JumpAnim", RpcTarget.All, false);
                                    photonView.RPC("WalkAnim", RpcTarget.All, false);
                                    photonView.RPC("IdleAnim", RpcTarget.All, false);
                                }
                                return;

                            case AnimState04.Rising:
                                if (!GameManager.inRoom)
                                {
                                    JumpAnim(true);
                                    WalkAnim(false);
                                    IdleAnim(false);
                                    FallingAnim(false);
                                    AterrisandoAnim(false);
                                }
                                else
                                {
                                    photonView.RPC("AterrisandoAnim", RpcTarget.All, false);
                                    photonView.RPC("FallingAnim", RpcTarget.All, false);
                                    photonView.RPC("JumpAnim", RpcTarget.All, true);
                                    photonView.RPC("WalkAnim", RpcTarget.All, false);
                                    photonView.RPC("IdleAnim", RpcTarget.All, false);
                                }
                                return;

                            case AnimState04.Walk:
                                if (!GameManager.inRoom)
                                {
                                    WalkAnim(true);
                                    IdleAnim(false);
                                    FallingAnim(false);
                                    JumpAnim(false);
                                    AterrisandoAnim(false);
                                }
                                else
                                {
                                    photonView.RPC("AterrisandoAnim", RpcTarget.All, false);
                                    photonView.RPC("FallingAnim", RpcTarget.All, false);
                                    photonView.RPC("JumpAnim", RpcTarget.All, false);
                                    photonView.RPC("WalkAnim", RpcTarget.All, true);
                                    photonView.RPC("IdleAnim", RpcTarget.All, false);
                                }
                                return;

                            case AnimState04.Idle:
                                if (!GameManager.inRoom)
                                {
                                    IdleAnim(true);
                                    FallingAnim(false);
                                    JumpAnim(false);
                                    WalkAnim(false);
                                    AterrisandoAnim(false);
                                }
                                else
                                {
                                    photonView.RPC("AterrisandoAnim", RpcTarget.All, false);
                                    photonView.RPC("FallingAnim", RpcTarget.All, false);
                                    photonView.RPC("JumpAnim", RpcTarget.All, false);
                                    photonView.RPC("WalkAnim", RpcTarget.All, false);
                                    photonView.RPC("IdleAnim", RpcTarget.All, true);
                                }
                                return;
                        }
                    }
                }
            }
        }
        #endregion

    }

    //Animações do State01
    [PunRPC] public void GanhouAnim() => playerAtivo.animation.FadeIn("2_Vencer", 0.1f);
    [PunRPC] public void PerdeuAnim()=> playerAtivo.animation.FadeIn("2_Perder", 0.1f); 
    [PunRPC] public void StunAnim() => playerAtivo.animation.FadeIn("3_Atordoado", 0.1f);

    //Animações do state02
    [PunRPC] public void PipaAnim() => playerAtivo.animation.Play("7_Pipa");

    [PunRPC]
    public void CarroWalkAnim()
    {
        if (animStates.canFadeIn)
        {
            playerAtivo.animation.FadeIn("6_Rolima(Andando)", 0.1f);
        }
        else
        {
            playerAtivo.animation.Play("6_Rolima(Andando)");
        }
    }
    [PunRPC]
    public void CarroUpAnim()
    {
        if (animStates.canFadeIn)
        {
            playerAtivo.animation.FadeIn("6_Rolima(SubindoNoAr)", 0.1f);
        }
        else
        {
            playerAtivo.animation.Play("6_Rolima(SubindoNoAr)");
        }
    }
    [PunRPC]
    public void CarroDownAnim()
    {
        if (animStates.canFadeIn)
        {
            playerAtivo.animation.FadeIn("6_Rolima(DescendoNoAr)", 0.1f);
        }
        else
        {
            playerAtivo.animation.Play("6_Rolima(DescendoNoAr)");
        }
    }

    // Animações do state03
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

    // Animações do state04


    [PunRPC] public void IdleAnim(bool play)
    {
        if (idleState == null)
        {
            idleState = playerFrente.animation.Play("0_Idle");
        }
        else
        {
            idleState.Play();
        }

        playState = idleState;
        animStates.oldState04 = animStates.animState04;
    }
    int landTimes = 0;
    public bool landed;
    [PunRPC] public void AterrisandoAnim(bool play)
    {
        if (play)
        {
            if (landState == null)
            {
                landState = playerLado.animation.FadeIn("1_Aterrisando", 0.1f, -1, 3, null, AnimationFadeOutMode.Single);
                landState.displayControl = true;
                landState.resetToPose = true;
                landed = true;
            }
            else
            {
                landState.displayControl = true;
                landState.weight = 1;
                landState.Play();
                landed = true;
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

        animStates.oldState04 = animStates.animState04;
    }


    [PunRPC]
    public void FallingAnim(bool play)
    {
        //landed = false;
        Debug.Log("Landed é" + landed);
        if (play)
        {
            if (fallState == null)
            {
                fallState = playerLado.animation.FadeIn("1_NoAr(2_Descendo)", 0.1f, -1, 2,null,AnimationFadeOutMode.Single);
                fallState.displayControl = true;
            }
            else
            {
                fallState.displayControl = true;
                fallState.weight = 1;
                fallState.Play();
            }
            animStates.oldState04 = animStates.animState04;
        }
        else
        {
            if (fallState == null) {return; }
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
                walkState = playerLado.animation.FadeIn("0_Corrida_V2", -1, -1, 0, null, AnimationFadeOutMode.Single);
                walkState.displayControl = true;
            }
            else
            {
                walkState.displayControl = true;
                walkState.weight = 1f;
                walkState.Play();
            }
            animStates.oldState04 = animStates.animState04;
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
                jumpState = playerLado.animation.FadeIn("1_NoAr(1_Subindo)",-1, -1, 1, null, AnimationFadeOutMode.Single);
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

        playState = jumpState;
        animStates.oldState04 = animStates.animState04;
    }

    private void _animationEventHandler(string type, EventObject eventObject)
    {
        switch (type)
        {
            case EventObject.LOOP_COMPLETE:
                if(eventObject.animationState.name == "1_Aterrisando")
                {
                    FallingAnim(false);
                }
                break;
        }
    }*/
}
