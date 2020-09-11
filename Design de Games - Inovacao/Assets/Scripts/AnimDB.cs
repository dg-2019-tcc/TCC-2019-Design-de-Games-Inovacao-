using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Complete;

public class AnimDB : MonoBehaviour
{
    public GameObject frente;
    public GameObject lado;

    public UnityArmatureComponent playerLado;
    public UnityArmatureComponent playerFrente;
    //[HideInInspector]
    public UnityArmatureComponent playerAtivo;

    [SerializeField]
    private string nextAnim;

    public enum State { Idle, Walking, Rising, Falling, Aterrisando, Chutando, Arremessando, Inativo, Pipa, CarroWalk, CarroUp, CarroDown, Stun, Ganhou, Perdeu, Null }

    public AnimStates animStates;

    public struct AnimStates
    {
        public AnimState01 animState01;
        public AnimState02 animState02;
        public AnimState03 animState03;
        public AnimState04 animState04;

        public AnimState02 oldState02;

        public string animFuction;
        public bool changingArmature;
        public bool callLanding;

        public void Reset()
        {
        animState01 = AnimState01.None;
        animState02 = AnimState02.None;
        animState03 = AnimState03.None;
        animState04 = AnimState04.None;

        oldState02 = AnimState02.None;
        }
    }

    [HideInInspector]
    public PhotonView photonView;


    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
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

        if(animStates.animState02 == AnimState02.None) { return; }

        PlayAnimState02();
    }

    public void CallAnimState03(AnimState03 state03)
    {
        if (state03 == animStates.animState03) { return; }
        if (animStates.animState01 != AnimState01.None || animStates.animState02 != AnimState02.None) { return; }
        //if ((int)animState03 < (int)state03) { return; }
        animStates.animState03 = state03;

        if(animStates.animState03 == AnimState03.None) { return; }

        PlayAnimState03();
    }

    public void CallAnimState04(AnimState04 state04)
    {
        if (state04 == animStates.animState04) { return; }
        if (animStates.animState01 != AnimState01.None || animStates.animState02 != AnimState02.None || animStates.animState03 != AnimState03.None) { return; }

        animStates.animState04 = state04;

        if (animStates.animState04 == AnimState04.None) { animStates.animState04 = AnimState04.Idle; }

        PlayAnimState04();
    }

    private void PlayAnimState01()
    {
        if (frente.activeInHierarchy == false) { ChangeArmature(0); }
        if (GameManager.inRoom)
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
        }
        PlayAnim();
    }
    private void PlayAnimState02()
    {
        if (lado.activeInHierarchy == false) { ChangeArmature(1); }

        animStates.oldState02 = animStates.animState02;

        while (animStates.changingArmature) { return; }
        if (GameManager.inRoom)
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
        while (animStates.changingArmature) { return; }
        if (GameManager.inRoom)
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
        Debug.Log("[AnimDB] PlayAnimState03()");
    }
    private void PlayAnimState04()
    {
        if (animStates.animState04 == AnimState04.Idle && frente.activeInHierarchy == false) { ChangeArmature(0); }
        else if (animStates.animState04 != AnimState04.Idle && frente.activeInHierarchy == true) { ChangeArmature(1); }

        while (animStates.changingArmature) { return; }
        if (GameManager.inRoom)
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
        animStates.changingArmature = true;
        if (type == 0)
        {
            frente.SetActive(true);
            lado.SetActive(false);
            playerAtivo = playerFrente;
        }
        else
        {
            frente.SetActive(false);
            lado.SetActive(true);
            playerAtivo = playerLado;
        }
        animStates.changingArmature = false;
    }

    public void PlayAnim()
    {
        if (GameManager.inRoom) { photonView.RPC(animStates.animFuction, RpcTarget.All); }
        else { ChooseFunction(); } //{ Invoke(functionName, 0); }
    }

    private void ChooseFunction()
    {
        if(animStates.animState01 != AnimState01.None)
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
                    switch (animStates.animState03)
                    {
                        case AnimState03.Arremesando:
                            ArremessandoAnim();
                            return;

                        case AnimState03.Chute:
                            ChutandoAnim();
                            return;
                    }
                }

                else
                {
                    if(animStates.animState04 != AnimState04.None)
                    {
                        switch (animStates.animState04)
                        {
                            case AnimState04.Aterrisando:
                                AterrisandoAnim();
                                return;

                            case AnimState04.Falling:
                                FallingAnim();
                                return;

                            case AnimState04.Rising:
                                JumpAnim();
                                return;

                            case AnimState04.Walk:
                                WalkAnim();
                                return;

                            case AnimState04.Idle:
                                IdleAnim();
                                return;
                        }
                    }
                }
            }
        }

    }

    //Animações do State01
    [PunRPC] public void GanhouAnim() => playerAtivo.animation.GotoAndPlayByFrame("2_Vencer");
    [PunRPC] public void PerdeuAnim()=> playerAtivo.animation.GotoAndPlayByFrame("2_Perder", 0); 
    [PunRPC] public void StunAnim() => playerAtivo.animation.GotoAndPlayByFrame("3_Atordoado", 0);

    //Animações do state02
    [PunRPC] public void PipaAnim() => playerAtivo.animation.GotoAndPlayByFrame("7_Pipa", 0);
    [PunRPC] public void CarroWalkAnim()=> playerAtivo.animation.GotoAndPlayByFrame("6_Rolima(Andando)", 0); 
    [PunRPC] public void CarroUpAnim() => playerAtivo.animation.GotoAndPlayByFrame("6_Rolima(SubindoNoAr)", 0); 
    [PunRPC] public void CarroDownAnim() => playerAtivo.animation.GotoAndPlayByFrame("6_Rolima(DescendoNoAr)", 0);

    // Animações do state03
    [PunRPC] public void ArremessandoAnim() => playerAtivo.animation.GotoAndPlayByFrame("5_Arremessar", 0, 1); 
    [PunRPC] public void ChutandoAnim() => playerAtivo.animation.GotoAndPlayByFrame("3_Bicuda", 0, 1); 

    // Animações do state04
    [PunRPC] public void IdleAnim() => playerAtivo.animation.GotoAndPlayByFrame("0_Idle", 0);
    [PunRPC] public void AterrisandoAnim() => playerAtivo.animation.FadeIn("1_Aterrisando", 0.15f, 1);
    [PunRPC] public void FallingAnim() => playerAtivo.animation.GotoAndPlayByFrame("1_NoAr(2_Descendo)", 0);
    [PunRPC] public void WalkAnim() => playerAtivo.animation.GotoAndPlayByFrame("0_Corrida_V2", 0); 
    [PunRPC] public void JumpAnim() => playerAtivo.animation.GotoAndPlayByFrame("1_NoAr(1_Subindo)", 0);
}
