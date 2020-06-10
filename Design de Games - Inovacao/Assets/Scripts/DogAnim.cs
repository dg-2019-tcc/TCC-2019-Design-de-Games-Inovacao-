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

    public enum State { Idle, Walk, Up, Down }
    public State state = State.Idle;

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

        else if (moveAmount.y > 0 /*&& dogController.collisions.below == false*/)
        {
            PlayAnim("Up");
        }

        else if (moveAmount.y <= -3 && dogController.collisions.below == false)
        {
            PlayAnim("Down");
        }
        else
        {
            PlayAnim("Idle");
        }
    }

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
                    dogArmature.animation.Play("4_Idle");
                    state = State.Idle;
                }
                break;

            case "Walk":
                if (state != State.Walk)
                {
                    dogArmature.animation.Play("0_Run");
                    state = State.Walk;
                }
                break;

            case "Up":
                if (state != State.Up)
                {
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

        }
    }
}
