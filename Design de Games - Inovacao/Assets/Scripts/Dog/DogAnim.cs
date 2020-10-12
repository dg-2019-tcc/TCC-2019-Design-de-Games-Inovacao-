using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Complete;

public class DogAnim : MonoBehaviour
{
    PhotonView pv;
    private bool isOnline;
    AIController2D controller;
    public DogController dogController;
    public ButtonA buttonA;
    public UnityArmatureComponent dogArmature;

    public enum State { Idle, Walk, Up, Down, Ativa, Desativa}
    public State state = State.Idle;

    public DragonBones.AnimationState idleState;
    public DragonBones.AnimationState walkState;
    public DragonBones.AnimationState upState;
    public DragonBones.AnimationState downState;
    public DragonBones.AnimationState ativaState;
    public DragonBones.AnimationState desativaState;

    public bool ativaDog;
    public bool dogState;

    private bool desativaPuff;
    private bool dog01Ativo;
    private bool jaAtivou;

    public GameObject puff;
    public GameObject[] dog01;
    public GameObject[] dog02;


    void Start()
    {
        pv = GetComponent<PhotonView>();
        controller = GetComponent<AIController2D>();
        dogArmature.armature.cacheFrameRate = 24;
        if (PhotonNetwork.InRoom)
        {
            isOnline = true;
        }
        else
        {
            isOnline = false;
        }
    }

    public void ChangeDogAnim(Vector3 moveAmount, Vector2 input)
    {
        //if (!jaAtivou) return;
        if (input.x != 0)
        {
            if (GameManager.inRoom)pv.RPC("WalkDog", RpcTarget.All, true);
            else WalkDog(true);
        }

        else if (moveAmount.y > 3)
        {
            if (GameManager.inRoom)pv.RPC("UpDog", RpcTarget.All, true);
            else UpDog(true);
        }
        else if (moveAmount.y < -1)
        {
            if (GameManager.inRoom)pv.RPC("DownDog", RpcTarget.All, true);
            else DownDog(true);
        }

        else
        {
            if (GameManager.inRoom) pv.RPC("IdleDog", RpcTarget.All, true);
            else IdleDog(true);
        }

        if(state != State.Idle)
        {
            if (GameManager.inRoom) pv.RPC("IdleDog", RpcTarget.All, false);
            else IdleDog(false);
        }

        if(state != State.Walk)
        {
            if (GameManager.inRoom) pv.RPC("WalkDog", RpcTarget.All, false);
            else WalkDog(false);
        }

        if(state != State.Up)
        {
            if (GameManager.inRoom) pv.RPC("UpDog", RpcTarget.All, false);
            else UpDog(false);
        }

        if(state != State.Down)
        {
            if (GameManager.inRoom) pv.RPC("DownDog", RpcTarget.All, false);
            else DownDog(false);
        }
    }


    [PunRPC]
    void WalkDog(bool play)
    {
        if (play)
        {
            if (state == State.Walk) return;

            dogArmature.animation.lastAnimationState.weight = 0;

            dog01Ativo = false;
            desativaPuff = true;

            if (walkState == null)
            {
                walkState = dogArmature.animation.FadeIn("0_Run", 0.1f, -1, 0, null, AnimationFadeOutMode.None);
                walkState.displayControl = true;
            }
            else
            {
                walkState.displayControl = true;
                walkState.weight = 1;
                walkState.Play();
            }
            state = State.Walk;
            
        }
        else
        {
            if (walkState == null) return;
            else
            {
               
                walkState.weight = 0;
                walkState.displayControl = false;
                walkState.Stop();
            }
        }
    }

    [PunRPC]
    void UpDog(bool play)
    {
        if (play)
        {
            if (state == State.Up) return;

            dogArmature.animation.lastAnimationState.weight = 0;

            dog01Ativo = false;
            desativaPuff = true;


            if (upState == null)
            {
                upState = dogArmature.animation.FadeIn("1_Subindo(NoAr)", 0.1f, -1, 1, null, AnimationFadeOutMode.Single);
                upState.displayControl = true;
            }
            else
            {
                //if (dog02[0].activeInHierarchy != true) { AtivandoDog02(true); }

                upState.displayControl = true;
                upState.weight = 1;
                upState.Play();
            }
        }
        else
        {
            if (upState == null) return;
            else
            {
                upState.weight = 0;
                upState.displayControl = false;
                upState.Stop();
            }
        }
    }

    [PunRPC]
    void DownDog(bool play)
    {
        if (play)
        {
            if (state == State.Down) return;
            dogArmature.animation.lastAnimationState.weight = 0;

            dog01Ativo = false;
                desativaPuff = true;

                if (downState == null)
                {
                    downState = dogArmature.animation.FadeIn("2_Descendo(NoAr)", 0.1f, -1, 2, null, AnimationFadeOutMode.None);
                    downState.displayControl = true;
                }
                else
                {
                    downState.displayControl = true;
                    downState.weight = 1;
                    downState.Play();
                }
                state = State.Down;
        }
        else
        {
            if (downState == null) return;
            else
            {
                downState.displayControl = false;
                downState.weight = 0;
                downState.Stop();
            }
        }
    }

    //Frente DOG
    [PunRPC]
    void IdleDog(bool play)
    {
        if (play)
        {
            if (state != State.Idle)
            {
                dogArmature.animation.lastAnimationState.weight = 0;

                dog01Ativo = true;
                desativaPuff = true;
                dogArmature.animation.lastAnimationState.Stop();
                if (idleState == null)
                {
                    //idleState = dogArmature.animation.FadeIn("4_Idle", 0.1f, -1, 3, null, AnimationFadeOutMode.None);
                    //idleState.displayControl = true;
                }
                else
                {
                    //idleState.displayControl = true;
                    //idleState.weight = 1;
                    //idleState.Play();
                }
                state = State.Idle;
            }
        }
        else
        {
            if (idleState == null) { return; }
            else
            {
                idleState.weight = 0;
                idleState.displayControl = false;
                idleState.Stop();
            }
        }
    }


    private void AtivandoDog01(bool isOn)
    {
        if (dog01[0].activeInHierarchy == isOn) return;

        for(int i = 0; i < dog01.Length; i++)
        {
            dog01[i].SetActive(isOn);
        }
    }

    private void AtivandoDog02(bool isOn)
    {
        if (dog02[0].activeInHierarchy == isOn) return;

        for (int i = 0; i < dog02.Length; i++)
        {
            dog02[i].SetActive(isOn);
        }
    }

}
