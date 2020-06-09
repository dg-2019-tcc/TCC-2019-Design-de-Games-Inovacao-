﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutebolPlayer : MonoBehaviour
{
    public Controller2D controller;
    public TriggerCollisionsController triggerController;

    private GameObject bola;
    private Rigidbody2D ballrb;
    private BolaFutebol bolaFutebol;

    public float cooldownKick;

    public FloatVariable normalKickForce;
    public float normalX;
    public float normalY;

    public FloatVariable kickForce;
    private float kickForceX;
    public float kickForceY;
    public float randomForceY = 10f;

    public FloatVariable superKickForce;
    private float superKickForceX;
    public float superKickForceY;

    private float forceVertical;

    public static bool kicked;
    public bool kickAnim;


    private Player2DAnimations anim;

    [HideInInspector]
    public PhotonView photonView;


    void Awake()
    {
        photonView = gameObject.GetComponent<PhotonView>();

        controller = GetComponent<Controller2D>();
        triggerController = GetComponent<TriggerCollisionsController>();
        anim = GetComponent<Player2DAnimations>();

        bola = GameObject.FindWithTag("Futebol");
        ballrb = bola.GetComponent<Rigidbody2D>();
        bolaFutebol = bola.GetComponent<BolaFutebol>();
    }


    void Update()
    {
        if (photonView.IsMine == true || !PhotonNetwork.InRoom)
        {

            if (PlayerThings.rightDir)
            {
                normalX = normalKickForce.Value;
                kickForceX = kickForce.Value;
                superKickForceX = superKickForce.Value;
            }

            if (PlayerThings.leftDir)
            {
                normalX = normalKickForce.Value * -1;
                kickForceX = kickForce.Value * -1;
                superKickForceX = superKickForce.Value * -1;
            }

            if (kicked == false)
            {
                if (triggerController.collisions.tocouBola == true || triggerController.collisions.cabecaBola == true || triggerController.collisions.chutouBola == true)
                {
                    gameObject.GetComponent<PhotonView>().RPC("TocouBola", RpcTarget.MasterClient);
                    triggerController.collisions.tocouBola = false;
                    triggerController.collisions.cabecaBola = false;
                    triggerController.collisions.chutouBola = false;
                }
            }

            else
            {
                if (triggerController.collisions.chutouBola == true || triggerController.collisions.cabecaBola == true)
                {
                    if (controller.collisions.below == true)
                    {
                        gameObject.GetComponent<PhotonView>().RPC("KickBola", RpcTarget.MasterClient);
                        triggerController.collisions.chutouBola = false;
                        triggerController.collisions.cabecaBola = false;
                    }

                    else
                    {
                        gameObject.GetComponent<PhotonView>().RPC("SuperKickBola", RpcTarget.MasterClient);
                        triggerController.collisions.chutouBola = false;
                        triggerController.collisions.cabecaBola = false;
                    }
                }

            }
        }
    }

    public void Chute()
    {
        //Debug.Log(forceVertical);
        gameObject.GetComponent<PhotonView>().RPC("KickedBall", RpcTarget.MasterClient, forceVertical);
    }

    [PunRPC]
    public void KickedBall(float force)
    {
        forceVertical = force;
        if (kicked == false)
        {
            StartCoroutine("CoolKick");
        }
    }

    IEnumerator CoolKick()
    {
        //anim.Chute();
        kicked = true;
        kickAnim = true;
        anim.DogButtonAnim(kickAnim);

        yield return new WaitForSeconds(cooldownKick);
        kickAnim = false;
        anim.DogButtonAnim(kickAnim);
        kicked = false;
    }
    [PunRPC]
    public void TocouBola()
    {
        bolaFutebol.normal = true;

        ballrb.velocity = new Vector2(0, 0);
        ballrb.AddForce(new Vector2(normalX, normalY), ForceMode2D.Impulse);
    }

    [PunRPC]
    public void KickBola()
    {
        bolaFutebol.kick = true;
        bolaFutebol.normal = false;
        bolaFutebol.superKick = false;

        ballrb.AddForce(new Vector2(kickForceX, Random.Range(kickForceY - randomForceY, kickForceY + randomForceY)), ForceMode2D.Impulse);

    }

    [PunRPC]
    public void SuperKickBola()
    {
        bolaFutebol.superKick = true;
        bolaFutebol.kick = false;
        bolaFutebol.normal = false;

        ballrb.AddForce(new Vector2(superKickForceX, kickForceY), ForceMode2D.Impulse);
    }
}
