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

    public string motoWalkAnim = "8_Moto(Andando)";
    public string motoDownAnim = "8_Moto(DescendoNoAr)";
    public string motoUpAnim = "8_Moto(SubindoNoAr)";
    public string motoGrauAnim = "8_Moto(Empinando)";
    public string motoCrashAnim = "8_Moto(Batendo)";
    public string motoLandAnim = "8_Moto(Aterrisando)";

    public enum State{ MotoWalk, MotoUp, MotoDown, MotoGrau, MotoCrash, MotoLand}

    public State state = State.MotoWalk;

    private Controller2D controller;
    [SerializeField]
    private UnityArmatureComponent player;
    [SerializeField]
    private UnityArmatureComponent playerFrente;

    [HideInInspector]
    public PhotonView photonView;

    public TriggerCollisionsController triggerCollisions;

    public EmpinaMoto empina;
    public BoolVariable levouDogada;

    private bool isOnline;


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

    public void ChangeMotoAnim(Vector3 moveAmount, Vector2 oldPos, bool stun)
    {
        if (stun == false)
        {

            if (empina.isEmpinando || empina.isManobrandoNoAr)
            {
                PlayAnim("MotoGrau");
            }


            else if (oldPos.y < moveAmount.y && controller.collisions.below == false)
            {
                PlayAnim("MotoUp");
            }

            else if (moveAmount.y <= 0 && controller.collisions.below == false)
            {
                PlayAnim("MotoDown");
            }

            else
            {
                PlayAnim("MotoWalk");
            }
        }

        else
        {
            PlayAnim("MotoCrash");
        }
    }

    private void PlayAnim(string anim)
    {
        if (isOnline)
        {
            photonView.RPC("MotoState", RpcTarget.All, anim);
        }
        else
        {
            MotoState(anim);
        }

    }

    [PunRPC]
    void MotoState(string anim)
    {
        switch (anim)
        {
            case "MotoWalk":
                if (state != State.MotoWalk)
                {
                    player.animation.Play(motoWalkAnim);
                    state = State.MotoWalk;
                }
                break;

            case "MotoCrash":
                if (state != State.MotoCrash)
                {
                    player.animation.Play(motoCrashAnim);
                    state = State.MotoCrash;
                }
                break;

            case "MotoUp":
                if (state != State.MotoUp)
                {
                    player.animation.Play(motoUpAnim);
                    state = State.MotoUp;
                }
                break;

            case "MotoDown":
                if (state != State.MotoDown)
                {
                    player.animation.Play(motoDownAnim);
                    state = State.MotoDown;
                }
                break;

            case "MotoGrau":
                if (state != State.MotoGrau)
                {
                    player.animation.Play(motoGrauAnim);
                    state = State.MotoGrau;
                }
                break;
        }
    }
}
