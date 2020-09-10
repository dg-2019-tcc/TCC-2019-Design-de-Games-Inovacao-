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
    [HideInInspector]
    public UnityArmatureComponent playerAtivo;

    /*public string idlePose = "0_Idle";
    public string walkAnimation = "0_Corrida_V2";
    public string subindoJumpAnimation = "1_NoAr(1_Subindo)";
    public string descendoJumpAnimation = "1_NoAr(3_Descendo)";
    public string aterrisandoAnimation = "1_Aterrisando";
    public string chuteAnimation = "3_Bicuda(SemPreparacao)";
    public string arremessoAnimation = "6_Arremessar(3_Arremesso)";
    public string inativoAnimation = "1_Inatividade(2_IdlePose)";
    public string pipaAnimation = "7_Pipa";
    public string carroWalkAnim = "6_Rolima(Andando)";
    public string carroUpAnim = "6_Rolima(SubindoNoAr)";
    public string carroDownAnim = "6_Rolima(DescendoNoAr)";
    public string stunAnim = "3_Atordoado";
    public string vitoriaAnim = "2_Vencer";
    public string derrotaAnim = "2_Perder";*/

    [SerializeField]
    private string nextAnim;

    private string animFuction;

    public enum State { Idle, Walking, Rising, Falling, Aterrisando, Chutando, Arremessando, Inativo, Pipa, CarroWalk, CarroUp, CarroDown, Stun, Ganhou, Perdeu, Null }
    public AnimState01 animState01 = AnimState01.None;
    public AnimState02 animState02 = AnimState02.None;
    public AnimState03 animState03 = AnimState03.None;
    public AnimState04 animState04 = AnimState04.None;

    [HideInInspector]
    public PhotonView photonView;

    private bool changingArmature;

    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
    }


    public void CallAnimState01(AnimState01 state01)
    {
        if ((int)animState01 < (int)state01) { return; }
        animState01 = state01;
        PlayAnimState01();
    }

    public void CallAnimState02(AnimState02 state02)
    {
        if(animState01 != AnimState01.None) { return; }
        if ((int)animState02 < (int)state02) { return; }
        animState02 = state02;
        PlayAnimState02();
    }

    public void CallAnimState03(AnimState03 state03)
    {
        if (animState01 != AnimState01.None || animState02 != AnimState02.None) { return; }
        if ((int)animState03 < (int)state03) { return; }
        animState03 = state03;
        PlayAnimState03();
    }

    public void CallAnimState04(AnimState04 state04)
    {
        if(state04 == animState04) { return; }
        if (animState01 != AnimState01.None || animState02 != AnimState02.None || animState03 != AnimState03.None) { return; }
        //if ((int)animState04 < (int)state04 && state04!= AnimState04.None) { return; }
        animState04 = state04;
        PlayAnimState04();
//        Debug.Log("[AnimDB] CallAnimState04()");
    }

    private void PlayAnimState01()
    {
        if(frente.activeInHierarchy == false) { ChangeArmature(0); }
        switch (animState01)
        {
            /*case AnimState01.Ganhou:
                PlayAnim(vitoriaAnim, true);
                break;

            case AnimState01.Perdeu:
                PlayAnim(derrotaAnim, true);
                break;

            case AnimState01.Stun:
                PlayAnim(stunAnim, true);
                break;*/
        }
//        Debug.Log("[AnimDB] PlayAnimState01()");

    }

    private void PlayAnimState02()
    {
        if(lado.activeInHierarchy == false) { ChangeArmature(1); }
        switch (animState02)
        {
            /*case AnimState02.Pipa:
                PlayAnim(pipaAnimation, true);
                break;

            case AnimState02.CarroDown:
                PlayAnim(carroDownAnim, true);
                break;

            case AnimState02.CarroUp:
                PlayAnim(carroUpAnim, true);
                break;

            case AnimState02.CarroWalk:
                PlayAnim(carroWalkAnim, true);
                break;*/
        }
    }
    private void PlayAnimState03()
    {
        if (lado.activeInHierarchy == false) { ChangeArmature(1); }
        switch (animState03)
        {
            /*case AnimState03.Arremesando:
                PlayAnim(arremessoAnimation, false);
                break;

            case AnimState03.Chute:
                PlayAnim(chuteAnimation, false);
                break;*/

        }
    }
    private void PlayAnimState04()
    {
        if (animState04 == AnimState04.Idle  && frente.activeInHierarchy == false) { ChangeArmature(0); }
        else if (animState04 != AnimState04.Idle && frente.activeInHierarchy == true) { ChangeArmature(1); }
        while (changingArmature) { return; }
        switch (animState04)
        {
            case AnimState04.Aterrisando:
                //PlayAnim(aterrisandoAnimation, false);
                //AterrisandoAnim();
                animFuction = "AterrisandoAnim";
                break;

            case AnimState04.Falling:
                //PlayAnim(descendoJumpAnimation, true);
                //FallingAnim();
                animFuction = "FallingAnim";
                break;

            case AnimState04.Rising:
                //PlayAnim(subindoJumpAnimation, true);
                //umpAnim();
                animFuction = "JumpAnim";
                break;

            case AnimState04.Walk:
                //PlayAnim(walkAnimation, true);
                //WalkAnim();
                animFuction = "WalkAnim";
                break;

            case AnimState04.Idle:
                //PlayAnim(idlePose, true);
                //IdleAnim();
                animFuction = "IdleAnim";
                break;
        }
        PlayAnim(animFuction);
     //   Debug.Log("[AnimDB] PlayAnimState04()");
    }

    // int type é 0 para ativar a frente e 1 para a de lado
    public void ChangeArmature(int type)
    {
        playerAtivo.animation.AdvanceTime(0);
        changingArmature = true;
        if(type == 0)
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
        changingArmature = false;
       // Debug.Log("[AnimDB] ChangeArmature()");
    }

    public void PlayAnim(string functionName)
    {

        if (GameManager.inRoom)
        {
            photonView.RPC(functionName, RpcTarget.All);
        }
        else
        {
            //CallAnim(anim,isLoop);
            Invoke(functionName,0);
        }

       // Debug.Log("[AnimDB] PlayAnim()");
    }
    //Animações do State01
    [PunRPC] public void GanhouAnim() => playerAtivo.animation.FadeIn("2_Vencer", 0.2f);
    [PunRPC] public void PerdeuAnim() => playerAtivo.animation.FadeIn("2_Perder", 0.2f);
    [PunRPC] public void StunAnim() => playerAtivo.animation.FadeIn("3_Atordoado", 0.2f);

    //Animações do state02
    [PunRPC] public void PipaAnim() => playerAtivo.animation.FadeIn("7_Pipa", 0.2f);
    [PunRPC] public void CarroWalkAnim() => playerAtivo.animation.FadeIn("6_Rolima(Andando)", 0.2f);
    [PunRPC] public void CarroUpAnim() => playerAtivo.animation.FadeIn("6_Rolima(SubindoNoAr)", 0.2f);
    [PunRPC] public void CarroDownAnim() => playerAtivo.animation.FadeIn("6_Rolima(DescendoNoAr)", 0.2f);

    // Animações do state03
    [PunRPC] public void ArremessandoAnim() => playerAtivo.animation.FadeIn("6_Arremessar(3_Arremesso)", 0.2f);
    [PunRPC] public void ChutandoAnim() => playerAtivo.animation.FadeIn("3_Bicuda(SemPreparacao)", 0.2f);


    // Animações do state04
    [PunRPC] public void FallingAnim() => playerAtivo.animation.FadeIn("1_NoAr(2_Descendo)", 0.2f);
    [PunRPC] public void AterrisandoAnim() => playerAtivo.animation.FadeIn("1_Aterrisando", 0.2f);
    [PunRPC] public void WalkAnim() => playerAtivo.animation.FadeIn("0_Corrida_V2", 0.2f);
    [PunRPC] public void JumpAnim() => playerAtivo.animation.FadeIn("1_NoAr(1_Subindo)", 0.2f);
    [PunRPC] public void IdleAnim() => playerAtivo.animation.FadeIn("0_Idle", 0.2f);


    [PunRPC]
    public void CallAnim(string anim, bool isLoop)
    {
        if (!isLoop) { playerAtivo.animation.Play(anim, 1); }
        else{ playerAtivo.animation.Play(anim);}
     //   Debug.Log("[AnimDB] CallAnim()");
    }
}
