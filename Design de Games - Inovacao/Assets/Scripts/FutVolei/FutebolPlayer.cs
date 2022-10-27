﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;
using AI;

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

    public bool kicked;
    public bool kickAnim;


    //private Player2DAnimations anim;
    private PlayerAnimController playerAnim;

    [HideInInspector]
    public PhotonView photonView;

    #region Unity Function

    void Awake()
    {
        photonView = gameObject.GetComponent<PhotonView>();

        controller = GetComponent<Controller2D>();
        triggerController = GetComponent<TriggerCollisionsController>();
        //anim = GetComponent<Player2DAnimations>();
        playerAnim = GetComponent<PlayerAnimController>();

        bola = GameObject.FindWithTag("Futebol");
        ballrb = bola.GetComponent<Rigidbody2D>();
        bolaFutebol = bola.GetComponent<BolaFutebol>();
    }

    void Update()
    {
        if (PlayerThings.rightDir)
        {
            normalX = normalKickForce.Value;
            kickForceX = kickForce.Value;
            superKickForceX = superKickForce.Value;

            if (kicked)
            {
                triggerController.rightRay = true;
            }

            else
            {
                triggerController.leftRay = false;
                triggerController.rightRay = false;
            }
        }

        if (PlayerThings.leftDir)
        {
            normalX = normalKickForce.Value * -1;
            kickForceX = kickForce.Value * -1;
            superKickForceX = superKickForce.Value * -1;

            if (kicked)
            {
                triggerController.leftRay = true;
                triggerController.rightRay = false;
            }
            else
            {
                triggerController.leftRay = false;
                triggerController.rightRay = false;
            }
        }

        if (kicked == false)
        {
            if (triggerController.collisions.tocouBola == true || triggerController.collisions.cabecaBola == true || triggerController.collisions.chutouBola == true)
            {
                gameObject.GetComponent<PhotonView>().RPC("TocouBola", RpcTarget.All, normalX, normalY);
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
                    gameObject.GetComponent<PhotonView>().RPC("KickBola", RpcTarget.All, kickForceX, kickForceY);
                    triggerController.collisions.chutouBola = false;
                    triggerController.collisions.cabecaBola = false;
                }

                else
                {
                    gameObject.GetComponent<PhotonView>().RPC("SuperKickBola", RpcTarget.All, superKickForceX, kickForceY);
                    triggerController.collisions.chutouBola = false;
                    triggerController.collisions.cabecaBola = false;
                }
            }

        }

    }

    #endregion

    #region Public Functions

    public void Chute()
    {
        if (kicked == false)
        {
            gameObject.GetComponent<PhotonView>().RPC("KickedBall", RpcTarget.All);
        }
    }

    [PunRPC]
    public void KickedBall()
    {
        if (kicked == false)
        {
            StartCoroutine("CoolKick");
        }
    }

    public void KickBot(FootballFSMController _footFSM)
    {
        if (!kicked) return;
        _footFSM.StunnedByPlayer();
    }

    #endregion

    #region Private Functions

    IEnumerator CoolKick()
    {
        kicked = true;
        //anim.dogButtonAnim = kicked;
        playerAnim.dogButtonAnim = kicked;
        yield return new WaitForSeconds(cooldownKick);
        kicked = false;
        playerAnim.dogButtonAnim = kicked;
        //anim.dogButtonAnim = kicked;
    }

    [PunRPC]
    private void TocouBola(float forceX, float forceY)
    {

        bolaFutebol.normal = true;

        ballrb.velocity = new Vector2(0, 0);
        ballrb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
    }

    [PunRPC]
    private void KickBola(float forceX, float forceY)
    {
        bolaFutebol.kick = true;
        bolaFutebol.normal = false;
        bolaFutebol.superKick = false;

        ballrb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);

    }

    [PunRPC]
    private void SuperKickBola(float forceX, float forceY)
    {
        bolaFutebol.superKick = true;
        bolaFutebol.kick = false;
        bolaFutebol.normal = false;

        ballrb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
    }

    #endregion
}
