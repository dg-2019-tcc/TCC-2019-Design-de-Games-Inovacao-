using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class DogAnim : MonoBehaviour
{
    PhotonView pv;
    private bool isOnline;
    AIController2D dogController;
    public UnityArmatureComponent dogArmature;

    public enum State { Idle, Walk, Up, Down, Ativa, Desativa}
    public State state = State.Idle;

    public bool ativaDog;
    public bool dogState;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        dogController = GetComponent<AIController2D>();

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

                if (input.x != 0)
                {
                    PlayAnim("Walk");
                }

                else if(moveAmount.y > 0)
                {
                    PlayAnim("Up");
                }

                else
                {
                    PlayAnim("Idle");
                }
           
    }

    /*public void PetChange(bool ativa)
    {
        ativaDog = true;

        if(ativa == true)
        {
            PlayAnim("Ativa");
        }

        else
        {
            PlayAnim("Desativa");
        }
    }*/


    private void PlayAnim(string anim)
    {
        if (isOnline)
        {
            pv.RPC("AnimState", RpcTarget.All, anim);
        }
        else
        {
            DogAnimState(anim);
        }

    }

    [PunRPC]
    public void DogAnimState(string anim)
    {
        switch (anim)
        {
            case "Idle":
                if (state != State.Idle)
                {
                    dogArmature.animation.timeScale = 1;
                    dogArmature.animation.Play("4_Idle");
                    state = State.Idle;
                }
                break;

            case "Walk":
                if (state != State.Walk)
                {
                    dogArmature.animation.timeScale = 1;
                    dogArmature.animation.Play("0_Run");
                    state = State.Walk;
                }
                break;

            case "Up":
                if (state != State.Up)
                {
                    dogArmature.animation.timeScale = 1;
                    dogArmature.animation.Play("1_Subindo(NoAr)");
                    state = State.Up;
                }
                break;

            case "Down":
                if (state != State.Down)
                {
                    dogArmature.animation.Play("2_Descendo(NoAr)");
                    state = State.Down;
                }
                break;

            case "Ativa":
                if (state != State.Ativa)
                {
                    dogArmature.animation.Play("4_Transform(Voltando)");
                    state = State.Ativa;
                }
                break;


            case "Desativa":
                if (state != State.Desativa)
                {
                    dogArmature.animation.Play("4_Transform(PraTudo)");
                    state = State.Desativa;
                }
                break;



        }
    }
}
