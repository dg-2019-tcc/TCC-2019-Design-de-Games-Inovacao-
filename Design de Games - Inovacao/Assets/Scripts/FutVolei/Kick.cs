﻿ using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    /*private PlayerMovement player;

    public float cooldownKick;

    public FloatVariable kickForce;
    public float kickForceX;
    public float kickForceY;

    public FloatVariable superKickForce;
    public float superKickForceX;

    private float forceVertical;

    private bool kicked;

    private bool rightDir;

    private Rigidbody2D ballrb;

    private BolaFutebol bola;

    public Joystick joyStick;

    public GameObject foot;

    public Transform footIncialPos;

    public Transform footKickPos;

    public void Start()
    {
		if (GetComponent<PhotonView>().IsMine || PhotonNetwork.InRoom)
		{
			joyStick = FindObjectOfType<Joystick>();
		}

        player = gameObject.GetComponentInParent<PlayerMovement>();
    }


    public void Update()
    {

        if (joyStick != null)
        {
            if (joyStick.Horizontal > 0)
            {
                rightDir = true;
                gameObject.GetComponent<PhotonView>().RPC("GiraFoot", RpcTarget.All, rightDir);
            }

            else if (joyStick.Horizontal < 0)
            {
                rightDir = false;
                gameObject.GetComponent<PhotonView>().RPC("GiraFoot", RpcTarget.All, rightDir);
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

        //foot.transform.position = footKickPos.transform.position;
        foot.SetActive(true);
        kicked = true;

        yield return new WaitForSeconds(cooldownKick);

        foot.SetActive(false);
        //foot.transform.position = footIncialPos.transform.position;
        kicked = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bola") && kicked == true && player.grounded == true)
        {
            ballrb = col.GetComponent<Rigidbody2D>();
            bola = col.GetComponent<BolaFutebol>();
            
            gameObject.GetComponent<PhotonView>().RPC("KickBola", RpcTarget.MasterClient);
        }

        if (col.CompareTag("Bola") && kicked == true && player.grounded == false)
        {
            ballrb = col.GetComponent<Rigidbody2D>();
            bola = col.GetComponent<BolaFutebol>();
            
            gameObject.GetComponent<PhotonView>().RPC("SuperKickBola", RpcTarget.MasterClient);
        }
    }

    /*private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Bola") && kicked == true)
        {
            ballrb = col.GetComponent<Rigidbody2D>();
            gameObject.GetComponent<PhotonView>().RPC("KickBola", RpcTarget.MasterClient);
        }
    }*//*

    [PunRPC]
    public void KickBola()
    {
        bola.kick = true;
        bola.normal = false;
        bola.superKick = false;
        Debug.Log("KickBola");
        ballrb.AddForce(new Vector2(kickForceX, forceVertical), ForceMode2D.Impulse);
    }

    [PunRPC]
    public void SuperKickBola()
    {
        bola.superKick = true;
        bola.kick = false;
        bola.normal = false;
        Debug.Log("SuperKickBola");
        ballrb.AddForce(new Vector2(superKickForceX, forceVertical), ForceMode2D.Impulse);
    }

    [PunRPC]
    void GiraFoot(bool dir)
    {
        if (dir)
        {
            //kickSizeX = 0.5f;
            kickForceX = kickForce.Value;
            superKickForceX = superKickForce.Value;
        }

        else
        {
            //kickSizeX = -0.5f;
            kickForceX = kickForce.Value * -1;
            superKickForceX = superKickForce.Value * -1;

        }
    }*/
}